using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class GrabbableController : MonoBehaviour {
	private Renderer myRenderer;

	private Rigidbody2D rb;

	[SerializeField]
	[Range(0, float.MaxValue)]
	private float moveSpeed = 1.0f;

	Vector2 lastPosition;

	[SerializeField]
	[Range(0, float.MaxValue)]
	private float saveDelay = 0.2f;

	[SerializeField]
	[Range(0, float.MaxValue)]
	private float power = 5f;

	private bool nextSave = true;

	private bool isHeld;

	private Vector2 dir;

	private bool canBePushed;

	[SerializeField]
	[Range(0, float.MaxValue)]
	private float fadeSpeed = 1.0f;

	[SerializeField]
	private bool isDiscardable = true;
	public bool IsDiscardable {
		get => isDiscardable && !isHeld;
		protected set => isDiscardable = value;
	}

	private void Awake() {
		rb = GetComponent<Rigidbody2D>();
		myRenderer = GetComponent<Renderer>();
	}

	void Start() {
		lastPosition = transform.position;
	}

	void Update() {
		if( nextSave )
			StartCoroutine("SavePosition");
	}

	void FixedUpdate() {
		if( canBePushed ) {
			canBePushed = false;
			rb.velocity = dir * power;
		}
	}

	void OnMouseDown() {
		isHeld = true;
		World.Instance.Hand.Close();
	}

	void OnMouseUp() {
		isHeld = false;
		dir = (Vector2)transform.position - lastPosition;
		canBePushed = true;
		World.Instance.Hand.Open();
	}

	void OnMouseDrag() {
		transform.position = Vector2.Lerp(transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition), moveSpeed);
	}

	private IEnumerator SavePosition() {
		nextSave = false;
		lastPosition = transform.position;
		yield return new WaitForSeconds(saveDelay);
		nextSave = true;
	}

	public virtual void Discard() {
		IsDiscardable = false;
		World.Instance.Take(gameObject);
	}

	private IEnumerator FadeOut() {
		float alpha = myRenderer.material.color.a;

		while( alpha < 1 ) {
			alpha += fadeSpeed * Time.fixedDeltaTime;
			myRenderer.material.color = new Color(
				myRenderer.material.color.r,
				myRenderer.material.color.g,
				myRenderer.material.color.b,
				alpha
			);
			yield return null;
		}
	}
}
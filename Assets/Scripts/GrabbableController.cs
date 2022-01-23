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
	private bool fadeOnSpawn = true;

	[SerializeField]
	[Range(0, float.MaxValue)]
	private float fadeTime = 0.1f;

	[SerializeField]
	[Range(0, float.MaxValue)]
	private float spawnTime = 3.0f;

	private bool isDiscardable;
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
		Spawn();
	}

	void Update() {
		if( nextSave )
			StartCoroutine(SavePosition());
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
		dir = new Vector3(
			transform.position.x - lastPosition.x,
			transform.position.y - lastPosition.y,
			transform.position.z
		);
		canBePushed = true;
		World.Instance.Hand.Open();
	}

	void OnMouseDrag() {
		var pos = Vector2.Lerp(
			transform.position,
			World.Instance.InteriorCamera.ScreenToWorldPoint(Input.mousePosition),
			moveSpeed
		);
		var delta = new Vector3(
			pos.x - transform.position.x,
			pos.y - transform.position.y
		);

		transform.Translate(delta);
	}

	private IEnumerator SavePosition() {
		nextSave = false;
		lastPosition = transform.position;
		yield return new WaitForSeconds(saveDelay);
		nextSave = true;
	}

	public virtual void Spawn() {
		if( fadeOnSpawn ) {
			StartCoroutine(FadeIn());
		}
		else {
			IsDiscardable = true;
		}
	}

	public virtual void Discard() {
		IsDiscardable = false;
		StartCoroutine(FadeOut());
	}

	private IEnumerator FadeIn() {
		Color color = myRenderer.material.color;

		IsDiscardable = false;
		for( int alpha = 0; alpha < 10; ++alpha ) {
			color.a = alpha * 0.1f;
			myRenderer.material.color = color;

			yield return new WaitForSeconds(fadeTime);
		}
		yield return new WaitForSeconds(spawnTime);
		IsDiscardable = true;
	}

	private IEnumerator FadeOut() {
		Color color = myRenderer.material.color;

		for( int alpha = 10; alpha >= 0; --alpha ) {
			color.a = alpha * 0.1f;
			myRenderer.material.color = color;

			yield return new WaitForSeconds(fadeTime);
		}

		World.Instance.Take(gameObject);
	}
}
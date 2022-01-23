using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class GrabbableController : MonoBehaviour {
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

	public bool IsHeld { get; private set; } = false;

	Rigidbody2D rb;

	Vector2 dir;

	bool canBePushed;

	void Start() {
		lastPosition = transform.position;
		rb = GetComponent<Rigidbody2D>();
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
		IsHeld = true;
		World.Instance.Hand.Close();
	}

	void OnMouseUp() {
		IsHeld = false;
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
		Destroy(gameObject);
	}
}
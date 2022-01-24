using System.Collections;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class GrabbableController : MonoBehaviour {
	[SerializeField]
	private Renderer myRenderer;

	private Rigidbody2D myBody;

	private Collider2D myCollider;

	[SerializeField]
	[Range(0, int.MaxValue)]
	private int heldSortingOrder = 10;

	private int lastSortingOrder;

	[SerializeField]
	[Range(0, float.MaxValue)]
	private float moveSpeed = 1.0f;

	Vector2 lastPosition;

	[SerializeField]
	[Range(0, float.MaxValue)]
	private float saveDelay = 0.2f;

	[SerializeField]
	[Range(0, float.MaxValue)]
	private float power = 100.0f;

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
		myBody = GetComponent<Rigidbody2D>();
		myCollider = GetComponent<Collider2D>();

	}

	void Start() {
		lastPosition = transform.position;
		lastSortingOrder = myRenderer.sortingOrder;
		Spawn();
	}

	void Update() {
		if( nextSave )
			StartCoroutine(SavePosition());
	}

	void FixedUpdate() {
		if( canBePushed ) {
			canBePushed = false;
			myBody.AddForce(dir * power);
		}
	}


	void OnMouseDown() {
		Hold();
	}

	void OnMouseUp() {
		LetGo();
		dir = new Vector3(
			transform.position.x - lastPosition.x,
			transform.position.y - lastPosition.y,
			transform.position.z
		);
		canBePushed = true;
	}

	void OnMouseDrag() {
		var pos = ClampToScreen(Vector2.Lerp(
			transform.position,
			World.Instance.InteriorCamera.ScreenToWorldPoint(Input.mousePosition),
			moveSpeed)
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
		myRenderer.sortingOrder = 3;
		if( fadeOnSpawn ) {
			StartCoroutine(FadeIn());
		}
		else {
			IsDiscardable = true;
		}
	}

	public virtual void Discard() {
		myRenderer.sortingOrder = 0;

		IsDiscardable = false;
		StartCoroutine(FadeOut());
	}

	private void Hold() {
		isHeld = true;
		lastSortingOrder = myRenderer.sortingOrder;
		myRenderer.sortingOrder = heldSortingOrder;
		World.Instance.Hand.Close();
	}

	private void LetGo() {
		isHeld = false;
		myRenderer.sortingOrder = lastSortingOrder;
		World.Instance.Hand.Open();
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

	private Vector2 ClampToScreen(Vector2 pos) {
		const float PADDING = 0.02f;
		var camera = World.Instance.InteriorCamera;
		float y = camera.orthographicSize - (myCollider.bounds.size.y / 2) - PADDING;
		float x = (camera.orthographicSize * camera.aspect) - (myCollider.bounds.size.x / 2) - PADDING;
		float minX = camera.transform.position.x - x;
		float maxX = camera.transform.position.x + x;
		float minY = camera.transform.position.y - y;
		float maxY = camera.transform.position.y + y;

		if( pos.x < minX )
			pos.x += minX - pos.x;
		else if( pos.x > maxX )
			pos.x -= pos.x - maxX;
		if( pos.y < minY )
			pos.y += minY - pos.y;
		else if( pos.y > maxY )
			pos.y -= pos.y - maxY;

		return pos;
	}
}
using System.Collections;
using UnityEngine;

public class ImportedMovement : MonoBehaviour {
	private Vector2 direction;

	[SerializeField]
	private float moveSpeed = 3f;

	private Rigidbody2D body;

	private Collider2D collision;

	public float MoveSpeed { get => moveSpeed; private set => moveSpeed = value; }

	private void Awake() {
		body = GetComponent<Rigidbody2D>();
		collision = GetComponent<Collider2D>();
		direction = new Vector2(0f, 0f);
	}

	private void OnEnable() {
		ImportedPlayerInput.Moving += PlayerInput_Move;
	}

	private void OnDisable() {
		ImportedPlayerInput.Moving -= PlayerInput_Move;
	}

	private void Update() {
		if( Input.GetKeyDown(KeyCode.Space) ) {
			StartCoroutine(Jump(0.5f));
			Debug.Log("jump");
		}
	}

	private void FixedUpdate() {
		body.velocity = direction * moveSpeed;
	}

	private void PlayerInput_Move(object sender, ImportedPlayerInput.ImportedMovingEventArgs e) {
		direction = e.Direction;
	}

	private IEnumerator Jump(float jumpTime) {
		float jumpDistance = 1f;
		int layer = 1;
		float height = 2f;
		float obstacleHeight = 0.5f;

		// Obstacle obstacle;
		Collider2D obstacleCollider;

		Debug.Log("cast from " + transform.position + " towards " + direction + " for " + jumpDistance + " units in layer " + layer);

		RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, direction, jumpDistance, layer);

		Debug.Log("hits: " + hits.Length);

		foreach( RaycastHit2D hit in hits ) {
			Debug.Log("hit!");
			obstacleCollider = hit.transform.gameObject.GetComponent<Collider2D>();

			if( obstacleCollider != null && obstacleHeight < height ) {
				Physics2D.IgnoreCollision(collision, obstacleCollider, true);
				Debug.Log(hit.transform.gameObject.ToString());
			}
		}

		yield return new WaitForSeconds(jumpTime);

		foreach( RaycastHit2D hit in hits ) {
			obstacleCollider = hit.transform.gameObject.GetComponent<Collider2D>();

			if( obstacleCollider != null )
				Physics2D.IgnoreCollision(collision, obstacleCollider, false);
		}
	}
}
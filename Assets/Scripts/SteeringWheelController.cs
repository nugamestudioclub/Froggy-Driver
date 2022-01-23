using UnityEngine;
using UnityEngine.EventSystems;

public class SteeringWheelController : MonoBehaviour {
	private const int MAX_DEGREES = 360;

	[SerializeField]
	[Range(0, float.MaxValue)]
	private float resetSpeed = 50.0f;

	private float angle = 0.0f;

	[SerializeField]
	[Range(0, MAX_DEGREES / 2)]
	private float maxAngle = 90.0f;

	private bool mouseIsOver = false;

	private bool mouseIsDown = false;

	private static float AngleBetween(Vector2 p1, Vector2 p2) {
		return Mathf.Rad2Deg * Mathf.Atan2(p2.y - p1.y, p2.x - p1.x);
	}

	private static float Sign(float angle) {
		return angle < (MAX_DEGREES / 2) ? 1 : -1;
	}

	private float ClampToMax(float angle) {
		return Sign(angle) > 0
			? Mathf.Min(angle, maxAngle)
			: Mathf.Max(angle, MAX_DEGREES - maxAngle);
	}

	private static float ClampToZero(float angle) {
		const float EPSILON = 0.01f;

		return Abs(angle) > EPSILON ? angle : 0;
	}

	private static float Abs(float angle) {
		return Sign(angle) > 0 ? angle : MAX_DEGREES - angle;
	}

	private static bool IsZero(float angle) {
		return Mathf.Approximately(Abs(angle), 0);
	}

	private void Rotate(float deltaZ) {
		transform.Rotate(0.0f, 0.0f, deltaZ);
	}
	private float MouseAngle() {
		var pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

		return AngleBetween(
			new Vector2(pos.x, pos.y),
			new Vector2(transform.position.x, transform.position.y)
		);
	}

	private void Update() {
		if( !(mouseIsOver && mouseIsDown) ) {
			float z = transform.rotation.eulerAngles.z;
			float deltaZ = Sign(z) * resetSpeed * Time.fixedDeltaTime;

			z = ClampToZero(transform.rotation.eulerAngles.z - deltaZ);
			deltaZ = z - transform.rotation.eulerAngles.z;
			
			Rotate(deltaZ);
			mouseIsDown = IsZero(transform.rotation.eulerAngles.z);
		}
	}

	private void OnMouseOver() {
		mouseIsOver = true;
	}

	private void OnMouseExit() {
		mouseIsOver = false;
	}

	private void OnMouseDown() {
		mouseIsDown = true;
		angle = MouseAngle();
	}

	private void OnMouseUp() {
		mouseIsDown = false;
	}

	private void OnMouseDrag() {

		if( mouseIsOver && mouseIsDown ) {
			float mouseAngle = MouseAngle();
			float deltaZ = mouseAngle - angle;
			float z = ClampToMax(transform.rotation.eulerAngles.z + deltaZ);

			deltaZ = z - transform.rotation.eulerAngles.z;

			Rotate(deltaZ);

			angle = mouseAngle;
		}
	}

}
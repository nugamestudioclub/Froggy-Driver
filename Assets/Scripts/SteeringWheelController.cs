using UnityEngine;
using UnityEngine.EventSystems;

public class SteeringWheelController : MonoBehaviour {
	[SerializeField]
	[Range(0, float.MaxValue)]
	private float steeringSpeed = 100.0f;
	[SerializeField]
	[Range(0, float.MaxValue)]
	private float resetSpeed = 50.0f;
	[SerializeField]
	[Range(0, 180)]
	private float maxAngle = 30.0f;
	private bool isHeld = false;
	private const int MAX_DEGREES = 360;

	private static float Sign(float angle) {
		return angle < (MAX_DEGREES / 2) ? 1 : -1;
	}

	private float ClampToMax(float angle) {
		return Sign(angle) > 0
			? Mathf.Min(angle, maxAngle)
			: MAX_DEGREES - Mathf.Min(MAX_DEGREES - angle, maxAngle);
	}

	private static float ClampToZero(float angle) {
		const float EPSILON = 0.01f;

		return (Sign(angle) > 0 ? angle : MAX_DEGREES - angle) > EPSILON
			? angle
			: 0;
	}

	private static bool IsZero(float angle) {
		Debug.Log($"angle {angle}");

		return Mathf.Approximately(angle, 0.0f)
			   || Mathf.Approximately(angle, MAX_DEGREES);
	}

	private void Rotate(float deltaZ) {
		transform.Rotate(0.0f, 0.0f, deltaZ);
	}

	private void Update() {
		float z = transform.rotation.eulerAngles.z;

		Debug.Log($"held {isHeld}");

		if( !isHeld ) {
			float deltaZ = Sign(z) * resetSpeed * Time.fixedDeltaTime;

			z = ClampToZero(transform.rotation.eulerAngles.z - deltaZ);
			deltaZ = z - transform.rotation.eulerAngles.z;

			Rotate(deltaZ);
			isHeld = IsZero(transform.rotation.eulerAngles.z);
		}
	}

	private void OnMouseDown() {
		isHeld = true;
	}

	private void OnMouseDrag() {
		float deltaZ = Input.GetAxis("Mouse X") * steeringSpeed * Time.fixedDeltaTime;
		float z = ClampToMax(transform.rotation.eulerAngles.z - deltaZ);

		deltaZ = z - transform.rotation.eulerAngles.z;

		Rotate(deltaZ);
	}

	private void OnMouseUp() {
		isHeld = false;
	}
}
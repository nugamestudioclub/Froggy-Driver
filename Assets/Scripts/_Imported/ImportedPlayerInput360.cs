using UnityEngine;

public class PlayerInput360 : ImportedPlayerInput {
	[SerializeField] private float rotationSpeed = 1.0f;
	[SerializeField] private float reverseModifier = 0.4f;
	private Vector3 facing;

	private void Awake() {
		facing = new Vector3(0f, 1f);
	}

	private void Start() {
		OnFacing(Vector3.down);
	}

	private void Update() {
		float rAxis = PlayerInput.GetAxis("Horizontal");
		float vAxis = PlayerInput.GetAxis("Vertical");
		Vector3 movement = new Vector3();
		float angle;

		facing = Quaternion.Euler(0f, 0f, rotationSpeed * -rAxis) * facing;

		angle = Quaternion.FromToRotation(Vector3.up, facing).eulerAngles.z;
		// angle = 45f * Mathf.Round(angle / 45f);

		movement = vAxis * (Quaternion.Euler(0f, 0f, angle) * Vector3.up);
		movement.Normalize();
		if( vAxis < 0 )
			movement *= reverseModifier;

		OnMoving(movement, false);
		OnFacing(facing);

	}
	protected override void OnFacing(Vector3 direction) {
		base.OnFacing(direction);
	}

	protected override void OnMoving(Vector3 direction, bool jumping) {
		base.OnMoving(direction, jumping);
	}
}

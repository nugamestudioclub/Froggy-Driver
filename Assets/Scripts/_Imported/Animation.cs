using UnityEngine;

public class Animation : MonoBehaviour {
	private Animator animator;
	private void Awake() {
		animator = GetComponent<Animator>();
	}

	private void OnEnable() {
		PlayerInput.Moving += PlayerInput_Move;
		PlayerInput.Facing += PlayerInput_Face;
	}

	private void OnDisable() {
		PlayerInput.Moving -= PlayerInput_Move;
		PlayerInput.Facing -= PlayerInput_Face;
	}

	private void PlayerInput_Move(object sender, PlayerInput.MovingEventArgs e) {
		if( e.Direction.magnitude > 0f ) {
			animator.SetFloat("xDirection", e.Direction.x);
			animator.SetFloat("yDirection", e.Direction.y);
		}
		animator.SetFloat("Speed", e.Direction.magnitude);
	}

	private void PlayerInput_Face(object sender, PlayerInput.FacingEventArgs e) {
		animator.SetFloat("xDirection", e.Direction.x);
		animator.SetFloat("yDirection", e.Direction.y);
	}
}
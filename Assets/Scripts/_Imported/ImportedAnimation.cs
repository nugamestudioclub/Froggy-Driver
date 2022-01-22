using UnityEngine;

public class ImportedAnimation : MonoBehaviour {
	private Animator animator;
	private void Awake() {
		animator = GetComponent<Animator>();
	}

	private void OnEnable() {
		ImportedPlayerInput.Moving += PlayerInput_Move;
		ImportedPlayerInput.Facing += PlayerInput_Face;
	}

	private void OnDisable() {
		ImportedPlayerInput.Moving -= PlayerInput_Move;
		ImportedPlayerInput.Facing -= PlayerInput_Face;
	}

	private void PlayerInput_Move(object sender, ImportedPlayerInput.ImportedMovingEventArgs e) {
		if( e.Direction.magnitude > 0f ) {
			animator.SetFloat("xDirection", e.Direction.x);
			animator.SetFloat("yDirection", e.Direction.y);
		}
		animator.SetFloat("Speed", e.Direction.magnitude);
	}

	private void PlayerInput_Face(object sender, ImportedPlayerInput.ImportedFacingEventArgs e) {
		animator.SetFloat("xDirection", e.Direction.x);
		animator.SetFloat("yDirection", e.Direction.y);
	}
}
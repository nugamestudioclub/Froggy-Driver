using UnityEngine.SceneManagement;

public class CrossController : GrabbableController {
	public override void Discard() {
		base.Discard();
		// Win
		SceneManager.LoadScene("GameWon");
	}
}

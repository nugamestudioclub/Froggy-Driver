using UnityEngine;

public class Billboard : MonoBehaviour {
	[SerializeField]
	private Camera cam;


	void Start() {
		if( cam == null )
			cam = World.Instance.Camera(gameObject.layer);
	}

	void Update() {
		transform.LookAt(cam.transform);
	}
}
using UnityEngine;

public class Billboard : MonoBehaviour {
	private Camera cam;


	void Start() {
		if( cam == null )
			cam = World.Instance.Camera(gameObject.layer);
	}

	void Update() {
		if (cam != null)
			transform.LookAt(cam.transform);
	}
}
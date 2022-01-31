using UnityEngine;

public class Billboard : MonoBehaviour {
	[SerializeField]
	private Camera cam;


	void Start() {
		Debug.Log($"{nameof(cam)} null ? {cam == null}");
		Debug.Log($"layer {gameObject.layer}");
		Debug.Log($"camera {World.Instance.Camera(gameObject.layer)}");
		if( cam == null )
			cam = World.Instance.Camera(gameObject.layer);
	}

	void Update() {
		transform.LookAt(cam.transform);
	}
}
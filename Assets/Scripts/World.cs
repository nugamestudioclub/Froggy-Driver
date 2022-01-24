using UnityEngine;

public class World : MonoBehaviour {
	public static World Instance { get; private set; }

	[SerializeField]
	private Hand hand;
	public Hand Hand => hand;

	[SerializeField]
	private Camera interiorCamera;
	public Camera InteriorCamera => interiorCamera;

	[SerializeField]
	private Camera exteriorCamera;
	public Camera ExteriorCamera => exteriorCamera;

	[SerializeField]
	private CabinController cabin;

	void Awake() {
		Instance = this;
	}

	public void Take(GameObject obj) {
		Destroy(obj);
	}

	public void Give(GameObject obj) {
		cabin.Take(obj);
	}
}
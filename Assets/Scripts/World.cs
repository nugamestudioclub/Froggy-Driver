using UnityEngine;

public class World : MonoBehaviour {
	public static World Instance { get; private set; }

	void Awake() {
		Instance = this;
	}
}
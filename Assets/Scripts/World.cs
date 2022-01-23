using UnityEngine;

public class World : MonoBehaviour {
	public static World Instance { get; private set; }

	[SerializeField]
	private Hand hand;
	public Hand Hand => hand;

	void Awake() {
		Instance = this;
	}
}
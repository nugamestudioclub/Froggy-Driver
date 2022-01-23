using UnityEngine;

public class Hand : MonoBehaviour {
	[SerializeField]
	private Texture2D openTexture;

	[SerializeField]
	private Texture2D closedTexture;

	[SerializeField]
	private Vector2 offset = Vector2.zero;

	private void Start() {
		Open();
	}

	public void Open() {
		Cursor.SetCursor(openTexture, offset, CursorMode.Auto);
	}

	public void Close() {
		Cursor.SetCursor(closedTexture, offset, CursorMode.Auto);
	}
}

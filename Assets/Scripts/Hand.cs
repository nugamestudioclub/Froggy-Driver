using UnityEngine;

public class Hand : MonoBehaviour {
	[SerializeField]
	private Texture2D openTexture;

	[SerializeField]
	private Texture2D closedTexture;

	private Vector2 offset;

	private void Start() {
		offset = new Vector2(openTexture.width / 2, openTexture.height / 2);
		Open();
	}

	public void Open() {
		Cursor.SetCursor(openTexture, offset, CursorMode.Auto);
	}

	public void Close() {
		Cursor.SetCursor(closedTexture, offset, CursorMode.Auto);
	}
}

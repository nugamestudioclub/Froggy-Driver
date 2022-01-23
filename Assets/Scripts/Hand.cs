using System.Collections;
using UnityEngine;

public class Hand : MonoBehaviour {
	[SerializeField]
	private Texture2D openTexture;

	[SerializeField]
	private Texture2D closedTexture;

	[SerializeField]
	private float clickSpeed = 0.2f;

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

	public void Touch() {
		Close();
		StartCoroutine(WaitAndOpen());
	}

	private IEnumerator WaitAndOpen() {
		yield return new WaitForSeconds(clickSpeed);
		Open();
	}
}

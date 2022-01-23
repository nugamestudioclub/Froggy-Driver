using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public abstract class ToggleableController : MonoBehaviour
{

	[SerializeField]
	private bool on;
	



	private void OnMouseDown()
	{

		Debug.Log($"Clicking {gameObject.name}");
		on = !on;
	}

	public abstract void Toggle();

}

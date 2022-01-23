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
		Toggle();
		World.Instance.Hand.Close();
		StartCoroutine(OpenHand());
		
	}

	private IEnumerator OpenHand()
	{
		yield return new WaitForSeconds(.2f);
		World.Instance.Hand.Open();
	}


    public abstract void Toggle();

	public bool IsOn()
    {
		return on;
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class CollectableController : MonoBehaviour {
	[SerializeField]
	private GameObject item;

	void OnTriggerEnter2D(Collider2D collision) {
		if( collision.tag == "Player" )
			World.Instance.Give(item);
		Destroy(gameObject);
	}
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Portal : MonoBehaviour {
	private Collider2D myCollider;

	private bool Contains(Collider2D collision) {
		return myCollider.bounds.Contains(collision.bounds.min)
			&& myCollider.bounds.Contains(collision.bounds.max);
	}

	private void DiscardIfPossible(Collider2D collision) {
		var grab = collision.gameObject.GetComponent<GrabbableController>();

		if( grab != null && !grab.IsHeld && Contains(collision) )
			grab.Discard();
	}

	private void Start() {
		myCollider = GetComponent<Collider2D>();
	}

	void OnTriggerStay2D(Collider2D collision) {
		DiscardIfPossible(collision);
	}
}
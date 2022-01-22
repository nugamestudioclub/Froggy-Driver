using System;
using UnityEngine;

public abstract class PlayerInput : MonoBehaviour {
	public class MovingEventArgs : EventArgs {
		public Vector3 Direction { get; set; }
	}

	public static event EventHandler<MovingEventArgs> Moving;

	protected virtual void OnMoving(Vector3 direction, bool jumping) {
		MovingEventArgs args = new MovingEventArgs {
			Direction = direction
		};

		Moving?.Invoke(this, args);
	}
}
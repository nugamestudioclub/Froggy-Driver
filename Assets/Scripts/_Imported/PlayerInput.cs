using System;
using UnityEngine;

public abstract class PlayerInput : MonoBehaviour {
	// Facing event

	public class FacingEventArgs : EventArgs {
		public Vector3 Direction { get; set; }
	}

	public static event EventHandler<FacingEventArgs> Facing;

	protected virtual void OnFacing(Vector3 direction) {
		FacingEventArgs args = new FacingEventArgs {
			Direction = direction
		};

		Facing?.Invoke(this, args);
	}

	// Moving event

	public class MovingEventArgs : EventArgs {
		public Vector3 Direction { get; set; }
		public bool Jumping { get; set; }
	}

	public static event EventHandler<MovingEventArgs> Moving;

	protected virtual void OnMoving(Vector3 direction, bool jumping) {
		MovingEventArgs args = new MovingEventArgs {
			Direction = direction,
			Jumping = jumping
		};

		Moving?.Invoke(this, args);
	}

	// Jumping Event

	public class JumpingEventArgs : EventArgs {
		public Vector3 Direction { get; set; }
	}

	public static event EventHandler<JumpingEventArgs> Jumping;
	protected virtual void OnJumping(Vector3 direction) {
		JumpingEventArgs args = new JumpingEventArgs {
			Direction = direction
		};

	Jumping?.Invoke(this, args);
	}
}

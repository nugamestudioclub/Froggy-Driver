using System;
using UnityEngine;

public abstract class ImportedPlayerInput : MonoBehaviour {
	// Facing event

	public class ImportedFacingEventArgs : EventArgs {
		public Vector3 Direction { get; set; }
	}

	public static event EventHandler<ImportedFacingEventArgs> Facing;

	protected virtual void OnFacing(Vector3 direction) {
		ImportedFacingEventArgs args = new ImportedFacingEventArgs {
			Direction = direction
		};

		Facing?.Invoke(this, args);
	}

	// Moving event

	public class ImportedMovingEventArgs : EventArgs {
		public Vector3 Direction { get; set; }
		public bool Jumping { get; set; }
	}

	public static event EventHandler<ImportedMovingEventArgs> Moving;

	protected virtual void OnMoving(Vector3 direction, bool jumping) {
		ImportedMovingEventArgs args = new ImportedMovingEventArgs {
			Direction = direction,
			Jumping = jumping
		};

		Moving?.Invoke(this, args);
	}

	// Jumping Event

	public class ImportedJumpingEventArgs : EventArgs {
		public Vector3 Direction { get; set; }
	}

	public static event EventHandler<ImportedJumpingEventArgs> Jumping;
	protected virtual void OnJumping(Vector3 direction) {
		ImportedJumpingEventArgs args = new ImportedJumpingEventArgs {
			Direction = direction
		};

	Jumping?.Invoke(this, args);
	}
}

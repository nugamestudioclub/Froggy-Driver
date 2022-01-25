using UnityEngine;

public class CabinController : MonoBehaviour {
	[SerializeField]
	private Vector2 spawnPointA;

	[SerializeField]
	[Range(0, 360)]
	private float spawnAngleA;

	[SerializeField]
	private Vector2 spawnPointB;

	[SerializeField]
	[Range(0, 360)]
	private float spawnAngleB;

	[SerializeField]
	[Range(0, float.MaxValue)]
	private float spawnSpeed = 25.0f;

	public void Take(GameObject obj) {
		Vector3 spawnPoint;
		float spawnAngle;

		(spawnPoint, spawnAngle) = Random.Range(0, 2) switch {
			0 => (spawnPointA, spawnAngleA),
			_ => (spawnPointB, spawnAngleB)
		};
		spawnPoint.z = gameObject.transform.position.z;

		var instance = Instantiate(obj, spawnPoint, Quaternion.identity);

		if( instance.TryGetComponent(out Rigidbody2D body) )
			body.velocity = new Vector2(
				Mathf.Cos(spawnAngle * Mathf.Deg2Rad),
				Mathf.Sin(spawnAngle * Mathf.Deg2Rad)
			).normalized * spawnSpeed;
	}
}
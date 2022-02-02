using UnityEngine;

public class CabinController : MonoBehaviour
{
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
    private float spawnSpeed = 30.0f;

    [SerializeField]
    [Range(0, 10)]
    private float spawnAngleRandomness = 0.0f;

    [SerializeField]
    [Range(0, 25)]
    private float spawnSpeedRandomness = 0.0f;

    public void Take(GameObject obj)
    {
        Vector3 position;
        float angle;
        const int OFFSET = 90;

        (position, angle) = Random.Range(0, 2) switch
        {
            0 => (spawnPointA, spawnAngleA),
            _ => (spawnPointB, spawnAngleB)
        };
        position.z = gameObject.transform.position.z;

        var instance = Instantiate(obj, position, Quaternion.identity);

        if (instance.TryGetComponent(out Rigidbody2D body))
        {
            angle += Random.Range(-spawnAngleRandomness, spawnAngleRandomness);

            float speed = spawnSpeed + Random.Range(-spawnSpeedRandomness, spawnSpeedRandomness);

            body.velocity = new Vector2(
                Mathf.Cos((angle + OFFSET) * Mathf.Deg2Rad),
                Mathf.Sin((angle + OFFSET) * Mathf.Deg2Rad)
            ).normalized * speed;
        }
    }
}
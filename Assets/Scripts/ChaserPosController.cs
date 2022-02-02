using UnityEngine;

public class ChaserPosController : MonoBehaviour
{
    [SerializeField]
    private Transform playerPos;
    [SerializeField]
    private Transform enemyPos;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (enemyPos == null)
        {
            transform.position = new Vector2(1000, 1000);
        }
        else
        {
            transform.position = Vector2.MoveTowards(playerPos.position, enemyPos.position, 2.5f);
        }

    }
}

using System.Collections;
using System.Collections.Generic;
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
        transform.position = Vector2.MoveTowards(playerPos.position, enemyPos.position, 2.5f);
    }
}

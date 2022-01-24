using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChaserController : MonoBehaviour
{
    [SerializeField]
    private Transform playerPos;
    [SerializeField]
    private float maxSpeed;
    [SerializeField]
    private float acceleration;

    private float chaseSpeed;
    private BoxCollider2D bc;

    // Start is called before the first frame update
    void Start()
    {
        Random.InitState(10);
        chaseSpeed = 0;
        bc = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(accelerate());
        if (bc.OverlapPoint(playerPos.position))
        {
            SceneManager.LoadScene("Trash");
        }
   
        transform.position = Vector2.Lerp(transform.position, playerPos.position, 1 - Mathf.Exp(-chaseSpeed * Time.deltaTime));
        transform.rotation = Quaternion.Slerp(transform.rotation, playerPos.rotation, Time.deltaTime * 5f);

    }

    IEnumerator accelerate()
    {
        if (chaseSpeed <= maxSpeed)
        {
            chaseSpeed += acceleration;
        }
        yield return new WaitForSeconds(1f);
    }
}

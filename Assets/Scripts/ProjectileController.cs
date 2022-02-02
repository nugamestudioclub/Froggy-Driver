using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ProjectileController : MonoBehaviour
{
    [SerializeField]
    private float speed;
    [SerializeField]
    private float deathTimer;
    private bool deleteThis = true;
    private Vector3 dir;
    private Transform playerPos;
    private BoxCollider2D bc;
    // Start is called before the first frame update
    void Start()
    {
        playerPos = GameObject.FindWithTag("Player").transform;
        dir = (playerPos.position - transform.position).normalized;
        bc = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        playerPos = GameObject.FindWithTag("Player").transform;
        if (bc.OverlapPoint(playerPos.position))
        {
            SceneManager.LoadScene("GameOver");
        }
        transform.position += dir * Time.deltaTime * speed;
        if (deleteThis)
        {
            StartCoroutine(delete());
        }
    }

    IEnumerator delete()
    {
        deleteThis = false;
        yield return new WaitForSeconds(deathTimer);
        Destroy(gameObject);
    }
}

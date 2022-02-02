using System.Collections;
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
    [SerializeField]
    private float shootDelay;
    [SerializeField]
    private GameObject projectile;

    private float chaseSpeed;
    private BoxCollider2D bc;
    private bool inShoot = false;
    // Start is called before the first frame update
    void Start()
    {
        chaseSpeed = 0;
        bc = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(accelerate());
        if (bc.OverlapPoint(playerPos.position))
        {
            SceneManager.LoadScene("GameOver");
        }
        float newX;
        float newY;
        if (transform.position.x > playerPos.position.x)
        {
            newX = transform.position.x - chaseSpeed * Time.deltaTime;
        }
        else
        {
            newX = transform.position.x + chaseSpeed * Time.deltaTime;
        }

        if (transform.position.y > playerPos.position.y)
        {
            newY = transform.position.y - chaseSpeed * Time.deltaTime;
        }
        else
        {
            newY = transform.position.y + chaseSpeed * Time.deltaTime;
        }

        transform.position = new Vector2(newX, newY);
        transform.rotation = Quaternion.Slerp(transform.rotation, playerPos.rotation, Time.deltaTime * 5f);
        if (!inShoot)
        {
            StartCoroutine(shoot());
        }
    }

    IEnumerator accelerate()
    {
        if (chaseSpeed <= maxSpeed)
        {
            chaseSpeed += acceleration;
        }
        yield return new WaitForSeconds(1f);
    }

    IEnumerator shoot()
    {
        inShoot = true;
        Instantiate(projectile, transform.position, transform.rotation);
        yield return new WaitForSeconds(shootDelay);
        inShoot = false;
    }
}

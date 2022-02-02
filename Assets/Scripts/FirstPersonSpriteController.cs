using UnityEngine;

public class FirstPersonSpriteController : MonoBehaviour
{
    [SerializeField]
    private Camera fpCamera;


    // Update is called once per frame
    void Update()
    {
        transform.LookAt(fpCamera.transform);
    }
}

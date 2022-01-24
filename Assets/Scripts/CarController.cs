using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.EventSystems;

public class CarController : MonoBehaviour
{
    [SerializeField]
    private CameraShake cameraShake;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShakeCar()
    {
        Debug.Log("Car shaking");
        //apply force from below

        //camera shake
        cameraShake.ShakeCamera();
        //sound effect
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomOutCamController : ToggleableController
{
    [SerializeField]
    private Camera cam;
    [SerializeField]
    private float maxCamSize = 10;
    public override void Toggle()
    {
        if (cam.orthographicSize < maxCamSize)
        {
            cam.orthographicSize++;
        }
    }


}

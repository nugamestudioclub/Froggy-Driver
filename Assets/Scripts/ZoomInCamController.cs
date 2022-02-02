using UnityEngine;

public class ZoomInCamController : ToggleableController
{
    [SerializeField]
    private Camera cam;
    [SerializeField]
    private float minCamSize = 3;
    public override void Toggle()
    {
        if (cam.orthographicSize > minCamSize)
        {
            cam.orthographicSize--;
        }
    }

}

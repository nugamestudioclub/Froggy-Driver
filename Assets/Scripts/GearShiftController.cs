using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GearShiftController : ToggleableController
{

    [SerializeField]
    private CarMovement externalCar;

    [SerializeField]
    private Vector3 startingPos;
    [SerializeField]
    private Vector3 alternatePos;


    public override void Toggle()
    {
        //swap sprite

        //move collider to other area
        transform.position = IsOn() ? startingPos : alternatePos;
        //toggle reverse on car
        externalCar.ToggleReverse();
    }
}

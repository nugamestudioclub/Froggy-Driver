using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GearShiftController : ToggleableController
{

    [SerializeField]
    private CarMovement externalCar;

    [SerializeField]
    private Vector2 startingPos;
    [SerializeField]
    private Vector2 alternatePos;

    private Collider2D collider2d;
    private SpriteRenderer sprite;
    [SerializeField]
    private Sprite driveSprite;
    [SerializeField]
    private Sprite reverseSprite;

    private void Start()
    {
        collider2d = GetComponent<Collider2D>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        collider2d.offset = startingPos;
    }

    public override void Toggle()
    {
       
        if (IsOn())
        {
            sprite.sprite = reverseSprite;
            collider2d.offset = alternatePos;
            

        } else
        {
            sprite.sprite = driveSprite;//swap sprite
            collider2d.offset = startingPos; //move collider to other area
        }
   
        //move collider to other area

        //toggle reverse on car
        externalCar.ToggleReverse();
    }
}

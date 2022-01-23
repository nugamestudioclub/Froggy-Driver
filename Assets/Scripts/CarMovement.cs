using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMovement : MonoBehaviour
{

    /// <summary>
    /// Direction facing (for when direction you move in by default
    /// andgle of wheels (how much to turn)
    /// speed
    /// 
    /// </summary>
    /// 
    //[SerializeField]
    //private Rigidbody2D rb;
    [SerializeField]
    GameObject backAxel;
    [SerializeField]
    private Transform rotationAxis;


    float acceleration;
    float angularAcceleration;
    float currentSpeed;
    float maxSpeed = 20;
    Rigidbody2D rb;
    bool breaking;

    Vector2 facing;
    float tiresAngle; //(-1 to 1)

    const float MAX_ANGLE = 30;
    const float MIN_ANGLE = -30;

    public CarController carInterior;


    // Start is called before the first frame update
    void Start()
    {
        //get components we need
        rb = GetComponentInChildren<Rigidbody2D>();
        breaking = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //get controls

        //add forces 
        
        //if break not active
        if (!breaking)
        {
            //rb.
            rb.AddForce(rb.transform.up * acceleration);
        }

        GetCarInputs();

        //take in turning
        // Create car rotation
        float direction = Vector2.Dot(rb.velocity, rb.GetRelativeVector(Vector2.up));
        //transform.Rotate(Vector3.forward, tiresAngle * MAX_ANGLE * Time.fixedDeltaTime);
        if (direction >= Mathf.Epsilon)
        {
            rb.rotation += tiresAngle * angularAcceleration * (rb.velocity.magnitude / maxSpeed);
        }
        else
        {
            rb.rotation -= tiresAngle * angularAcceleration * (rb.velocity.magnitude / maxSpeed);
        }
        //rb.AddTorque(tiresAngle);

        // Change velocity based on rotation
        float driftForce = Vector2.Dot(rb.velocity, rb.GetRelativeVector(Vector2.left)) * 2.0f;
        Vector2 relativeForce = Vector2.right * driftForce;
        Debug.DrawLine(rb.position, rb.GetRelativePoint(relativeForce), Color.green);
        rb.AddForce(rb.GetRelativeVector(relativeForce));


       // rb.AddForce(transform.right * tiresAngle * rb.velocity.magnitude);

        // Force max speed limit
        if (rb.velocity.magnitude > maxSpeed)
        {
            rb.velocity = rb.velocity.normalized * maxSpeed;
        }
        currentSpeed = rb.velocity.magnitude;

    }

    private void GetCarInputs()
    {
        if (Input.GetKey(KeyCode.D)){
            NormalizeTiresAngle(-30f);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            NormalizeTiresAngle(30f);
        } else
        {
            NormalizeTiresAngle(0f);
        }



        acceleration = 5;
        angularAcceleration = 2;

    }


    private void NormalizeTiresAngle(float angle)
    {
        tiresAngle =  (angle) / (MAX_ANGLE);
        Debug.Log($"Tires angle is {tiresAngle}!");
    }
 

    private void OnCollisionEnter2D(Collision2D collision)
    {
        carInterior.ShakeCar();
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
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


    public float acceleration = 5;
    public float angularAcceleration = 2;
    public float maxSpeed = 20;
    [ReadOnly]
    public float currentSpeed;

    Rigidbody2D rb;
    bool reverse = false;
    float tiresAngle; //(-1 to 1)

    float MAX_ANGLE = 30;
    const float MIN_ANGLE = -30;

    public CarController carInterior;
    public SteeringWheelController wheel;


    // Start is called before the first frame update
    void Start()
    {
        //get components we need
        rb = GetComponentInChildren<Rigidbody2D>();
        MAX_ANGLE =  wheel.MaxAngle;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        

        
        
        //if break not active
        if (!reverse) //add forces 
        {
            rb.AddForce(rb.transform.up * acceleration);
        } else
        {
            rb.AddForce(-rb.transform.up * acceleration);
        }


        //get controls
        GetCarInputs();

        //take in turning
        // Create car rotation
        float direction = Vector2.Dot(rb.velocity, rb.GetRelativeVector(Vector2.up));
        if (direction >= Mathf.Epsilon)
        {
            rb.rotation += tiresAngle * angularAcceleration * (rb.velocity.magnitude / maxSpeed);
        }
        else
        {
            rb.rotation -= tiresAngle * angularAcceleration * (rb.velocity.magnitude / maxSpeed);
        }


        // Change velocity based on rotation
        float driftForce = Vector2.Dot(rb.velocity, rb.GetRelativeVector(Vector2.left)) * 2.0f;
        Vector2 relativeForce = Vector2.right * driftForce;
        Debug.DrawLine(rb.position, rb.GetRelativePoint(relativeForce), Color.green);
        rb.AddForce(rb.GetRelativeVector(relativeForce));


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
            NormalizeTiresAngle(SteeringWheelController.Sign(wheel.transform.rotation.eulerAngles.z) * 
                SteeringWheelController.Abs(wheel.transform.rotation.eulerAngles.z));
        }

    }


    private void NormalizeTiresAngle(float angle)
    {
        //Debug.Log($"Wheel angle is {angle}!");
        tiresAngle =  (angle) / (MAX_ANGLE);
      //  Debug.Log($"Tires angle is {tiresAngle}!");
    }
 

    private void OnCollisionEnter2D(Collision2D collision)
    {
        carInterior.ShakeCar();
    }

    public void ToggleReverse()
    {
        reverse = !reverse;
    }
}

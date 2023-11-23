using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    public float maxPower;
    private Rigidbody ball;
    private float angle;
    private Vector3 position;

    private void Awake()
    {
        ball = GetComponent<Rigidbody>();
        ball.maxAngularVelocity = 1000;
        position = transform.position;
    }

    void OnCollisionEnter(Collision collision)
    {
        // Check if the collision is with the wall
        if (collision.gameObject.CompareTag("BallCatcher"))
        {
            // Reset the ball to its original position
            ResetToOriginalPosition();
        }
    }

    private void ResetToOriginalPosition()
    {
        transform.position = position;
        // You might also want to reset any velocity or other properties of the Rigidbody
        ball.velocity = Vector3.zero;
        ball.angularVelocity = Vector3.zero;
    }
}

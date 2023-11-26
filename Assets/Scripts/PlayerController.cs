using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Transform ball;
    private Vector3 offset = new Vector3(5f, 1f, 5f);
    public bool isMoving;
    public float moveSpeed = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public void ResetPlayer()
    {
        if (ball != null)
        {
            if (ball.position.x < 0)
                offset.x = -3f;

            if (ball.position.x >= 0)
                offset.x = 3f;

            transform.position = ball.position + offset;
        }
    }

    public void MoveTowardsBall()
    {
        if (ball != null)
        {
            Vector3 direction = ball.position - transform.position;

            float distance = direction.magnitude;

            // Check if the player has reached the ball
            if (distance <= 0.1f)
            {
                // Stop moving
                isMoving = false;
            }
            else
            {
                // Normalize the direction to get a unit vector
                direction.Normalize();

                // Move the player towards the ball
                transform.Translate(direction * moveSpeed * Time.deltaTime);
            }
        }
    }
}

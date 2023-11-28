using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Transform ball;
    private Vector3 offset = new Vector3(5f, 1f, 5f);
    public bool isMoving;
    private float moveSpeed = 5f;

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
            direction.z += 1;
            float distance = direction.magnitude;

            if (distance <= 0.1f)
            {
                isMoving = false;
            }
            else
            {
                direction.Normalize();
                transform.Translate(direction * moveSpeed * Time.deltaTime);
            }
        }
    }
}

using UnityEngine;

public class BallController : MonoBehaviour
{
    public float maxPower;
    public Transform nets;
    public CameraController cameraFollow;
    public LivesManager livesManager;
    public PlayerController player;
    private Rigidbody ball;
    private float angle;
    private Vector3 position;
    private LineTrajectory lineTrajectory;

    private void Awake()
    {
        ball = GetComponent<Rigidbody>();
        ball.maxAngularVelocity = 1000;
        position = transform.position;
        lineTrajectory = GameObject.Find("Line").GetComponent<LineTrajectory>();
        cameraFollow = Camera.main.GetComponent<CameraController>();
        player = GameObject.Find("Player").GetComponent<PlayerController>();
        livesManager = GameObject.Find("Lives").GetComponent<LivesManager>();
    }

    void OnCollisionEnter(Collision collision)
    {
        // Check if the collision is with the wall
        if (collision.gameObject.CompareTag("BallCatcher"))
        {
            livesManager.CheckIfPlayerLosesALife();
            // Reset the ball to its original position
            ResetToOriginalPosition();

            if (cameraFollow != null)
            {
                cameraFollow.ResetCamera();
            }

            if (player != null)
            {
                player.ResetPlayer();
            }
        }
    }

    private void ResetToOriginalPosition()
    {
        // Generate a random position within a specified range
        float randomX = Random.Range(-40, 40);
        float randomY = Random.Range(0.5f, 0.5f);
        float randomZ = Random.Range(-47, 61);

        // Set the position of the ball to the random coordinates
        transform.position = new Vector3(randomX, randomY, randomZ);
        // You might also want to reset any velocity or other properties of the Rigidbody
        ball.velocity = Vector3.zero;
        ball.angularVelocity = Vector3.zero;
        Quaternion target = Quaternion.Euler(0, 0, 0);
        transform.rotation = target;
        lineTrajectory.isPowerAdjustable = true;
        lineTrajectory.ResetLineAim();

    }
}

using System.Collections;
using UnityEngine;

public class BallController : MonoBehaviour
{
    public float maxPower;
    public Transform nets;
    public CameraController cameraFollow;
    public LivesManager livesManager;
    public PlayerController player;
    private Rigidbody ball;
    private AimAndShoot aimAndShoot;

    public float rollingStartTime;
    private const float rollingCheckDuration = 5f; // Adjust as needed
    private const float rollingVelocityThreshold = 5f; // Adjust as needed

    private void Start()
    {
        ball = GetComponent<Rigidbody>();
        ball.maxAngularVelocity = 1000;
        aimAndShoot = GameObject.Find("Line").GetComponent<AimAndShoot>();
        cameraFollow = Camera.main.GetComponent<CameraController>();
        player = GameObject.Find("Player").GetComponent<PlayerController>();
        livesManager = GameObject.Find("Lives").GetComponent<LivesManager>();

        
    }

    void FixedUpdate()
    {
        if (aimAndShoot.hasShot && Mathf.Abs(ball.velocity.z) < rollingVelocityThreshold)
        {
            // If the ball's z-axis velocity is below the threshold, start or update the rolling timer
            if (rollingStartTime == 0f)
            {
                rollingStartTime = Time.time;
            }
            else if (Time.time - rollingStartTime > rollingCheckDuration)
            {
                HandleWallCollision();
            }
        }
        else
        {
            // If the ball's velocity is above the threshold, reset the rolling timer
            rollingStartTime = 0f;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("BallCatcher") || collision.gameObject.CompareTag("Sideline"))
        {
            HandleWallCollision();
        }
    }

    public void HandleWallCollision()
    {
        livesManager.CheckIfPlayerLosesALife();
        SetBallToRandomPosition();
    }

    private void SetBallToRandomPosition()
    {
        aimAndShoot.ResetAim();
        ResetBallState();
        
        Vector3 randomPosition = GenerateRandomPosition();
        while (Vector3.Distance(randomPosition, nets.position) > 55)
        {
            randomPosition = GenerateRandomPosition();
        }        
        transform.position = randomPosition;
        ball.MovePosition(randomPosition);

        aimAndShoot.isPowerAdjustable = true;
        ResetCameraAndPlayer();
    }

    private Vector3 GenerateRandomPosition()
    {
        float randomX = Random.Range(-40, 40);
        float randomY = Random.Range(0.5f, 0.5f);
        float randomZ = Random.Range(-47, 61);

        return new Vector3(randomX, randomY, randomZ);
    }

    private void ResetBallState()
    {
        ball.velocity = Vector3.zero;
        ball.angularVelocity = Vector3.zero;
        transform.rotation = Quaternion.Euler(Vector3.zero);
    }

    private void ResetCameraAndPlayer()
    {
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

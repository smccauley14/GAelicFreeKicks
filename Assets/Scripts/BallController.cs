using UnityEngine;

public class BallController : MonoBehaviour
{
    public float maxPower;
    public Transform nets;
    public CameraController cameraFollow;
    public LivesManager livesManager;
    public PlayerController player;
    private Rigidbody ball;
    private AimAndShoot lineTrajectory;

    private void Start()
    {
        ball = GetComponent<Rigidbody>();
        ball.maxAngularVelocity = 1000;
        lineTrajectory = GameObject.Find("Line").GetComponent<AimAndShoot>();
        cameraFollow = Camera.main.GetComponent<CameraController>();
        player = GameObject.Find("Player").GetComponent<PlayerController>();
        livesManager = GameObject.Find("Lives").GetComponent<LivesManager>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("BallCatcher"))
        {
            HandleWallCollision();
        }
    }

    private void HandleWallCollision()
    {
        livesManager.CheckIfPlayerLosesALife();
        SetBallToRandomPosition();
    }

    private void SetBallToRandomPosition()
    {
        Vector3 randomPosition = GenerateRandomPosition();
        transform.position = randomPosition;

        ResetBallState();
        lineTrajectory.isPowerAdjustable = true;
        lineTrajectory.ResetAim();

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

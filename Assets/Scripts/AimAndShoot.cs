using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class AimAndShoot : MonoBehaviour
{
    public Transform ball;
    public Slider powerSlider;
    public PlayerController player;
    private GameManager gameManager;
    private LivesManager livesManager;
    private BallController ballController;

    public float maxDistanceX = 50f;
    public float maxDistanceY = 10f;

    private LineRenderer lineRenderer;
    private Rigidbody ballRigidbody;

    private Vector3 initialPosition;
    private Vector3 endPosition;
    private Vector3 defaultShotDirection;
    private float lastDistanceX = 0f;
    private float lastDistanceY = 0f;
    private float shotPower = 0f;
    private float yRange = 0;
    public bool isPowerAdjustable = true;
    public bool hasShot = false;

    private void Start()
    {
        InitializeComponents();
        FindGameObjects();

    }

    private void Update()
    {
        if (livesManager.IsGameActive())
        {
            if (isPowerAdjustable)
            {
                HandleInput();
                UpdateAim();
                gameManager.SetDistanceFromGoals();
            }

            if (Input.GetKeyDown(KeyCode.Space) && !hasShot)
            {
                isPowerAdjustable = false;
                shotPower = powerSlider.value;
                player.isMoving = true;
                StartCoroutine(MovePlayerTowardsBall());

            }
        }
    }

    private IEnumerator MovePlayerTowardsBall()
    {
        while (player.isMoving)
        {
            player.MoveTowardsBall();
            yield return null;
        }
        Shoot();
    }

    private void InitializeComponents()
    {
        initialPosition = new Vector3(0, 0.5f, 0);
        lastDistanceX = 0f;
        lastDistanceY = 0;
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(0, new Vector3(0, 1, 0));
        defaultShotDirection = (lineRenderer.GetPosition(1) - lineRenderer.GetPosition(0)).normalized;
        powerSlider.onValueChanged.AddListener(HandlePowerAdjustment);
        ballRigidbody = ball.GetComponent<Rigidbody>();
    }

    private void FindGameObjects()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        player = GameObject.Find("Player").GetComponent<PlayerController>();
        livesManager = GameObject.Find("Lives").GetComponent<LivesManager>();
        ballController = GameObject.Find("Ball").GetComponent<BallController>();
    }


    private void HandleInput()
    {
        float horizontalInput = -Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        lastDistanceX = Mathf.Clamp(lastDistanceX + horizontalInput, -maxDistanceX, maxDistanceX);
        lastDistanceY = Mathf.Clamp(lastDistanceY + verticalInput, -maxDistanceY, maxDistanceY);

        if (lastDistanceY < yRange)
            lastDistanceY = 0;

        powerSlider.value = Mathf.PingPong(Time.time, 1);
    }

    private void UpdateAim()
    {
        endPosition = new Vector3(initialPosition.x + lastDistanceX, initialPosition.y + lastDistanceY, initialPosition.z);
        lineRenderer.SetPosition(0, endPosition);
        gameObject.SetActive(true);
    }

    private void Shoot()
    {
        gameObject.SetActive(false);
        float forceMagnitude = shotPower * 100;
        Vector3 forceDirection = (lineRenderer.GetPosition(0) - lineRenderer.GetPosition(1)).normalized;
        ballRigidbody.AddForce(forceDirection * forceMagnitude, ForceMode.Impulse);
        hasShot = true;
        ballController.rollingStartTime = Time.time;
    }

    private void HandlePowerAdjustment(float value)
    {
        if (!isPowerAdjustable)
        {
            powerSlider.value = shotPower;
        }
    }

    public void ResetAim()
    {

        lastDistanceX = 0;
        lastDistanceY = 0;
        UpdateAim();
        hasShot = false;
    }
}


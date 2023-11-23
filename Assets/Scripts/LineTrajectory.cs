using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LineTrajectory : MonoBehaviour
{
    public Transform ball;
    public Slider powerSlider;
    private GameManager manager;

    public float maxDistanceX = 10f;
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

    private void Start()
    {
        InitializeComponents();
        manager = GameObject.Find("GameManager").GetComponent<GameManager>();

    }

    private void Update()
    {
        if (isPowerAdjustable)
        {
            HandleInput();
            UpdateLinePosition();
            manager.SetDistanceFromGoals();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            isPowerAdjustable = false;
            shotPower = powerSlider.value;
            Shoot();
        }
    }

    private void InitializeComponents()
    {
        initialPosition = new Vector3(0, 0.5f, 0);
        lastDistanceX = 0f;
        lastDistanceY = 0;
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 2;
        defaultShotDirection = (lineRenderer.GetPosition(1) - lineRenderer.GetPosition(0)).normalized;
        powerSlider.onValueChanged.AddListener(HandlePowerAdjustment);
        ballRigidbody = ball.GetComponent<Rigidbody>();
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

    private void UpdateLinePosition()
    {
        endPosition = new Vector3(initialPosition.x + lastDistanceX, initialPosition.y + lastDistanceY, initialPosition.z);
        lineRenderer.SetPosition(0, endPosition);
    }

    private void Shoot()
    {
        float forceMagnitude = shotPower * 100;
        Vector3 forceDirection = (lineRenderer.GetPosition(0) - lineRenderer.GetPosition(1)).normalized;
        ballRigidbody.AddForce(forceDirection * forceMagnitude, ForceMode.Impulse);
    }

    private void HandlePowerAdjustment(float value)
    {
        if (!isPowerAdjustable)
        {
            powerSlider.value = shotPower;
        }
    }

    public void ResetLineAim()
    {
        // Reset the line to its default state
        lastDistanceX = 0f;
        lastDistanceY = 0f;
        powerSlider.value = 0f;
        UpdateLinePosition();  // Update the line position after resetting
    }
}

    
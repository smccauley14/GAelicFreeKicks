using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Transform ball;  // Reference to the first object.
    public Transform nets;  // Reference to the second object.
    public TextMeshProUGUI distanceUIDisplay;
    public TextMeshProUGUI scoreUIDisplay;
    private string distanceFromGoals = string.Empty;
    private int goalsScored = 0;
    private int pointsScored = 0;


    public void SetDistanceFromGoals()
    {
        // Calculate the distance between the two objects.
        float distance = Vector3.Distance(ball.position, nets.position);

        // Convert the distance to a string with two decimal places.
        distanceFromGoals = distance.ToString("F2");

        distanceUIDisplay.text = "Distance: " + distanceFromGoals + " m";
    }

    private void Update()
    {
        scoreUIDisplay.text = "Score: " + goalsScored + "-" + pointsScored;
    }

    public void AddPoint()
    {
        pointsScored++;
    }

    public void AddGoal()
    {
        goalsScored++;
    }
}

using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Transform ball;  // Reference to the first object.
    public Transform nets;  // Reference to the second object.
    public TextMeshProUGUI distanceUIDisplay;
    private string distanceFromGoals = string.Empty;


    public void SetDistanceFromGoals()
    {
        // Calculate the distance between the two objects.
        float distance = Vector3.Distance(ball.position, nets.position);

        // Convert the distance to a string with two decimal places.
        distanceFromGoals = distance.ToString("F2");

        distanceUIDisplay.text = "Distance: " + distanceFromGoals + " m";
    }
}

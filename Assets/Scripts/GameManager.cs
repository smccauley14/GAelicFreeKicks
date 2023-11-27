using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private Transform ball;  // Reference to the first object.
    [SerializeField]
    private Transform nets;  // Reference to the second object.
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private GameObject goalkeeper;
    [SerializeField]
    private Button restartButton;
    public TextMeshProUGUI distanceUIDisplay;
    public TextMeshProUGUI gameOverUIDisplay;
    private string distanceFromGoals = string.Empty;


    public void SetDistanceFromGoals()
    {
        // Calculate the distance between the two objects.
        float distance = Vector3.Distance(ball.position, nets.position);

        // Convert the distance to a string with two decimal places.
        distanceFromGoals = distance.ToString("F2");

        distanceUIDisplay.text = "Distance: " + distanceFromGoals + " m";
    }

    public void GameOver()
    {
        ball.gameObject.SetActive(false);
        nets.gameObject.SetActive(false);
        player.SetActive(false);
        goalkeeper.SetActive(false);
        gameOverUIDisplay.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}

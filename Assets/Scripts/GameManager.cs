using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Transform ball;
    [SerializeField] private Transform nets;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject goalkeeper;
    [SerializeField] private Button restartButton;

    public TextMeshProUGUI distanceUIDisplay;
    public TextMeshProUGUI gameOverUIDisplay;

    private void Start()
    {
        InitializeGame();
    }

    private void InitializeGame()
    {
        // Set initial UI states
        gameOverUIDisplay.gameObject.SetActive(false);
        restartButton.gameObject.SetActive(false);

        // Set initial game object states
        SetObjectsActiveState(true);
    }

    private void SetObjectsActiveState(bool isActive)
    {
        ball.gameObject.SetActive(isActive);
        nets.gameObject.SetActive(isActive);
        player.SetActive(isActive);
        goalkeeper.SetActive(isActive);
    }

    public void SetDistanceFromGoals()
    {
        float distance = Vector3.Distance(ball.position, nets.position);
        string distanceString = distance.ToString("F2");
        distanceUIDisplay.text = "Distance: " + distanceString + " m";
    }

    public void GameOver()
    {
        SetObjectsActiveState(false);
        gameOverUIDisplay.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}

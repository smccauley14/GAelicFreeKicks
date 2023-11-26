using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LivesManager : MonoBehaviour
{
    private ScoreManager scoreManager;
    public TextMeshProUGUI livesUIDisplay;
    private int lives = 3;
    // Start is called before the first frame update
    void Start()
    {
        scoreManager = GameObject.Find("ScoreManager").GetComponent<ScoreManager>();
        livesUIDisplay.text = "Lives: " + lives;
    }

    // Update is called once per frame
    public void CheckIfPlayerLosesALife()
    {
        if(scoreManager.hasPlayerScored == false)
        {
            lives--;
            livesUIDisplay.text = "Lives: " + lives;
        }
            

        Debug.Log("lives left: " + lives);
    }
}

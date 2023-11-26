using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private int goalsScored = 0;
    private int pointsScored = 0;
    public bool hasPlayerScored;
    public TextMeshProUGUI scoreUIDisplay;

    private void Start()
    {
        UpdateScoreDisplay();
    }
    public void AddPoint()
    {
        pointsScored++;
        UpdateScoreDisplay();
    }

    public void AddGoal()
    {
        goalsScored++;
        UpdateScoreDisplay();
    }

    private void UpdateScoreDisplay()
    {
        scoreUIDisplay.text = "Score: " + goalsScored + "-" + pointsScored;
    }
}

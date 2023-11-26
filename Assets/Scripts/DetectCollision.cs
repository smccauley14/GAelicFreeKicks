using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectCollision : MonoBehaviour
{
    private ScoreManager scoreManager;
    // Start is called before the first frame update
    void Start()
    {
        scoreManager = GameObject.Find("ScoreManager").GetComponent<ScoreManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (gameObject.CompareTag("Point"))
        {
            scoreManager.hasPlayerScored = true;
            scoreManager.AddPoint();
        }

        if (gameObject.CompareTag("Goal"))
        {
            scoreManager.hasPlayerScored = true;
            scoreManager.AddGoal();
        }
    }
}

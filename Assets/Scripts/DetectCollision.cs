using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectCollision : MonoBehaviour
{
    private GameManager manager;
    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("test");
        if (gameObject.CompareTag("Point"))
        {
            manager.AddPoint();
        }

        if (gameObject.CompareTag("Goal"))
        {
            manager.AddGoal();
        }
    }
}

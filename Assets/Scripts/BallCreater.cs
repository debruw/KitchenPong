using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallCreater : MonoBehaviour
{
    public GameObject ballPrefab;
    public float waitTime;
    float counter;

    // Start is called before the first frame update
    void Start()
    {
        counter = waitTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.isGameOver)
        {
            return;
        }
        counter -= Time.deltaTime;
        if(counter <= 0)
        {
            Instantiate(ballPrefab, transform);
            counter = waitTime;
        }
    }
}

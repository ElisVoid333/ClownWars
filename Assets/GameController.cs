using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    /*-- Timer Variables --*/
    public Text timeOutput;
    public float maxTime;
    private float timer;


    // Start is called before the first frame update
    void Start()
    {
        maxTime = 120f;     // 2 Minutes
    }

    // Update is called once per frame
    void Update()
    {
        timer = Time.deltaTime;

        float timeLeft = maxTime - timer;

        timeOutput.text = "Timer: " + timeLeft;
    }
}

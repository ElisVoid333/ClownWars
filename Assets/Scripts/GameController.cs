using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class GameController : MonoBehaviour
{
    public static GameController instance;
    public GameObject PCGUI;
    public CameraController cam;

    /*-- Timer Variables --*/
    public TMP_Text timeOutput;
    public TMP_Text timerOutput;

    public float maxTime;
    private float timer;

    // Score Variables
    public TMP_Text scoreOutput;
    private int score = 0;
    public int rocketClownScore = 1500;
    public int bombClownScore = 4000;
    private int nextRocket = 1;
    private int nextBomb = 1;

    // Clown Spawners
    public GameObject normalSpawner;
    public GameObject rocketSpawner;
    public GameObject bombSpawner;


    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        maxTime = 120f;     // 2 Minutes
        timer = 0f;

        score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (TestClownController.instance.CurrentClown != null)
        {
            Debug.Log("CLOWNFIRED!");
            timer -= Time.deltaTime;
            PCGUI.SetActive(false);
            cam.setTrackingClown(true);
        }
        else
        {
            PCGUI.SetActive(true);
            cam.setTrackingClown(false);
        }

        float timeLeft = maxTime + timer;

        //timerOutput.text = "" + (int)timeLeft;
        timeOutput.text = "Timer: " + (int)timeLeft;

        scoreOutput.text = "Score: " + score;

        if(Input.GetKeyDown(KeyCode.A)) rocketSpawner.GetComponent<ClownRackController>().SpawnClown();
        if(Input.GetKeyDown(KeyCode.S)) bombSpawner.GetComponent<ClownRackController>().SpawnClown();
        if (Input.GetKeyDown(KeyCode.W)) normalSpawner.GetComponent<ClownRackController>().SpawnClown();
    }

    public void incremeantScore(int scoreAdd)
    {
        score += scoreAdd;

        // Check to see if the score is sufficient for a bonus clown.
        
        if(score / rocketClownScore >= nextRocket)
        {
            rocketSpawner.GetComponent<ClownRackController>().SpawnClown();
            nextRocket++;
        }

        if (score / bombClownScore >= nextBomb)
        {
            bombSpawner.GetComponent<ClownRackController>().SpawnClown();
            nextBomb++;
        }
    }

    public void SpawnNormalClown()
    {
        normalSpawner.GetComponent<ClownRackController>().SpawnClown();
    }
    
    public void SpawnRocketClown()
    {
        rocketSpawner.GetComponent<ClownRackController>().SpawnClown();
    }
    public void SpawnBombClown()
    {
        bombSpawner.GetComponent<ClownRackController>().SpawnClown();
    }
}

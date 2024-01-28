using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class StartController : MonoBehaviour
{
    int score;
    public TMP_Text scoreOutput;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void GoToMainMenu()
    {
        ScoreController.instance.Despawn();
        SceneManager.LoadScene(0);
    }

    public void GoToMain()
    {
        SceneManager.LoadScene(1);
    }

    public void exitGame()
    {
        Application.Quit();
    }


}

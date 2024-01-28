using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class EndController : MonoBehaviour
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
        if (GameController.instance != null)
        {
            scoreOutput.text = "" + GameController.instance.score;
        }
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene(0);
        Destroy(GameController.instance.gameObject);
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

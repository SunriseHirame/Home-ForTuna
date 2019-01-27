using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using Hirame.Minerva;

public class Timer : MonoBehaviour
{
    //public GameObject MainMenuCanvas;
    //public GameObject GameOverCanvas;
    public Score PlayerScore;
    public TMP_Text EndScoreTextX;
    public TMP_Text EndScoreTextY;

    public GameEvent GameEnd;
    
    // Start is called before the first frame update
    void Start()
    {
        //MainMenuCanvas.gameObject.SetActive (false);
        //GameOverCanvas.gameObject.SetActive (false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeSinceLevelLoad >= 5)
        {
            //EndScoreTextX.text = "Score: " + PlayerScore.CurrentScore.ToString ();
            EndScoreTextX.text = PlayerScore.TunaCollected.ToString ();
            EndScoreTextY.text = PlayerScore.CurrentScore.ToString ();
            //GameOverCanvas.gameObject.SetActive (true);
            GameEnd.RaiseEvent ();
        }
    }
    
    public void hardRestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}

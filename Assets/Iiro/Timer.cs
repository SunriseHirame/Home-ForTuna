using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class Timer : MonoBehaviour
{
    public GameObject MainMenuCanvas;
    public GameObject GameOverCanvas;
    public Score PlayerScore;
    public TMP_Text EndScoreText;
    
    // Start is called before the first frame update
    void Start()
    {
        MainMenuCanvas.gameObject.SetActive (false);
        GameOverCanvas.gameObject.SetActive (false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeSinceLevelLoad >= 20)
        {
            EndScoreText.text = "Score: " + PlayerScore.CurrentScore.ToString ();
            GameOverCanvas.gameObject.SetActive (true);
            Invoke (nameof(hardRestartGame), 5);
        }
    }
    
    void hardRestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollisionDetection : MonoBehaviour
{
    public Text ScoreText;
    public Text PopUpText;
    public int Score = 0;
    
    
    private void OnTriggerEnter (Collider other)
    {
        if (other.gameObject.CompareTag (gameObject.tag))
        {
            Score += 20;
            ScoreText.text = "Score: " + Score.ToString();
        }
        else
        {
            Score += 10;
            ScoreText.text = "Score: " + Score.ToString();
        }
        
        PopUpText.gameObject.SetActive (true);    // PopUpTextTest gets deactivated after delay by its own script
        
    }
}

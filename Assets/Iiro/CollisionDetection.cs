using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollisionDetection : MonoBehaviour
{
    
    public Score PlayerScore;
    public Text PopUpText;
    
    private void OnTriggerEnter2D (Collider2D other)
    {
        if (other.gameObject.CompareTag (gameObject.tag))
        {
            int points_correct = 20;
            PlayerScore.CurrentScore += points_correct;
            PopUpText.text = points_correct.ToString ();
            PopUpText.gameObject.SetActive (true);    // This gets disabled after delay by another script called DisableAfterDelay
        }
        else
        {
            int points_incorrect = 10;
            if(!other.gameObject.CompareTag("NotFish"))
                PlayerScore.CurrentScore += points_incorrect;
            PopUpText.text = points_incorrect.ToString ();
            PopUpText.gameObject.SetActive (true);    // This gets disabled after delay by another script called DisableAfterDelay
        }
    }
}

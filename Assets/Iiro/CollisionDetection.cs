using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CollisionDetection : MonoBehaviour
{
    
    public Score PlayerScore;
    public TMP_Text PopUpText;
    
    private void OnTriggerEnter2D (Collider2D other)
    {
        if (other.gameObject.CompareTag (gameObject.tag))
        {
            int points_correct = 20;
            PlayerScore.CurrentScore += points_correct;
            PlayerScore.TunaCollected += 1;
            PopUpText.text = points_correct.ToString ();
            PopUpText.gameObject.SetActive (true);    // This gets disabled after certain delay by another script called DisableAfterDelay
        }
        else if(!other.gameObject.CompareTag("NotFish"))
        {
            int points_incorrect = 10;
            PlayerScore.CurrentScore += points_incorrect;
            PlayerScore.TunaCollected += 1;
            PopUpText.text = points_incorrect.ToString ();
            PopUpText.gameObject.SetActive (true);    // This gets disabled after certain delay by another script called DisableAfterDelay
        }
    }
}

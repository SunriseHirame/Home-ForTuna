using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollisionDetection : MonoBehaviour
{
    
    public Score PlayerScore;
    
    private void OnTriggerEnter2D (Collider2D other)
    {
        if (other.gameObject.CompareTag (gameObject.tag))
        {
            PlayerScore.CurrentScore += 20;
        }
        else
        {
            if(!other.gameObject.CompareTag("NotFish"))
                PlayerScore.CurrentScore += 10;
        }
    }
}

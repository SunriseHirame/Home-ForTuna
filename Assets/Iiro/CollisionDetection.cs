using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollisionDetection : MonoBehaviour
{
    public Text TextObject;
    public int Score = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter (Collider other)
    {
        if (other.gameObject.CompareTag (gameObject.tag))
        {
            Score += 20;
            TextObject.text = "Score: " + Score.ToString();
        }
        else
        {
            Score += 10;
            TextObject.text = "Score: " + Score.ToString();
        }
    }
}

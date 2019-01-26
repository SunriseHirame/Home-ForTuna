using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour {
    public int CurrentScore = 0;
    public Text TextObject;

    private void Update() {
        TextObject.text = "Score: " + CurrentScore.ToString();
    }
}

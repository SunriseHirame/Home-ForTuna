using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Score : MonoBehaviour {
    public int CurrentScore = 0;
    public TMP_Text TextObject;

    private void Update() {
        TextObject.text = "Score: " + CurrentScore.ToString();
    }
}

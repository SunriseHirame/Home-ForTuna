using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ButtonConditions : MonoBehaviour
{
    public Button MenuButton;
    public TMP_InputField InputField;

    public void CheckCondition()
    {
        if (InputField.text != null)
        {
            MenuButton.interactable = true;
        }
        else
        {
            MenuButton.interactable = false;
        }
    }
}

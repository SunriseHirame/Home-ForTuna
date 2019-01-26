using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableAfterDelay : MonoBehaviour
{
    public float Delay;

    private void OnEnable ()
    {
        Invoke (nameof(Disable), Delay);
    }

    private void Disable ()
    {
        gameObject.SetActive (false);
    }
}

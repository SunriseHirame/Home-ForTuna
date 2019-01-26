using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Splasher : MonoBehaviour {
    private bool _firstSplash;
    public UnityEvent Splash;

    private void OnTriggerExit(Collider other) {
        if (!_firstSplash && other.gameObject.tag.Equals("Water")) {
            transform.rotation = Quaternion.Euler(-90,0,0);
            Splash.Invoke();
            _firstSplash = true;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ROllingPropellers : MonoBehaviour {
    public GameObject leftprop;
    public GameObject rightprop;
    public UnityEvent left;
    public UnityEvent right;
    public UnityEvent none;
    void Update()
    {
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) {
            leftprop.transform.Rotate(Vector3.right,10);
            left.Invoke();
        } else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) {
            rightprop.transform.Rotate(Vector3.right,10);
            right.Invoke();
        }
        else {
            none.Invoke();
        }
    }
}

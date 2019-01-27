using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ROllingPropellers : MonoBehaviour {
    public GameObject leftprop;
    public GameObject rightprop;
    void Update()
    {
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) {
            leftprop.transform.Rotate(Vector3.right,10);
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) {
            rightprop.transform.Rotate(Vector3.right,10);
        }
    }
}

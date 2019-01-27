using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotator : MonoBehaviour {
    public Vector3 axis;

    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(transform.position,axis,10);
    }
}

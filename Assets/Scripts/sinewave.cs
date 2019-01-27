using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sinewave : MonoBehaviour {
    public float timescale = 1;
    public float deltaheight = 1;
    private float startY;
    // Start is called before the first frame update
    void Start() {
        startY = transform.position.y;
    }

    // Update is called once per frame
    void Update() {
        float delta = Mathf.Sin(Time.realtimeSinceStartup * timescale) * deltaheight;
        transform.position = new Vector3(transform.position.x, startY + delta, transform.position.z);
    }
}

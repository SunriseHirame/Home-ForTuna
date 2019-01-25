using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class FishController : MonoBehaviour {
    public Rigidbody Mid;

    public float Force = 1;

    private bool _isBounced = false;
    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, Vector3.zero) > 20) {
            Destroy(this.gameObject);
        }
        transform.Translate(0.1f,0,0,Space.Self);
        if (Random.value < 0.01f && !_isBounced) {
            Bounce();
        }
    }

    private void Bounce() {
        foreach (Rigidbody rb in transform.GetComponentsInChildren<Rigidbody>()) {
            rb.isKinematic = false;
        }
        Vector3 Force = new Vector3(Random.Range(-1,1),Random.Range(0.5f,1),0);
        Mid.AddForce(Force.normalized*this.Force,ForceMode.Impulse);
        _isBounced = true;
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class FishController : MonoBehaviour {
    public Rigidbody2D Mid;

    public float Force = 1;

    private bool _isBounced = false;
    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, Vector3.zero) > 20) {
            Destroy(this.gameObject);
        }
        
        if(!_isBounced)
            transform.Translate(0.1f,0,0,Space.Self);
        
        if (Random.value < 0.01f && !_isBounced) {
            Bounce();
        }
    }

    private void Bounce() {
        foreach (Rigidbody2D rb in transform.GetComponentsInChildren<Rigidbody2D>()) {
            rb.isKinematic = false;
        }
        Vector3 Force = new Vector3(0,1,0);
        Mid.AddForce(Force.normalized*this.Force,ForceMode2D.Impulse);
        _isBounced = true;
    }
}

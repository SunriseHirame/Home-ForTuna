using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class FishController : MonoBehaviour {
    public Rigidbody2D Mid;

    public float Force = 1;

    private bool _isBounced = false;

    private float _speed = 0.1f;

    public GameObject animated;
    // Update is called once per frame
    private void Start() {
        _speed = Random.Range(0.05f, 0.2f);
    }

    void Update()
    {
        if (Vector3.Distance(transform.position, Vector3.zero) > 20) {
            Destroy(this.gameObject);
        }
        
        if(!_isBounced)
            transform.Translate(_speed,0,0,Space.Self);
        
        if (Random.value < 0.01f && !_isBounced) {
            Bounce();
        }
    }

    private void Bounce() {
        animated.SetActive(false);
        transform.GetComponent<MeshRenderer>().enabled = true;
        transform.GetChild(0).GetComponent<MeshRenderer>().enabled = true;
        transform.GetChild(1).GetComponent<MeshRenderer>().enabled = true;
        foreach (Rigidbody2D rb in transform.GetComponentsInChildren<Rigidbody2D>()) {
            rb.isKinematic = false;
        }
        Vector3 Force = new Vector3(Random.Range(-1.0f,1.0f),1,0);
        Mid.AddForce(Force.normalized*this.Force,ForceMode2D.Impulse);
        _isBounced = true;
    }
}

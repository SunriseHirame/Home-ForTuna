using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomScale : MonoBehaviour
{
    // Start is called before the first frame update
    void Start() {
        float scale = Random.Range(1f, 1.5f);
        transform.localScale = transform.localScale * scale;
    }
}

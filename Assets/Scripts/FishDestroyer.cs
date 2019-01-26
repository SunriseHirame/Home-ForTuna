using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishDestroyer : MonoBehaviour
{
    private void Update() {
        if (Vector3.Distance(transform.position, Vector3.zero) > 20) {
            Destroy();
        }
    }

    public void Destroy() {
        Destroy(transform.parent.gameObject);
    }
}

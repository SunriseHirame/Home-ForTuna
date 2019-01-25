using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishSpawner : MonoBehaviour {
    
    public float SpawnDelay = 3;
    public GameObject[] Fish = new GameObject[0];
    private float _timer = 0;
    
    private void Update() {
        _timer += Time.deltaTime;
        if (_timer >= SpawnDelay) {
            _timer = 0;
            SpawnFish();
        }
    }

    private void SpawnFish() {
        GameObject f = Fish[Random.Range(0, Fish.Length)];
        Vector3 pos = Vector3.right * (Random.value < 0.5f ? -12 : 12);
        Quaternion rot = Quaternion.identity;
        if (pos.x > 0) {
            rot = Quaternion.Euler(0,0,180);
        }
        Instantiate(f, pos, rot);
        
    }
}

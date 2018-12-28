using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

    public GameObject objectToSpawn;
    public float spawnRate;
    public int max;
    public bool finished;
    private float spawnTimer;
    private int spawned;

    private void Start() {
        finished = false;
    }

    void Update() {
        spawnTimer -= Time.deltaTime;
        if (spawnTimer <= 0 && (spawned < max || max <= 0)) {
            Instantiate(objectToSpawn, transform.position, Quaternion.identity);
            spawnTimer = spawnRate;
            spawned++;
            if (spawned == max) finished = true;
        }
    }

}

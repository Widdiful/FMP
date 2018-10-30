using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

    public GameObject objectToSpawn;
    public float spawnRate;
    public int max;
    private float spawnTimer;
    private int spawned;

    void Update() {
        spawnTimer -= Time.deltaTime;
        if (spawnTimer <= 0 && (spawned < max || max <= 0)) {
            Instantiate(objectToSpawn, transform.position, Quaternion.identity);
            spawnTimer = spawnRate;
            spawned++;
        }
    }

}

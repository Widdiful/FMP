using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMultiple : MonoBehaviour {

    public List<GameObject> objectsToSpawn;
    public float spawnRate;
    public int max;
    public bool randomFirst;
    public bool finished;
    private float spawnTimer;
    private int spawned;
    private int spawnIndex;

    private void Start() {
        finished = false;
        if (randomFirst)
            spawnIndex = Random.Range(0, objectsToSpawn.Count);
    }

    void Update() {
        spawnTimer -= Time.deltaTime;
        if (spawnTimer <= 0 && (spawned < max || max <= 0)) {
            Instantiate(objectsToSpawn[spawnIndex], transform.position, Quaternion.identity);
            spawnTimer = spawnRate;
            spawned++;
            if (spawned == max) finished = true;
            spawnIndex = (spawnIndex + 1) % objectsToSpawn.Count;
        }
    }
}

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

    private List<GameObject> droplets = new List<GameObject>();

    private void Start() {
        finished = false;

        for (int i = 0; i < max; i++) {
            GameObject newDroplet = Instantiate(objectToSpawn, transform.position, Quaternion.identity);
            newDroplet.SetActive(false);
            droplets.Add(newDroplet);
        }

        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 20;
    }

    void Update() {
        spawnTimer -= Time.deltaTime;
        if (spawnTimer <= 0 && (spawned < max || max <= 0)) {
            droplets[spawned].transform.position = transform.position;
            droplets[spawned].SetActive(true);
            spawnTimer += spawnRate;
            spawned++;
            if (spawned == max) finished = true;
        }
    }

}

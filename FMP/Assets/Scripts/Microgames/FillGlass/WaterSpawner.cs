using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterSpawner : MonoBehaviour {

    public float moveRange;
    public float speed;
    public float clearPercentage;
    private Vector3 startPos;
    private float timer = 0;
    Spawner spawner;
    bool completed;

    private void Start() {
        startPos = transform.position;
        //timer = Random.Range(0.0f, 1.0f);
        spawner = FindObjectOfType<Spawner>();
    }

    private void Update() {
        timer += Time.deltaTime;
        speed += Time.deltaTime * 0.1f;
        transform.position = startPos + new Vector3(Mathf.Sin(timer * speed) * moveRange, 0, 0);
        if (spawner.finished && !completed) {
            completed = true;
            StartCoroutine(delayedEnd());
        }
    }

    IEnumerator delayedEnd() {
        yield return new WaitForSeconds(0.5f);
        if ((float)FindObjectsOfType<WaterDroplet>().Length / (float)spawner.max >= clearPercentage)
            FindObjectOfType<gameManager>().CompleteGame();
        else
            FindObjectOfType<gameManager>().FailGame();
    }
}

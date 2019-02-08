using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeftRightSin : MonoBehaviour {

    public float moveRange;
    public float speed;
    private Vector3 startPos;
    private float timer = 0;

    private void Start() {
        startPos = transform.position;
        timer = Random.Range(0.0f, 1.0f);
    }

    private void Update() {
        timer += Time.deltaTime;
        speed += Time.deltaTime * 0.1f;
        transform.position = startPos + new Vector3(Mathf.Sin(timer * speed) * moveRange, 0, 0);
    }
}

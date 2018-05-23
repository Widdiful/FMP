using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour {

    private Vector3 startPos;
    private float speed;
    private Transform mid;
    private Transform right;

    void Start () {
        foreach (Transform child in transform) {
            if (child.name == "CloudMid") {
                mid = child;
            }
            else if (child.name == "CloudRight") {
                right = child;
            }
        }
        Initialise();
	}

    void Initialise() {
        startPos = transform.localPosition;
        speed = Random.Range(0.005f, 0.05f);
        float randScale = Random.Range(5.0f, 20.0f);
        transform.localScale = new Vector3(randScale, randScale, randScale);
        transform.localPosition = new Vector3(startPos.x, startPos.y, Random.Range(10.0f, 12.0f));

        mid.localScale = new Vector3(Random.Range(1.0f, 5.0f), 1, 1);
        mid.localPosition = new Vector3(mid.localScale.x * 0.08f, 0, 0);
        right.localPosition = new Vector3(mid.localPosition.x * 2.0f, 0, 0);
    }

	void Update () {
        transform.Translate(new Vector3(-speed, 0, 0), Space.Self);

        if (Vector3.Distance(transform.localPosition - right.localPosition, startPos) > Camera.main.orthographicSize * 6) {
            transform.localPosition = startPos;
            Initialise();
        }
    }
}

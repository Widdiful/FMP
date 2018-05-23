using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balloon : MonoBehaviour {

    private Vector3 startPos;
    private float speed;

	void Start () {
        Initialise();
	}

    void Initialise() {
        startPos = transform.localPosition;
        speed = Random.Range(0.01f, 0.1f);
        float randScale = Random.Range(0.5f, 5.0f);
        transform.localScale = new Vector3(randScale, randScale, randScale);
        transform.localPosition = new Vector3(startPos.x, startPos.y, Random.Range(9.0f, 11.0f));

        if (GetComponent<SpriteRenderer>()) {
            GetComponent<SpriteRenderer>().color = Random.ColorHSV(0, 1, 0.5f, 0.75f, 1, 1);
        }
    }
	
	void Update () {
        transform.Translate(new Vector3(0, speed, 0), Space.Self);
        if (Vector3.Distance(transform.localPosition, startPos) > Camera.main.orthographicSize * 4) {
            transform.localPosition = startPos;
            Initialise();
        }
	}
}

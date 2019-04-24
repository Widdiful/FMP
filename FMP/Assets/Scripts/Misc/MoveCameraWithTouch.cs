using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCameraWithTouch : MonoBehaviour {

    public float minX, maxX;
    public bool useOrthographic;
    float orthographicWidth;
    public float speed;

    private void Start() {
        float height = Camera.main.orthographicSize;
        orthographicWidth = height * Screen.width / Screen.height;
    }

    void Update () {
		if (Input.touchCount > 0) {
            transform.Translate((-Input.touches[0].deltaPosition.x / Screen.width)* speed, 0, 0);
            transform.position = new Vector3(Mathf.Clamp(transform.position.x, minX + orthographicWidth, maxX - orthographicWidth), transform.position.y, transform.position.z);
        }
	}
}

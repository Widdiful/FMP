using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccelerometerCamera : MonoBehaviour {

    public float speed;

	void Update () {
        transform.Translate(Input.acceleration.x * Time.deltaTime, 0, 0);
	}
}

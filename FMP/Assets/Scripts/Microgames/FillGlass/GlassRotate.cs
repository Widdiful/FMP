using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlassRotate : MonoBehaviour {

    private void Start() {
        Input.gyro.enabled = false;
        Input.gyro.enabled = true;
    }

    private void Update() {
        transform.rotation = Quaternion.Euler(0, 0, (Input.gyro.attitude.z + 0.5f) * 100);
        transform.position = new Vector3((Input.gyro.attitude.z + 0.5f) * -5, transform.position.y, 0);
    }
}

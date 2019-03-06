using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollBallTable : MonoBehaviour {

    public float rotateScale;

	void Start () {
        Input.gyro.enabled = false;
        Input.gyro.enabled = true;
    }
	
	void Update () {
        transform.localRotation = Quaternion.Euler(Input.gyro.gravity.x * rotateScale, 0, Input.gyro.gravity.y * rotateScale);
	}
}

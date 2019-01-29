﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericSteering : MonoBehaviour {

    public float rotateSpeed;
    public float moveSpeed;

	void Start () {
        Input.gyro.enabled = false;
        Input.gyro.enabled = true;
    }
	
	void Update () {
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, (Input.gyro.gravity.x) * -rotateSpeed);
        transform.position = new Vector3((Input.gyro.gravity.x) * moveSpeed, transform.position.y, transform.position.z);
    }
}

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt : MonoBehaviour {

    public Transform target;

	void Update () {
		if (target) {
            transform.up = target.position - transform.position;
        }
	}
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockRot : MonoBehaviour {

    Quaternion startRot;

	void Start () {
        startRot = transform.rotation;
	}
	
	void Update () {
        transform.rotation = startRot;
	}
}

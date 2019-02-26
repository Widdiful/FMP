using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttachToObject : MonoBehaviour {

    public Vector3 offset;
    public Transform target;

	void LateUpdate () {
        transform.position = target.position + offset;
	}
}

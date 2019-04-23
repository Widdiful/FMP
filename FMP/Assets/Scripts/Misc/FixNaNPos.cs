using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixNaNPos : MonoBehaviour {

    Vector3 previousPosition;
    Rigidbody rb;

	void Start () {
        rb = GetComponent<Rigidbody>();
        previousPosition = transform.position;
    }
	
	void Update () {
        if (float.IsNaN(transform.position.x)) {
            print("nan fix");
            transform.position = previousPosition;
            if (rb) rb.velocity = Vector3.zero;
        }
        else {
            previousPosition = transform.position;
        }
    }
}

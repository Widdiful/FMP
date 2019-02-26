using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StretchToPos : MonoBehaviour {

    public Transform target;

	void LateUpdate () {
        float distance = Vector3.Distance(transform.position, target.position);
        transform.localScale = new Vector3(transform.localScale.x, distance, transform.localScale.z);
	}
}

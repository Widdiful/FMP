using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt : MonoBehaviour {

    public Transform target;
    public bool use3d;

	void LateUpdate () {
		if (target) {
            if (use3d) {
                transform.LookAt(target);
            }
            else {
                transform.up = target.position - transform.position;
            }
        }
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIFollowTarget : MonoBehaviour {

    public Transform target;

	void Update () {
        transform.position = Camera.main.WorldToScreenPoint(target.position);
	}
}

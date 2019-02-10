using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DistanceToTarget : MonoBehaviour {

    public Transform target;
    public Text distanceText;

	void Update () {
        distanceText.text = Vector3.Distance(transform.position, target.position).ToString("F2");
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceRandomlyOnSphere : MonoBehaviour {

    public float radius;

	void Start () {
        transform.position = Random.onUnitSphere * radius;
        transform.LookAt(Vector3.zero);
        //transform.rotation = Quaternion.Euler(-transform.rotation.eulerAngles);
	}
}

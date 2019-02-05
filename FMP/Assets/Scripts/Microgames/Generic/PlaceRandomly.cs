using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceRandomly : MonoBehaviour {

    public Vector3 minPos, maxPos;

	void Start () {
        transform.position = new Vector3(Random.Range(minPos.x, maxPos.x), Random.Range(minPos.y, maxPos.y), Random.Range(minPos.z, maxPos.z));
	}
}

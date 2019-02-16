using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceRandomly : MonoBehaviour {

    public Vector3 minPos, maxPos;
    public bool x = true;
    public bool y = true;
    public bool z = true;

	void Start () {
        if (x)
            transform.position = new Vector3(Random.Range(minPos.x, maxPos.x), transform.position.y, transform.position.z);
        if (y)
            transform.position = new Vector3(transform.position.x, Random.Range(minPos.y, maxPos.y), transform.position.z);
        if (z)
            transform.position = new Vector3(transform.position.x, transform.position.y, Random.Range(minPos.z, maxPos.z));
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceRandomly : MonoBehaviour {

    public Vector3 minPos, maxPos;
    public bool x = true;
    public bool y = true;
    public bool z = true;
    public bool localPosition;

	void Awake () {
        if (!localPosition) {
            if (x)
                transform.position = new Vector3(Random.Range(minPos.x, maxPos.x), transform.position.y, transform.position.z);
            if (y)
                transform.position = new Vector3(transform.position.x, Random.Range(minPos.y, maxPos.y), transform.position.z);
            if (z)
                transform.position = new Vector3(transform.position.x, transform.position.y, Random.Range(minPos.z, maxPos.z));
        }
        else {
            if (x)
                transform.localPosition = new Vector3(Random.Range(minPos.x, maxPos.x), transform.localPosition.y, transform.localPosition.z);
            if (y)
                transform.localPosition = new Vector3(transform.localPosition.x, Random.Range(minPos.y, maxPos.y), transform.localPosition.z);
            if (z)
                transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, Random.Range(minPos.z, maxPos.z));
        }
	}
}

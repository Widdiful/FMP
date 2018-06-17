using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockPos : MonoBehaviour {

    public bool X;
    public bool Y;
    public bool Z;

    private Vector3 startPos;

	void Start () {
        startPos = gameObject.transform.position;
	}

	void Update () {
        if (X) transform.position = new Vector3(startPos.x, transform.position.y, transform.position.z);
        if (Y) transform.position = new Vector3(transform.position.x, startPos.y, transform.position.z);
        if (Z) transform.position = new Vector3(transform.position.x, transform.position.y, startPos.z);
    }
}

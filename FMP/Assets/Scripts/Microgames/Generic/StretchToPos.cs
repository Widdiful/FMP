using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StretchToPos : MonoBehaviour {

    public Transform target;
    public Vector3 offset;
    public bool x;
    public bool y = true;
    public bool z;
    public bool singleDirection;
    public bool constant = true;

    private void Start() {
        ChangeScale();
    }

    void LateUpdate () {
        if (constant)
            ChangeScale();
    }

    private void ChangeScale() {
        if (!singleDirection) {
            float distance = Vector3.Distance(transform.position, target.position);

            if (x)
                transform.localScale = new Vector3(distance - offset.x, transform.localScale.y, transform.localScale.z);
            if (y)
                transform.localScale = new Vector3(transform.localScale.x, distance - offset.y, transform.localScale.z);
            if (z)
                transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, distance - offset.z);
        }
        else {
            Vector3 distance = new Vector3(target.position.x - transform.position.x, target.position.y - transform.position.y, target.position.z - transform.position.z);
            if (x)
                transform.localScale = new Vector3(distance.x - offset.x, transform.localScale.y, transform.localScale.z);
            if (y)
                transform.localScale = new Vector3(transform.localScale.x, distance.y - offset.y, transform.localScale.z);
            if (z)
                transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, distance.z - offset.z);
        }
    }
}

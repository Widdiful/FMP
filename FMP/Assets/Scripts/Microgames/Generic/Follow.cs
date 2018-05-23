using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour {

    public Vector2 offset;
    public float speed;
    public Transform target;

    void FixedUpdate() {
        if (target) {
            transform.position = Vector3.Lerp(transform.position, new Vector3(target.position.x + offset.x, target.position.y + offset.y, transform.position.z), speed);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow3D : MonoBehaviour {

    public Transform target;
    public Vector3 offset;
    public float speed;

	void FixedUpdate () {
        transform.position = Vector3.Lerp(transform.position, new Vector3(target.position.x + offset.x, target.position.y + offset.y, target.position.z + offset.z), speed * Time.deltaTime);
    }
}

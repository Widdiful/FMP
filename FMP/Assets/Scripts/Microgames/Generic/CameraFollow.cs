using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    public Transform target;
    public bool clampPos;
    public Vector2 minPos;
    public Vector2 maxPos;
    private Camera cam;

    void Start() {
        cam = GetComponent<Camera>();
    }

    void FixedUpdate() {
        if (target) {
            transform.position = Vector3.Lerp(transform.position, new Vector3(target.position.x, target.position.y, transform.position.z), 0.2f);
            if (clampPos) {
                transform.position = new Vector3(Mathf.Clamp(transform.position.x, minPos.x + cam.orthographicSize * cam.aspect, maxPos.x - cam.orthographicSize * cam.aspect),
                                                 Mathf.Clamp(transform.position.y, minPos.y + cam.orthographicSize, maxPos.y - cam.orthographicSize),
                                                 transform.position.z);
            }
        }

        // alt method
        //transform.position = Vector3.MoveTowards(transform.position, new Vector3(target.position.x, target.position.y, transform.position.z), 1);
    }
}
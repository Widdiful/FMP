using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    public Vector2 offset;
    public Transform target;
    public bool clampPos;
    public bool clampLocal;
    public bool useOrthographicSize = true;
    public Vector2 minPos;
    public Vector2 maxPos;
    public float speed = 0.2f;
    private Camera cam;

    void Start() {
        cam = GetComponent<Camera>();
    }

    void FixedUpdate() {
        if (target) {
            transform.position = Vector3.Lerp(transform.position, new Vector3(target.position.x + offset.x, target.position.y + offset.y, transform.position.z), speed);
            if (clampPos) {
                if (cam && useOrthographicSize)
                    transform.position = new Vector3(Mathf.Clamp(transform.position.x, minPos.x + cam.orthographicSize * cam.aspect, maxPos.x - cam.orthographicSize * cam.aspect),
                                                 Mathf.Clamp(transform.position.y, minPos.y + cam.orthographicSize, maxPos.y - cam.orthographicSize),
                                                 transform.position.z);
                else
                    transform.position = new Vector3(Mathf.Clamp(transform.position.x, minPos.x, maxPos.x),
                                                 Mathf.Clamp(transform.position.y, minPos.y, maxPos.y),
                                                 transform.position.z);
            }

            else if (clampLocal) {
                transform.localPosition = new Vector3(Mathf.Clamp(transform.localPosition.x, minPos.x, maxPos.x),
                                                 Mathf.Clamp(transform.localPosition.y, minPos.y, maxPos.y),
                                                 transform.localPosition.z);
            }
        }

        // alt method
        //transform.position = Vector3.MoveTowards(transform.position, new Vector3(target.position.x, target.position.y, transform.position.z), 1);
    }
}
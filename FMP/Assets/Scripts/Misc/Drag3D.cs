using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drag3D : MonoBehaviour {

    bool grabbed;
    Vector3 grabOffset;
    Rigidbody rb;

    private void Start() {
        rb = GetComponent<Rigidbody>();
    }

    void Update () {
        if (Input.touchCount > 0) {
            RaycastHit hit;
            bool ray = Physics.Raycast(Camera.main.ScreenToWorldPoint(Input.touches[0].position), Vector2.zero, out hit);

            if (Physics.Raycast(Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position), Camera.main.transform.forward, out hit) && hit.transform == rb.transform) {
                if (!grabbed) {
                    grabbed = true;
                    grabOffset = transform.position - Camera.main.ScreenToWorldPoint(Input.touches[0].position);
                    grabOffset.z = transform.position.z;
                }
            }
        }
        else if (grabbed) {
            grabbed = false;
        }
    }

    private void FixedUpdate() {
        if (Input.touchCount > 0 && grabbed) {
            Vector3 dragPos = Camera.main.ScreenToWorldPoint(Input.touches[0].position) + grabOffset;
            dragPos.z = transform.position.z;
            rb.velocity = (dragPos - transform.position) / Time.deltaTime;
            //transform.position = dragPos;
        }
    }
}

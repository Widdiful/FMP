using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drag : MonoBehaviour {

    public bool useVelocity;
    bool grabbed = false;
    Rigidbody2D rb;
    Vector3 grabOffset;

    private void Start() {
        rb = GetComponent<Rigidbody2D>();
    }


    void Update () {
        if (Input.touchCount > 0) {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.touches[0].position), Vector2.zero);

            if (hit && hit.transform == transform) {
                if (!grabbed) {
                    grabbed = true;
                    grabOffset = transform.position - Camera.main.ScreenToWorldPoint(Input.touches[0].position);
                    grabOffset.z = transform.position.z;
                }
            }
            if (grabbed) {
                Vector3 dragPos = Camera.main.ScreenToWorldPoint(Input.touches[0].position) + grabOffset;
                dragPos.z = transform.position.z;
                if (useVelocity) {
                    rb.velocity = (dragPos - transform.position) / Time.deltaTime;
                }
                else {
                    transform.position = dragPos;

                    if (rb) {
                        rb.velocity = Vector3.zero;
                    }
                }
            }
            else if (useVelocity) {
                if (rb) rb.velocity = Vector3.zero;
            }
        }

        else {
            grabbed = false;
            if (useVelocity) rb.velocity = Vector3.zero;
        }
    }
}

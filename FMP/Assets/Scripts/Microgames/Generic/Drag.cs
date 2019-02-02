using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drag : MonoBehaviour {

    bool grabbed = false;
    Rigidbody2D rb;

    private void Start() {
        rb = GetComponent<Rigidbody2D>();
    }


    void Update () {
        if (Input.touchCount > 0) {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.touches[0].position), Vector2.zero);

            if (hit && hit.transform == transform) {
                grabbed = true;
            }
            if (grabbed) {
                Vector3 dragPos = Camera.main.ScreenToWorldPoint(Input.touches[0].position);
                dragPos.z = transform.position.z;
                transform.position = dragPos;

                if (rb) {
                    rb.velocity = Vector3.zero;
                }
            }
        }

        else {
            grabbed = false;
        }
    }
}

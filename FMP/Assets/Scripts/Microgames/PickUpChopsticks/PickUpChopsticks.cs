using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpChopsticks : MonoBehaviour {

    public bool grabbed;
    public bool doubleGrabbed;
    float gravityScale;
    Rigidbody2D rb;

    private void Start() {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update() {
        if (Input.touchCount > 0) {

            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.touches[0].position), Vector2.zero);
            if (hit && hit.transform == rb.transform) {
                if (!grabbed) {
                    grabbed = true;
                    doubleGrabbed = false;
                }
            }

            if (Input.touchCount > 1) {
                hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.touches[1].position), Vector2.zero);
                if (hit && hit.transform == rb.transform) {
                    if (!doubleGrabbed) {
                        grabbed = false;
                        doubleGrabbed = true;
                    }
                }
            }
        }
    }

    private void FixedUpdate() {
        if (Input.touchCount == 1 && grabbed) {
            Vector3 dragPos = Camera.main.ScreenToWorldPoint(Input.touches[0].position);
            dragPos.y = transform.position.y;
            dragPos.z = transform.position.z;
            rb.velocity = (dragPos - transform.position) * Time.deltaTime;
            transform.position = dragPos;
        }
        else if (Input.touchCount > 1 && doubleGrabbed) {
            Vector3 dragPos = Camera.main.ScreenToWorldPoint(Input.touches[0].position);
            dragPos.z = transform.position.z;
            rb.velocity = (dragPos - transform.position) * Time.deltaTime;
            transform.position = dragPos;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrushCan : MonoBehaviour {

    public MeshFilter meshFilter;
    public Mesh crushedMesh;
    public Rigidbody rb;
    public float gravityScale;
    bool hasBeenFar;
    bool crushed;
    bool grabbed;
    bool letGo;
    Vector3 grabOffset;
    Rigidbody thisRb;
    AudioSource audioSource;

    private void Start() {
        thisRb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Update() {
        if (!crushed) {
            if (!ProximitySensor.instance.nearby && !hasBeenFar) {
                hasBeenFar = true;
            }
            if (ProximitySensor.instance.nearby && hasBeenFar) {
                crushed = true;
                meshFilter.mesh = crushedMesh;
                audioSource.Play();
            }
        }

        else {
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
                letGo = true;
            }
        }

    }

    void FixedUpdate() {
        if (Input.touchCount > 0 && grabbed) {
            Vector3 dragPos = Camera.main.ScreenToWorldPoint(Input.touches[0].position) + grabOffset;
            dragPos.z = transform.position.z;
            rb.velocity = (dragPos - transform.position) / Time.deltaTime;
            transform.position = dragPos;
        }

        if (letGo) {
            Vector3 gravity = -9.81f * gravityScale * Vector3.up;
            rb.AddForce(gravity, ForceMode.Acceleration);
        }
    }
}

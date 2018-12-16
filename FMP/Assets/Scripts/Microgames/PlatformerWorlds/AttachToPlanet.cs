using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class AttachToPlanet : MonoBehaviour {

    public float attractRadius;
    public float moveSpeed;
    public float rotateSpeed;
    public float jumpHeight;
    private Rigidbody2D rb;
    private Vector2 closestPoint;
    private Transform closestObject;
    private CameraFollow cameraFollow;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
        cameraFollow = FindObjectOfType<CameraFollow>();
    }

	void Update () {
		foreach(Collider2D other in Physics2D.OverlapCircleAll(transform.position, attractRadius)){
            if (Vector2.Distance(other.transform.position, transform.position) < Vector2.Distance(closestPoint, transform.position) && other.gameObject != gameObject) {
                closestPoint = other.transform.position;
                closestObject = other.transform;
            }
        }

        Vector2 position = transform.position;
        transform.up = Vector2.Lerp(transform.up, -(closestPoint - position), rotateSpeed * Time.deltaTime);

        transform.Translate(moveSpeed * Time.deltaTime, 0, 0);

        cameraFollow.target = closestObject;

        bool Jump = CrossPlatformInputManager.GetButtonDown("Jump") || Input.GetButtonDown("Jump");

        if (Jump) {
            rb.velocity *= transform.up * jumpHeight;
        }
    }
}

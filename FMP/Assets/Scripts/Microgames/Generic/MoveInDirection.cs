using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveInDirection : MonoBehaviour {

    public Vector3 direction;
    public bool useRandom;
    public Vector3 minRange, maxRange;
    public bool useVelocity;
    public bool useY;
    private Rigidbody2D rb2d;
    private Rigidbody rb;

    private void Start() {
        rb2d = GetComponent<Rigidbody2D>();
        rb = GetComponent<Rigidbody>();

        if (useRandom) {
            direction = new Vector3(Random.Range(minRange.x, maxRange.x), Random.Range(minRange.y, maxRange.y), Random.Range(minRange.z, maxRange.z));
        }
    }

    void Update () {
		

        if (!useVelocity) {
            transform.Translate(direction * Time.deltaTime);
        }
	}

    private void FixedUpdate() {
        if (useVelocity) {
            if (rb2d) {
                if (!useY) {
                    direction.y = rb2d.velocity.y;
                }
                rb2d.velocity = direction * Time.deltaTime;
            }
            else if (rb) {
                if (!useY) {
                    direction.y = rb.velocity.y;
                }
                rb.velocity = direction * Time.deltaTime;
            }
        }
    }
}

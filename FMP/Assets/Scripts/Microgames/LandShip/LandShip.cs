using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandShip : MonoBehaviour {

    public float boostSpeed;
    public float deathVelocity;
    public float rotateSpeed;
    Rigidbody2D rb;
    bool complete;

    private void Start() {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update() {
        if (!complete)
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, transform.rotation.z - Input.acceleration.x * 45), rotateSpeed * Time.deltaTime);
    }

    void FixedUpdate () {
        if (!complete && Input.touchCount > 0) {
            Vector2 direction = new Vector2(0, boostSpeed * Time.deltaTime);
            direction = (Vector2)transform.TransformDirection(direction);
            rb.velocity += direction;
        }
	}

    private void OnCollisionEnter2D(Collision2D collision) {
        if (rb.velocity.magnitude >= deathVelocity) {
            // die
            print("die");
        }
        else {
            if (collision.transform.CompareTag("Finish")) {
                complete = true;
                gameManager.instance.CompleteGame();
            }
        }
    }
}

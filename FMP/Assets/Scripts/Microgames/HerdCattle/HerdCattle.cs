using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HerdCattle : MonoBehaviour {

    public float moveSpeed;
    bool moving = true;
    LockAndKey key;
    float movement;
    float slowdown;

    private void Start() {
        key = GetComponent<LockAndKey>();
        slowdown = Random.Range(0.95f, 0.9875f);
    }

    void Update () {
		if (moving) {
            movement = moveSpeed;
        }
        else {
            movement *= slowdown;
        }
        transform.Translate(transform.up * movement * Time.deltaTime);

        if (key.complete && moving) {
            moving = false;
            GetComponent<Drag>().enabled = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        LockAndKey other = collision.GetComponent<LockAndKey>();
        if (other) {
            if (key && other.type == LockAndKey.Types.Lock && key.id != other.id) {
                foreach (Rigidbody2D rb1 in FindObjectsOfType<Rigidbody2D>()) {
                    LockAndKey other1 = rb1.GetComponent<LockAndKey>();
                    if (rb1.transform != transform && other1 && other1.id != key.id) {
                        rb1.gravityScale = 2;
                        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
                    }
                }
                StartCoroutine(DelayedLoss());
            }
        }
    }

    IEnumerator DelayedLoss() {
        FindObjectOfType<Timer>().winOnTimeOver = false;
        yield return new WaitForSeconds(1);
        gameManager.instance.FailGame();
    }
}

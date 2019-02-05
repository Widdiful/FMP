using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockAndKey : MonoBehaviour {

	public enum Types { Lock, Key };
    public Types type;
    public bool snapToLock;
    public static int connectionsMade;
    public int connectionsRequired;

    bool complete = false;
    Rigidbody2D rb;

    private void Start() {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        LockAndKey other = collision.GetComponent<LockAndKey>();
        if (!complete && !other.complete && type == Types.Key && other.type == Types.Lock) {
            connectionsMade++;
            other.complete = true;
            complete = true;
            if (snapToLock) {
                transform.position = collision.transform.position;
            }
            if (rb) {
                rb.gravityScale = 0;
                rb.velocity = Vector3.zero;
            }
            foreach (Behaviour comp in GetComponents<Behaviour>()) {
                if (comp != this) {
                    comp.enabled = false;
                }
            }

            if (connectionsMade >= connectionsRequired) {
                gameManager.instance.CompleteGame();
            }
        }
    }
}

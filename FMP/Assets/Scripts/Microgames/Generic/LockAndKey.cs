using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockAndKey : MonoBehaviour {

	public enum Types { Lock, Key };
    public Types type;
    public int id;
    public bool snapToLock;
    public static int connectionsMade = 0;
    public int connectionsRequired;
    public bool disableComponents = true;
    public bool allowMultiple;
    public bool failOnWrongLock;

    public bool complete = false;
    Rigidbody2D rb;

    private void Start() {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        LockAndKey other = collision.GetComponent<LockAndKey>();
        if (!complete && other && !other.complete && type == Types.Key && other.type == Types.Lock) {
            if (id == other.id) {
                connectionsMade++;
                if (other.allowMultiple)
                    other.complete = true;
                complete = true;
                if (snapToLock) {
                    transform.position = collision.transform.position;
                }

                if (disableComponents) {
                    if (rb) {
                        rb.gravityScale = 0;
                        rb.velocity = Vector3.zero;
                    }

                    foreach (Behaviour comp in GetComponents<Behaviour>()) {
                        if (comp != this) {
                            comp.enabled = false;
                        }
                    }
                }

                if (connectionsMade >= connectionsRequired) {
                    connectionsMade = 0;
                    gameManager.instance.CompleteGame();
                }
            }

            else if (failOnWrongLock) {
                complete = true;
                gameManager.instance.FailGame();
            }
        }
    }
}

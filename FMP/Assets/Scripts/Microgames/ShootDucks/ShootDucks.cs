using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootDucks : MonoBehaviour {

    public Vector2 minAngle;
    public Vector2 maxAngle;
    public float speed;
    public float fallSpeed;
    public Vector2 spawnTimeRange;
    Vector3 direction;
    float spawnTimer;
    public bool dead;

    private void Start() {
        direction = new Vector3(Random.Range(minAngle.x, maxAngle.x), Random.Range(minAngle.y, maxAngle.y), 0).normalized;
        spawnTimer = Random.Range(spawnTimeRange.x, spawnTimeRange.y);
    }

    void Update () {
        if (spawnTimer <= 0 && !dead) {
            transform.Translate(direction * speed * Time.deltaTime);

            if (Input.touchCount > 0) {
                foreach (Touch touch in Input.touches) {
                    RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(touch.position), Vector2.zero);
                    if (hit && hit.transform == transform) {
                        dead = true;

                        bool complete = true;
                        foreach (ShootDucks duck in FindObjectsOfType<ShootDucks>()) {
                            if (!duck.dead)
                                complete = false;
                        }
                        if (complete)
                            gameManager.instance.CompleteGame();

                    }
                }
            }
        }

        else if (!dead) {
            spawnTimer -= Time.deltaTime;
        }

        else {
            transform.Translate(Vector3.down * fallSpeed * Time.deltaTime);
        }
	}
}

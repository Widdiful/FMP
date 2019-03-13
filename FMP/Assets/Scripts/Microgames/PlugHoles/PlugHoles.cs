using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlugHoles : MonoBehaviour {

    public int maxHoles;
    public Vector2 minSpawnPos, maxSpawnPos;
    public bool plugged;
    public GameObject particles;
    bool hasBeenPlugged;
    bool complete;

    private void Start() {
        Vector3 position = new Vector3(Random.Range(minSpawnPos.x, maxSpawnPos.x), Random.Range(minSpawnPos.y, maxSpawnPos.y), 0);
        transform.position = position;
        if (position.x < 0) {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x) * -1, transform.localScale.y, transform.localScale.z);
        }
        else {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
    }

    void Update() {
        if (!complete) {
            bool isTouched = false;
            foreach (Touch touch in Input.touches) {
                RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(touch.position), Vector2.zero);

                if (hit && hit.transform == transform) {
                    if (!hasBeenPlugged) {
                        if (maxHoles > 0) {
                            maxHoles--;
                            Instantiate(gameObject, Vector3.zero, Quaternion.identity);
                        }
                        hasBeenPlugged = true;
                    }
                    isTouched = true;
                    plugged = true;
                    particles.SetActive(false);

                    if (maxHoles == 0) {
                        bool allPlugged = true;
                        foreach (PlugHoles hole in FindObjectsOfType<PlugHoles>()) {
                            if (!hole.plugged) {
                                allPlugged = false;
                                break;
                            }
                        }
                        if (allPlugged) {
                            foreach (PlugHoles hole in FindObjectsOfType<PlugHoles>()) {
                                hole.complete = true;
                            }
                            gameManager.instance.CompleteGame();
                        }
                    }

                }
            }

            if (!isTouched) {
                plugged = false;
                particles.SetActive(true);
            }
        }
    }
}

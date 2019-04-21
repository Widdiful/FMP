using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dog : MonoBehaviour {

    public List<Color> colours = new List<Color>();
    public float rubsRequired;
    public float rubs;
    private float rubTime;
    public float jumpSpeed = 2;
    public float maxJumpSpeed;
    private bool completed;
    public bool runOntoScreen;
    public Vector3 runLocation;

    void Start() {
        if (jumpSpeed == -1) {
            jumpSpeed = Random.Range(5.0f, 20.0f);
        }
        GetComponentInChildren<Renderer>().material.color = colours[Random.Range(0, colours.Count)];
        if (Random.Range(0, 2) == 0) {
            GetComponentInChildren<Renderer>().materials[1].color = colours[Random.Range(0, colours.Count)];
        }
        else {
            GetComponentInChildren<Renderer>().materials[1].color = GetComponentInChildren<Renderer>().material.color;
        }
    }

    public void Update() {
        rubTime += Time.deltaTime * jumpSpeed;
        transform.position = new Vector3(transform.position.x, Mathf.Abs(Mathf.Sin(rubTime) * 0.2f), transform.position.z);
        if (runOntoScreen) {
            transform.position = Vector3.Lerp(transform.position, runLocation, 0.2f);
        }

        if (Input.touchCount > 0) {
            if (Input.touches[0].phase == TouchPhase.Moved) {
                RaycastHit hit;
                if (Physics.Raycast(Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position), Camera.main.transform.forward, out hit) && hit.transform == transform) {
                    Rub();
                }
            }
        }
    }

    public void Rub() {
        if (!completed) {
            rubs += Time.deltaTime;
            jumpSpeed = (rubs / rubsRequired) * maxJumpSpeed;
        }
        if (rubs >= rubsRequired && !completed) {
            completed = true;
            GetComponent<AudioSource>().Play();
            if (FindObjectOfType<gameManager>()) {
                FindObjectOfType<gameManager>().CompleteGame();
            }
            foreach (Dog dog in FindObjectsOfType<Dog>()) {
                if (dog != this) {
                    dog.runOntoScreen = true;
                }
            }
        }
    }
}

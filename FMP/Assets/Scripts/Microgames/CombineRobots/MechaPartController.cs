using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechaPartController : MonoBehaviour {

    public float moveSpeed;
    public float accelleration;
    public int connectionsRequired;
    public float failTime;

    public Canvas completeCanvas;
    public GameObject explosionObject;

    private float currentSpeed;
    private int numberOfConnections;

    private bool completed;
	
	void Update () {
        currentSpeed = Mathf.Lerp(currentSpeed, moveSpeed, accelleration);

        transform.Translate(transform.forward * currentSpeed * Time.deltaTime);

        if (numberOfConnections >= connectionsRequired && !completed) {
            completed = true;
            completeCanvas.enabled = true;
            FindObjectOfType<gameManager>().CompleteGame();
        }

        if (!completed && GameObject.FindObjectOfType<Timer>().timer <= failTime) {
            completed = true;
            moveSpeed = 0;
            foreach(ParticleSystem particleSystem in GameObject.FindObjectsOfType<ParticleSystem>()) {
                if (particleSystem.gameObject.name != "ExplosionFire")
                    particleSystem.gameObject.SetActive(false);
            }
            foreach(Renderer renderer in transform.GetComponentsInChildren<Renderer>()) {
                renderer.enabled = false;
            }
            explosionObject.SetActive(true);
        }
	}

    private void OnTriggerEnter(Collider other) {
        numberOfConnections++;
        other.transform.SetParent(transform);
        other.gameObject.GetComponentInChildren<ParticleSystem>().Play();
        other.gameObject.GetComponent<MechaPart>().moving = false;
    }
}

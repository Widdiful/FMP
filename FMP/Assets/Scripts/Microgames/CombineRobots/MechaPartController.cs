using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechaPartController : MonoBehaviour {

    public float moveSpeed;
    public float accelleration;
    public int connectionsRequired;
    private float currentSpeed;
    private int numberOfConnections;

    private bool completed;
	
	void Update () {
        currentSpeed = Mathf.Lerp(currentSpeed, moveSpeed, accelleration);

        transform.Translate(transform.forward * currentSpeed);

        if (numberOfConnections >= connectionsRequired && !completed) {
            completed = true;
            FindObjectOfType<gameManager>().CompleteGame();
        }
	}

    private void OnTriggerEnter(Collider other) {
        numberOfConnections++;
        other.transform.SetParent(transform);
        other.gameObject.GetComponentInChildren<ParticleSystem>().Play();
    }
}

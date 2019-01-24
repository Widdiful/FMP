using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlowBoat : MonoBehaviour {

    public float moveSpeed;
    private Rigidbody rb;

    private void Start() {
        rb = GetComponent<Rigidbody>();
    }

    void Update () {
        float forwardVelocity = moveSpeed * MicManager.instance.levelMax * Time.deltaTime;
        forwardVelocity *= 100;
        if (forwardVelocity > rb.velocity.z) rb.velocity = new Vector3(0, 0, forwardVelocity);
        print(forwardVelocity);
	}

    private void OnTriggerEnter(Collider other) {
        FindObjectOfType<gameManager>().CompleteGame();
    }
}

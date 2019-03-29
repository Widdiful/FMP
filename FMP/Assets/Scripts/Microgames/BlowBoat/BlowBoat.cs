using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlowBoat : MonoBehaviour {

    public float moveSpeed;
    public float floatOffset;
    private Rigidbody rb;
    public Cloth cloth;

    private void Start() {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate() {
        float forwardVelocity = moveSpeed * MicManager.instance.levelMax * Time.deltaTime;
        forwardVelocity *= 100;
        if (forwardVelocity > rb.velocity.z) rb.velocity = new Vector3(0, 0, forwardVelocity);

        //Vector3 closestVertex = new Vector3();
        //float closestDistance = Mathf.Infinity;
        //foreach (Vector3 vertex in cloth.vertices) {
        //    float distance = Vector3.Distance(transform.position, vertex - cloth.transform.position);
        //    if (distance < closestDistance) {
        //        closestDistance = distance;
        //        closestVertex = vertex - cloth.transform.position;
        //    }
        //}

        //transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, closestVertex.y + floatOffset, transform.position.z), 0.2f);
    }

    private void OnTriggerEnter(Collider other) {
        gameManager.instance.CompleteGame();
    }
}

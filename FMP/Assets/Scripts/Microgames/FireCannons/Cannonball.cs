using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannonball : MonoBehaviour {

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Enemy")) {
            other.GetComponent<Renderer>().enabled = false;
            other.GetComponent<MoveInDirection>().enabled = false;

            foreach (ParticleSystem particle in other.GetComponentsInChildren<ParticleSystem>()) {
                particle.Play();
            }

            gameManager.instance.CompleteGame();
            Destroy(gameObject);
        }
    }
}

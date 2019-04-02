using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannonball : MonoBehaviour {

    AudioSource audioSource;

    private void Start() {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Enemy")) {
            foreach (MeshRenderer renderer in other.GetComponentsInChildren<MeshRenderer>()) {
                renderer.enabled = false;
            }
            foreach (SkinnedMeshRenderer renderer in other.GetComponentsInChildren<SkinnedMeshRenderer>()) {
                renderer.enabled = false;
            }
            //other.GetComponent<Renderer>().enabled = false;
            other.GetComponent<MoveInDirection>().enabled = false;

            foreach (ParticleSystem particle in other.GetComponentsInChildren<ParticleSystem>()) {
                particle.Play();
            }

            audioSource.Play();
            transform.position = new Vector3(-100, -100, -100);
            gameManager.instance.CompleteGame();
        }
    }
}

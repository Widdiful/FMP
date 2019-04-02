using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalanceBall : MonoBehaviour {

    AudioSource audioSource;

    private void Start() {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        audioSource.Play();
        gameManager.instance.FailGame();
    }
}

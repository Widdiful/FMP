using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotSounds : MonoBehaviour {

    AudioSource audioSource;

    private void Start() {
        audioSource = GetComponent<AudioSource>();
    }

    void Update() {
        if (Input.touchCount > 0) {
            foreach (Touch touch in Input.touches) {
                audioSource.Play();
            }
        }
    }
}

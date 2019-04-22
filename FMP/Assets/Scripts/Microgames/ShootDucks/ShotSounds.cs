using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotSounds : MonoBehaviour {

    AudioSource audioSource;
    float audioCountdown;

    private void Start() {
        audioSource = GetComponent<AudioSource>();
    }

    void Update() {
        audioCountdown -= Time.deltaTime;
        if (Input.touchCount > 0) {
            foreach (Touch touch in Input.touches) {
                if (touch.phase == TouchPhase.Began && audioCountdown <= 0) {
                    audioSource.Play();
                    audioCountdown = 0.2f;
                }
            }
        }
    }
}

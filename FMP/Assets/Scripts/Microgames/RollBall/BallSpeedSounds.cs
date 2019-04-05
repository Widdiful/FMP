using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSpeedSounds : MonoBehaviour {

    public float minPitch, maxPitch;
    public float minSpeed, maxSpeed;
    AudioSource audioSource;
    Rigidbody rb;

    private void Start() {
        audioSource = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody>();
    }

    void Update () {
        
	}

    private void OnCollisionStay(Collision collision) {
        float percentage = (minSpeed - rb.velocity.magnitude) / (minSpeed - maxSpeed);
        percentage = Mathf.Clamp01(percentage);

        float pitch = (percentage * (maxPitch - minPitch)) + minPitch;
        audioSource.pitch = pitch;

        if (percentage <= 0) audioSource.Stop();
        else if (!audioSource.isPlaying) audioSource.Play();
    }

    private void OnCollisionExit(Collision collision) {
        audioSource.Stop();
    }
}

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundOnVelocity : MonoBehaviour {

    public float velocity;
    Rigidbody2D rb;
    AudioSource audioSource;
    bool canPlay = true;

    private void Start() {
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update () {
		if (canPlay && rb.velocity.magnitude >= velocity) {
            audioSource.Play();
            canPlay = false;
        }
	}
}
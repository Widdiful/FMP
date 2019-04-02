﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireCannons : MonoBehaviour {

    public GameObject cannonBall;
    public float shotVelocity;
    public ParticleSystem particle;
    AudioSource audioSource;

    bool canShoot = true;

    private void Start() {
        audioSource = GetComponent<AudioSource>();
    }

    private void Update() {
        if (MicManager.instance.levelMax >= 0.75 && canShoot) {
            FireCannon();
            canShoot = false;
        }
    }

    private void FireCannon() {
        GameObject ball = Instantiate(cannonBall, transform.position, transform.rotation);
        ball.GetComponent<Rigidbody>().velocity = transform.forward * shotVelocity;
        particle.Play();
        audioSource.Play();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandShip : MonoBehaviour {

    public float boostSpeed;
    public float deathVelocity;
    public float rotateSpeed;
    public ParticleSystem fireParticles, smokeParticles;
    public AudioSource audioSource;
    Animator anim;
    Rigidbody2D rb;
    bool complete;
    ParticleSystem.EmissionModule fireEmitter, smokeEmitter;

    private void Start() {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        if (fireParticles)
            fireEmitter = fireParticles.emission;
        if (smokeParticles)
            smokeEmitter = smokeParticles.emission;
    }

    private void Update() {
        if (!complete)
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, transform.rotation.z - Input.acceleration.x * 45), rotateSpeed * Time.deltaTime);
    }

    void FixedUpdate () {
        if (!complete && Input.touchCount > 0) {
            Vector2 direction = new Vector2(0, boostSpeed * Time.deltaTime);
            direction = (Vector2)transform.TransformDirection(direction);
            rb.velocity += direction;
            if (fireParticles)
                fireEmitter.rateOverTime = 100;
            if (smokeParticles)
                smokeEmitter.rateOverTime = 100;
            if (anim)
                anim.SetBool("RocketOn", true);
            if (audioSource && !audioSource.isPlaying)
                audioSource.Play();
        }
        else {
            if (fireParticles)
                fireEmitter.rateOverTime = 0;
            if (smokeParticles)
                smokeEmitter.rateOverTime = 0;
            if (anim)
                anim.SetBool("RocketOn", false);
            if (audioSource)
                audioSource.Stop();
        }
	}

    private void OnCollisionEnter2D(Collision2D collision) {
        if (rb.velocity.magnitude >= deathVelocity) {
            // die
            //print("die");
        }
        else {
            if (collision.transform.CompareTag("Finish")) {
                complete = true;
                gameManager.instance.CompleteGame();
            }
        }
    }
}

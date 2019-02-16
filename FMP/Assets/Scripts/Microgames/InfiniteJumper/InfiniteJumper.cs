using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfiniteJumper : MonoBehaviour {

    public float jumpHeight, finishVelocity;
    public ParticleSystem fireParticle, smokeParticle;
    Rigidbody2D rb;
    bool canJump;
    Animator anim;
    Squish squish;
    SpriteRenderer sprite;
    LockAndKey key;

	void Start () {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        squish = GetComponent<Squish>();
        key = GetComponent<LockAndKey>();

        sprite = GetComponent<SpriteRenderer>();
        sprite.color = Color.HSVToRGB(Random.Range(0.0f, 1.0f), 1, 1);
    }
	
	void Update () {
        if (canJump) {
            rb.velocity += new Vector2((transform.up * jumpHeight).x, (transform.up * jumpHeight).y);
            squish.Pulse(new Vector2(-1, 1));
            canJump = false;
        }

        if (key.complete) {
            rb.velocity = new Vector3(0, finishVelocity);
            FindObjectOfType<CameraFollow>().enabled = false;
            fireParticle.Play();
            smokeParticle.Play();
        }

        anim.SetFloat("velocityY", rb.velocity.y);
        anim.SetBool("grounded", canJump);
    }

    void OnCollisionEnter2D(Collision2D col) {
        // Ground check
        RaycastHit2D ray = Physics2D.Raycast(transform.position, -transform.up, 1.5f);
        if (ray.collider) {
            canJump = true;
        }
    }
}

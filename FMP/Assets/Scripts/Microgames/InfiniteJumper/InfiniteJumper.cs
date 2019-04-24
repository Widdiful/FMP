using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfiniteJumper : MonoBehaviour {

    public float jumpHeight, finishVelocity;
    public ParticleSystem fireParticle, smokeParticle;
    public AudioClip finishClip;
    Rigidbody2D rb;
    bool canJump = false;
    Animator anim;
    Squish squish;
    SpriteRenderer sprite;
    LockAndKey key;
    AudioSource audioSource;
    bool complete;

	void Start () {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        squish = GetComponent<Squish>();
        key = GetComponent<LockAndKey>();
        audioSource = GetComponent<AudioSource>();

        sprite = GetComponent<SpriteRenderer>();
        sprite.color = Color.HSVToRGB(Random.Range(0.0f, 1.0f), 1, 1);
    }
	
	void Update () {
        if (canJump) {
            rb.velocity += new Vector2((transform.up * jumpHeight).x, (transform.up * jumpHeight).y);
            squish.Pulse(new Vector2(-1, 1));
            if (audioSource)
                audioSource.Play();
            canJump = false;
        }

        if (key.complete) {
            rb.velocity = new Vector3(0, finishVelocity);
            FindObjectOfType<CameraFollow>().enabled = false;
            fireParticle.Play();
            smokeParticle.Play();
            if (!complete && audioSource) {
                audioSource.clip = finishClip;
                audioSource.Play();
            }
            complete = true;
        }

        anim.SetFloat("velocityY", rb.velocity.y);
        anim.SetBool("grounded", canJump);

        Vector3 newPos = transform.position;
        newPos.x = Mathf.Clamp(newPos.x, -3, 3);
        transform.position = newPos;
    }

    void OnCollisionEnter2D(Collision2D col) {
        // Ground check
        RaycastHit2D ray = Physics2D.Raycast(transform.position, -transform.up, 1.5f);
        if (ray.collider) {
            canJump = true;

            LockAndKey lockAndKey = ray.collider.gameObject.GetComponent<LockAndKey>();
            if (lockAndKey && lockAndKey.id != key.id) {
                gameManager.instance.FailGame();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        // Ground check
        RaycastHit2D ray = Physics2D.Raycast(transform.position, -transform.up, 1.5f);
        if (ray.collider) {
            LockAndKey lockAndKey = ray.collider.gameObject.GetComponent<LockAndKey>();
            if (lockAndKey && lockAndKey.id != key.id) {
                gameManager.instance.FailGame();
            }
        }
    }
}

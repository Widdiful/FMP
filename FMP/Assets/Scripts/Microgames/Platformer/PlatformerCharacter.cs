using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlatformerCharacter : MonoBehaviour {
    public float moveSpeed;
    public float jumpHeight;
    public int maxJumps;

    private Rigidbody2D rb;
    private Animator anim;
    private int jumps;
    private bool grounded;
    private float maxSpeed = 10;

    private float Horizontal;
    private bool jumping;

    private AudioSource audioSource;

	void Start () {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        jumps = maxJumps;
	}
	
	void Update () {
        Debug.DrawLine(transform.position, transform.position - (transform.up * 1.5f), Color.yellow);
        // Input
        Horizontal = Mathf.Clamp(CrossPlatformInputManager.GetAxis("Horizontal") + Input.GetAxis("Horizontal"), -1, 1);
        jumping = CrossPlatformInputManager.GetButtonDown("Jump") || Input.GetButtonDown("Jump");

        // Movement
        if (Horizontal != 0) {
            if (grounded) {
                rb.velocity = new Vector2(moveSpeed * Horizontal, rb.velocity.y);
                if (Horizontal < 0) GetComponent<SpriteRenderer>().flipX = true;
                else GetComponent<SpriteRenderer>().flipX = false;
            }
            else {
                if (Mathf.Abs(Horizontal) > 0.1f) {
                    rb.AddForce(transform.right * moveSpeed * Horizontal * 3);
                    rb.velocity = new Vector2(Mathf.Clamp(rb.velocity.x, -maxSpeed, maxSpeed), rb.velocity.y);
                }
            }
        }
        if (jumping) {
            if (jumps > 0 && rb.velocity.y <= 0) {
                Jump();
            }
        }

        // Animation data
        anim.SetFloat("velocityY", rb.velocity.y);
        anim.SetBool("grounded", grounded);
        if (grounded && Horizontal == 0) anim.speed = 0;
        else anim.speed = 1;
    }

    void OnCollisionEnter2D(Collision2D col) {
        // Ground check
        RaycastHit2D ray = Physics2D.Raycast(transform.position, -transform.up, 1.5f);
        if (ray.collider) {
            if (!grounded) GetComponent<Squish>().Pulse(new Vector2(2, -2));
            grounded = true;
            jumps = maxJumps;
        }
    }

    void OnCollisionExit2D(Collision2D col) {
        // Ground check
        RaycastHit2D ray = Physics2D.Raycast(transform.position, -transform.up, 1.5f);
        if (!ray.collider) {
            grounded = false;
        }
    }

    void OnTriggerEnter2D(Collider2D col) {
        if (col.GetComponent<PlatformerEnemy>()) {
            if (rb.velocity.y <= 0) {
                col.GetComponent<PlatformerEnemy>().Kill();
                Jump();
            }
        }
    }

    private void Jump() {
        Vector2 velocity = rb.velocity;
        velocity.y = 0;
        rb.velocity = velocity;
        rb.velocity += new Vector2((transform.up * jumpHeight).x, (transform.up * jumpHeight).y);
        GetComponent<Squish>().Pulse(new Vector2(-2, 2));
        jumps--;
        if (audioSource)
            audioSource.Play();
    }
}

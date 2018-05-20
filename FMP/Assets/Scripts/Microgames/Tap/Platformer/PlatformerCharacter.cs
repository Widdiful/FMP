using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlatformerCharacter : MonoBehaviour {
    public float moveSpeed;
    public float jumpHeight;
    public int maxJumps;

    private Rigidbody2D rb;
    private int jumps;
    private bool grounded;
    private float maxSpeed = 10;

    private float Horizontal;
    private bool Jump;

	void Start () {
        rb = GetComponent<Rigidbody2D>();
        jumps = maxJumps;
	}
	
	void Update () {
        // Input
        Horizontal = Mathf.Clamp(CrossPlatformInputManager.GetAxis("Horizontal") + Input.GetAxis("Horizontal"), -1, 1);
        Jump = CrossPlatformInputManager.GetButtonDown("Jump") || Input.GetButtonDown("Jump");

        // Movement
        if (Horizontal != 0) {
            if (grounded)
                rb.velocity = new Vector2(moveSpeed * Horizontal, rb.velocity.y);
            else {
                if (Mathf.Abs(Input.GetAxis("Horizontal")) > 0.1f) {
                    rb.AddForce(Vector2.right * moveSpeed * Horizontal * 3);
                    rb.velocity = new Vector2(Mathf.Clamp(rb.velocity.x, -maxSpeed * 1.333f, maxSpeed * 1.333f), rb.velocity.y);
                }
            }
        }
        if (Jump) {
            if (jumps > 0 && rb.velocity.y <= 0) {
                rb.velocity = new Vector2(rb.velocity.x, jumpHeight);
                jumps--;
            }
        }
    }

    void OnCollisionEnter2D(Collision2D col) {
        // Ground check
        RaycastHit2D ray = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y), -transform.up, 1);
        if (ray.collider) {
            grounded = true;
            jumps = maxJumps;
        }
    }

    void OnCollisionExit2D(Collision2D col) {
        // Ground check
        RaycastHit2D ray = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y), -transform.up, 1);
        if (!ray.collider) {
            grounded = false;
        }
    }
}

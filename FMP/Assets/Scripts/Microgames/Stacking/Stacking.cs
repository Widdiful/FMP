using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Stacking : MonoBehaviour {

    public bool isPlayer;
    public Transform attachedObject;
    public Vector3 attachOffset;
    public float lerpSpeed;
    public float moveSpeed;
    public float jumpHeight;
    public Rigidbody2D rb;
    CameraFollow cameraFollow;
    SpriteRenderer sprite;
    bool canJump;
    float jumpTimer;

    private void Start() {
        rb = GetComponent<Rigidbody2D>();
        cameraFollow = FindObjectOfType<CameraFollow>();
        sprite = GetComponent<SpriteRenderer>();
    }

    private void Update() {
        jumpTimer -= Time.deltaTime;
        if (attachedObject) {
            Vector3 newPos = Vector3.Lerp(transform.position, attachedObject.position + attachOffset, lerpSpeed * Time.deltaTime);

            if (transform.position.x - newPos.x < 0)
                sprite.flipX = false;
            else if (transform.position.x - newPos.x > 0)
                sprite.flipX = true;

            transform.position = newPos;
        }
    }

    private void FixedUpdate() {
        if (isPlayer) {
            Vector2 velocity = rb.velocity;
            velocity.x = CrossPlatformInputManager.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;

            if (velocity.x < 0)
                sprite.flipX = true;
            else if (velocity.x > 0)
                sprite.flipX = false;

            rb.velocity = velocity;

            if (CrossPlatformInputManager.GetButtonDown("Jump") && canJump && jumpTimer <= 0) {
                jumpTimer = 0.1f;
                canJump = false;
                rb.velocity += new Vector2((transform.up * jumpHeight).x, (transform.up * jumpHeight).y);
                GetComponent<Squish>().Pulse(new Vector2(-1, 2));
            }

            if (cameraFollow)
                cameraFollow.target = transform;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (!canJump) {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector3.down, 0.55f);
            if (hit) {
                canJump = true;
            }
        }

        if (isPlayer && rb.velocity.y < 0) {
            Stacking stack = collision.gameObject.GetComponent<Stacking>();
            if (stack && !attachedObject && !stack.attachedObject) {
                attachedObject = stack.transform;
                isPlayer = false;
                rb.velocity = Vector2.zero;
                stack.isPlayer = true;
                GetComponent<Collider2D>().isTrigger = true;
                GetComponent<Rigidbody2D>().gravityScale = 0;

                bool complete = true;
                foreach(Stacking stacking in FindObjectsOfType<Stacking>()) {
                    if (!stacking.attachedObject && !stacking.isPlayer) {
                        complete = false;
                    }
                }

                if (complete)
                    gameManager.instance.CompleteGame();
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision) {
        if (!canJump) {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector3.down, 0.55f);
            if (!hit) {
                canJump = false;
            }
        }
    }
}

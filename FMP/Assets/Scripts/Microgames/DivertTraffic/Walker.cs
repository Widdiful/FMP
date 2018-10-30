using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walker : MonoBehaviour {

    public float speed;
    public float speedIncrease;
    public bool randomWandering;
    public float wanderSize;
    private Vector2 direction;
    private SpriteRenderer sprite;
    private Vector3 pregrabPosition;
    private gameManager gm;

    void Start() {
        direction += new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
        direction = direction.normalized;
        sprite = GetComponent<SpriteRenderer>();
        sprite.color = Color.HSVToRGB(Random.Range(0.0f, 1.0f), 1, 1);
        gm = FindObjectOfType<gameManager>();
    }

    void Update () {

        bool grabbed = false;

        if (Input.touchCount > 0) {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.touches[0].position), Vector2.zero);

            if (hit) {
                if (hit.transform == transform) {
                    grabbed = true;
                    Vector3 dragPos = Camera.main.ScreenToWorldPoint(Input.touches[0].position);
                    dragPos.z = transform.position.z;
                    transform.position = dragPos;
                    direction = (transform.position - pregrabPosition).normalized;
                }
            }
        }

        if (!grabbed && !gm.endingGame) {
            transform.Translate(direction * speed * Time.deltaTime);
            pregrabPosition = transform.position;

            if (randomWandering) {
                direction += new Vector2(Random.Range(-wanderSize, wanderSize), Random.Range(-wanderSize, wanderSize));
                direction = direction.normalized;
            }

            if (direction.x < -0.1f) sprite.flipX = true;
            else if (direction.x > 0.1f) sprite.flipX = false;

            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.y * 0.01f);
        }
        else if (gm.endingGame) {
            if (Time.frameCount % 10 == 0) {
                sprite.flipX = !sprite.flipX;
            }
        }
	}

    private void OnCollisionEnter2D(Collision2D collision) {
        direction = Vector2.Reflect(direction, collision.contacts[0].normal);
        speed += speedIncrease;

        if (collision.gameObject.CompareTag("Player")) {
            FindObjectOfType<gameManager>().FailGame();
        }
    }
}

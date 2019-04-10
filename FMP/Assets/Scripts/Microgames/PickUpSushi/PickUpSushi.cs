using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpSushi : MonoBehaviour {

    public float moveSize;
    public float moveSpeed;
    public float lerpSpeed;
    public float downPosition;
    float timer;
    bool movingDown, movingUp;
    float baseY;
    bool complete;
    public Sprite grabSprite;
    public SpriteRenderer spriteRenderer;

    private void Start() {
        baseY = transform.position.y;
        timer = Random.Range(0.0f, 1.0f);
    }

    void Update () {
        if (!movingDown && !movingUp) {
            timer += Time.deltaTime * moveSpeed;
            transform.position = new Vector3(Mathf.Sin(timer) * moveSize, transform.position.y, transform.position.z);

            if (Input.touchCount > 0 && !complete) {
                movingDown = true;
            }
        }
        else if (movingDown) {
            Vector3 targetPos = transform.position;
            targetPos.y = downPosition;
            transform.position = Vector3.MoveTowards(transform.position, targetPos, lerpSpeed * Time.deltaTime);
            if (transform.position == targetPos) {
                movingDown = false;
                movingUp = true;
            }
        }
        else if (movingUp) {
            Vector3 targetPos = transform.position;
            targetPos.y = baseY;
            transform.position = Vector3.MoveTowards(transform.position, targetPos, lerpSpeed * Time.deltaTime);
            if (transform.position == targetPos) {
                movingDown = false;
                movingUp = false;
            }
        }
	}

    private void OnTriggerEnter2D(Collider2D collision) {
        collision.transform.SetParent(transform);
        collision.GetComponent<MoveInDirection>().enabled = false;
        complete = true;
        spriteRenderer.sprite = grabSprite;
        gameManager.instance.CompleteGame();
    }
}

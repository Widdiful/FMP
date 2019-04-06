using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SneakAndStealGuard : MonoBehaviour {

    public bool looking;
    public Vector2 waitRange;
    public Vector2 lookRange;
    public float jumpHeight, jumpSpeed, jumpWait;
    public bool gameEnd;
    SpriteRenderer sprite;
    float time;
    float baseHeight;
    AudioSource audioSource;

    private void Start() {
        sprite = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
        baseHeight = transform.position.y;

        StartCoroutine(Wait());
        time = jumpSpeed;
    }

    private void Update() {
        if (time < 10) {
            time += Time.deltaTime * jumpSpeed;
            transform.position = new Vector3(transform.position.x, baseHeight + Mathf.Clamp01(Mathf.Sin(time) / 2 + (0.5f - jumpWait)) * jumpHeight, transform.position.z);
            if (transform.position.y >= baseHeight && !audioSource.isPlaying) {
                audioSource.Play();
            }
        }
        else if (gameEnd) {
            time = 0;
            if (!looking)
                TurnAround();
        }
        else {
            transform.position = new Vector3(transform.position.x, baseHeight, transform.position.z);
        }
    }

    void TurnAround() {
        looking = !looking;
        sprite.flipX = !sprite.flipX;
    }

    IEnumerator Wait() {
        yield return new WaitForSeconds(Random.Range(waitRange.x, waitRange.y));
        StartCoroutine(Turn());
        
    }

    IEnumerator Turn() {
        time = 0;
        while (time < 10) {
            yield return new WaitForEndOfFrame();
        }
        TurnAround();
        yield return new WaitForSeconds(Random.Range(lookRange.x, lookRange.y));
        TurnAround();
        StartCoroutine(Wait());
    }
}

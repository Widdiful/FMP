using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShushFinger : MonoBehaviour {

    public float speed;
    public bool drag;
    bool completed;
    bool onMouth;
    AudioSource audioSource;

    private void Start() {
        audioSource = GetComponent<AudioSource>();
    }

    void Update () {
		if (Input.touchCount > 0) {
            if (drag) {
                Vector3 direction = Input.touches[0].deltaPosition;
                transform.Translate(direction * speed * Time.deltaTime);
            }
            else {
                Vector3 pos = Camera.main.ScreenToWorldPoint(Input.touches[0].position);
                pos.z = transform.position.z;
                transform.position = Vector3.MoveTowards(transform.position, pos, speed);
            }
        }

        if (onMouth && !completed) {
            if (MicManager.instance.levelMax >= 0.75f) {
                completed = true;
                audioSource.Play();
                gameManager.instance.CompleteGame();
            }
        }
	}

    private void OnTriggerStay2D(Collider2D collision) {
        TalkingMouth mouth = collision.GetComponent<TalkingMouth>();

        if (mouth && !completed) {
            mouth.enabled = false;
            onMouth = true;
            mouth.GetComponent<AudioSource>().Pause();
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        TalkingMouth mouth = collision.GetComponent<TalkingMouth>();

        if (mouth && !completed) {
            mouth.enabled = true;
            onMouth = false;
            mouth.GetComponent<AudioSource>().Play();
        }
    }
}

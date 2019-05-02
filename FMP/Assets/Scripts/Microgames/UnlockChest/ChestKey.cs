using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestKey : MonoBehaviour {

    public Vector3 pickupOffset;
    public GameObject particles;
    Transform target;
    bool complete;
    bool pickedUp;
    AudioSource audioSource;
    public AudioClip pickupSound, unlockSound;
    public Timer timer;

    private void Start() {
        audioSource = GetComponent<AudioSource>();
    }

    private void LateUpdate() {
        if (target) {
            transform.position = target.position + pickupOffset;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Player") && !pickedUp) {
            transform.parent = null;
            target = collision.transform;
            transform.GetComponent<CircleCollider2D>().enabled = false;
            transform.GetComponent<BoxCollider2D>().enabled = true;
            pickedUp = true;
            if (audioSource) {
                audioSource.clip = pickupSound;
                audioSource.Play();
            }
        }
        else if (collision.name == "Chest" && !complete) {
            complete = true;
            HideKey();
            particles.SetActive(true);
            if (audioSource) {
                audioSource.Stop();
                audioSource.clip = unlockSound;
                audioSource.Play();
            }
            StartCoroutine(DelayedComplete());
        }
    }

    public void HideKey() {
        GetComponent<SpriteRenderer>().enabled = false;
    }

    IEnumerator DelayedComplete() {
        timer.active = false;
        yield return new WaitForSeconds(0.5f);
        gameManager.instance.CompleteGame();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundOnCollision : MonoBehaviour {

    public Transform target;
    public AudioClip clip;
    AudioSource audioSource;

    private void Start() {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (!target || collision.transform == target)
            if (clip) {
                audioSource.clip = clip;
                audioSource.Stop();
            }

            if (!audioSource.isPlaying) {
                audioSource.Play();
            }
    }
}

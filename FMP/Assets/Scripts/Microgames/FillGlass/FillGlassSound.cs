using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FillGlassSound : MonoBehaviour {

    public float timerAddPerDrop;
    float timer;
    public AudioSource audioSource;
    List<Transform> collidedParticles = new List<Transform>();

    void Update () {
        timer -= Time.deltaTime;
        if (timer > 0) {
            if (!audioSource.isPlaying) {
                audioSource.Play();
            }
        }
        else {
            timer = 0;
            audioSource.Pause();
        }
	}

    private void OnTriggerEnter2D(Collider2D collision) {
        if (!collidedParticles.Contains(collision.transform)) {
            collidedParticles.Add(collision.transform);
            timer = timerAddPerDrop;
        }
    }
}

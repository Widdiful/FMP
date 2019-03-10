using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interrupt : MonoBehaviour {

    public Vector2 timeBeforeSpeechRange;
    float timeBeforeSpeech;
    bool talking;
    bool completed;
    float timer;
    AudioSource audioSource;

    private void Start() {
        audioSource = GetComponent<AudioSource>();

        timeBeforeSpeech = Random.Range(timeBeforeSpeechRange.x, timeBeforeSpeechRange.y);
    }

    void Update () {
        if (!talking && !completed) {
            if (timer < timeBeforeSpeech) {
                timer += Time.deltaTime;
                if (MicManager.instance.levelMax >= 0.75f) {
                    completed = true;
                    gameManager.instance.FailGame();
                }
            }

            else {
                talking = true;
                audioSource.Play();
            }
        }

        if (!completed && MicManager.instance.levelMax >= 0.75f) {
            if (audioSource.isPlaying) {
                completed = true;
                audioSource.Stop();
                gameManager.instance.CompleteGame();
            }
            else {
                completed = true;
                gameManager.instance.FailGame();
            }
        }
	}
}

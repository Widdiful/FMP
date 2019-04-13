using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interrupt : MonoBehaviour {

    public Vector2 timeBeforeSpeechRange;
    float timeBeforeSpeech;
    bool talking;
    bool completed;
    float timer;
    public List<AudioClip> clips = new List<AudioClip>();
    public AudioClip shushClip;
    public Animator anim;
    AudioSource audioSource;

    private void Start() {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = clips[Random.Range(0, clips.Count)];

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
                anim.SetBool("Open", true);
            }
        }

        if (!completed && talking && !audioSource.isPlaying) {
            anim.SetBool("Open", false);
        }

        if (!completed && MicManager.instance.levelMax >= 0.75f) {
            if (audioSource.isPlaying) {
                completed = true;
                audioSource.Stop();
                audioSource.clip = shushClip;
                audioSource.Play();
                anim.SetBool("Open", false);
                gameManager.instance.CompleteGame();
            }
            else {
                completed = true;
                gameManager.instance.FailGame();
            }
        }
	}
}

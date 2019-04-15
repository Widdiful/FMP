using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkingMouth : MonoBehaviour {

    public float speed;
    public List<AudioClip> audioClips = new List<AudioClip>();
    AudioSource audioSource;
    float timer;

    private void Start() {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = audioClips[Random.Range(0, audioClips.Count)];
        audioSource.Play();
    }

    void Update () {
        timer += Time.deltaTime * speed;
        float yScale = Mathf.Sin(timer);
        transform.localScale = new Vector3(transform.localScale.x, yScale, transform.localScale.z);
	}
}

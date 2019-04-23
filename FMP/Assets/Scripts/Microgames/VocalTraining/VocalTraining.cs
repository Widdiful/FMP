using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VocalTraining : MonoBehaviour {

    public Transform noteCentre, noteHolder;
    public GameObject notePrefab;
    public VocalTrainingTrack track;
    public float allowedDistance;
    bool hasTouched;
    public List<GameObject> singers = new List<GameObject>();
    List<Animator> anims = new List<Animator>();
    List<Squish> squishes = new List<Squish>();
    AudioSource audioSource;
    public float pitchMin, pitchMax;

    List<VocalTrainingNote> notes = new List<VocalTrainingNote>();
    int touchPhase = 0;

    private void Start() {
        foreach(GameObject singer in singers) {
            anims.Add(singer.GetComponentInChildren<Animator>());
            squishes.Add(singer.GetComponent<Squish>());
        }
        audioSource = GetComponent<AudioSource>();
    }

    private void Update() {
        if (Time.timeScale > 0) {
            if (Input.touchCount > 0) {
                hasTouched = true;
                if (Input.GetTouch(0).phase == TouchPhase.Began)
                    touchPhase = 0;
                else if (Input.GetTouch(0).phase == TouchPhase.Ended) {
                    foreach (Animator anim in anims) {
                        anim.SetBool("Open", false);
                    }
                    foreach (Squish squish in squishes) {
                        squish.Pulse(new Vector2(1, -1));
                    }
                    audioSource.Pause();
                }
                else
                    touchPhase = 1;
            }
            else if (hasTouched)
                touchPhase = 2;
            else touchPhase = -1;

            if (touchPhase == 0) {
                VocalTrainingNote newNote = Instantiate(notePrefab, noteCentre.position, Quaternion.identity, noteHolder).GetComponent<VocalTrainingNote>();
                newNote.type = VocalTrainingNote.Types.Start;
                notes.Add(newNote);
                if (track.notes.Count >= notes.Count && Mathf.Abs(newNote.transform.position.x - track.notes[notes.Count - 1].transform.position.x) <= allowedDistance) {
                    newNote.correct = true;
                }

                newNote = Instantiate(notePrefab, noteCentre.position, Quaternion.identity, noteHolder).GetComponent<VocalTrainingNote>();
                newNote.type = VocalTrainingNote.Types.End;
                newNote.previousNote = notes[notes.Count - 1].transform;
                notes.Add(newNote);

                foreach (Animator anim in anims) {
                    anim.SetBool("Open", true);
                }
                foreach (Squish squish in squishes) {
                    squish.Pulse(new Vector2(-1, 1));
                }
                audioSource.pitch = Random.Range(pitchMin, pitchMax);
                audioSource.Play();
            }
            else if (touchPhase == 1) {
                notes[notes.Count - 1].transform.position = noteCentre.position;
            }
            else if (touchPhase == 2) {
                if (track.notes.Count >= notes.Count && Mathf.Abs(notes[notes.Count - 1].transform.position.x - track.notes[notes.Count - 1].transform.position.x) <= allowedDistance) {
                    notes[notes.Count - 1].correct = true;
                }

                if (notes.Count >= track.notes.Count) {
                    bool complete = true;
                    foreach (VocalTrainingNote note in notes) {
                        if (!note.correct) {
                            complete = false;
                        }
                    }

                    if (complete) {
                        notes.Clear();
                        track.notes.Clear();
                        gameManager.instance.CompleteGame();
                    }
                }
            }
        }
    }
}

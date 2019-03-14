using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VocalTraining : MonoBehaviour {

    public Transform noteCentre, noteHolder;
    public GameObject notePrefab;
    public VocalTrainingTrack track;
    public float allowedDistance;

    List<VocalTrainingNote> notes = new List<VocalTrainingNote>();
    int touchPhase = 0;

    private void Update() {

        if (Input.touchCount > 0) {
            if (Input.GetTouch(0).phase == TouchPhase.Began)
                touchPhase = 0;
            else
                touchPhase = 1;
        }
        else touchPhase = 2;
        
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
                foreach(VocalTrainingNote note in notes) {
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

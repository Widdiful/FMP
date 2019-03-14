using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VocalTrainingTrack : MonoBehaviour {

    public int numberOfNotes;
    public float startingDistance;
    public Vector2 lengthRange;
    public Vector2 distanceRange;
    public Transform noteHolder;
    public Transform notePrefab;
    public List<VocalTrainingNote> notes = new List<VocalTrainingNote>();

    private void Start() {
        float currentDistance = startingDistance;

        for(int i = 0; i < numberOfNotes; i++) {
            Vector3 position = new Vector3(currentDistance, 0, 0);
            VocalTrainingNote newNote = Instantiate(notePrefab, noteHolder.position + position, Quaternion.identity, noteHolder).GetComponent<VocalTrainingNote>();
            notes.Add(newNote);

            currentDistance += Random.Range(lengthRange.x, lengthRange.y);

            position = new Vector3(currentDistance, 0, 0);
            newNote = Instantiate(notePrefab, noteHolder.position + position, Quaternion.identity, noteHolder).GetComponent<VocalTrainingNote>();
            newNote.type = VocalTrainingNote.Types.End;
            newNote.previousNote = notes[notes.Count - 1].transform;
            notes.Add(newNote);

            currentDistance += Random.Range(distanceRange.x, distanceRange.y);
        }
    }
}

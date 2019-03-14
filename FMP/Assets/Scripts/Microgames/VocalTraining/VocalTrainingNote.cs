using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VocalTrainingNote : MonoBehaviour {

	public enum Types { Start, End };
    public Types type;
    public Transform previousNote;
    public LineRenderer line;
    public bool correct = false;

    private void Update() {
        if (previousNote && line) {
            line.SetPosition(0, transform.position);
            line.SetPosition(1, previousNote.position);
        }
    }
}

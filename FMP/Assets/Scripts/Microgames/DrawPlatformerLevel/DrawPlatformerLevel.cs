using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawPlatformerLevel : MonoBehaviour {

    public Canvas inputCanvas;
    public Drawing drawing;

	void Update () {
		if (!inputCanvas.enabled && drawing.finished) {
            inputCanvas.enabled = true;
        }
	}
}

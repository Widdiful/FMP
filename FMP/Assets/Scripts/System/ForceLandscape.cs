using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceLandscape : MonoBehaviour {

	void Start () {
        Screen.orientation = gameManager.instance.previousLandscapeOrientation;
        Screen.autorotateToLandscapeLeft = true;
        Screen.autorotateToLandscapeRight = true;
        Screen.autorotateToPortrait = false;
        Screen.autorotateToPortraitUpsideDown = false;
        Screen.orientation = ScreenOrientation.AutoRotation;
    }
}

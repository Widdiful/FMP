using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RotateManager : MonoBehaviour {

    public enum TargetOrientation { Portrait, Landscape }
    public TargetOrientation targetOrientation;
    public GameObject horizontalTexts, verticalTexts;

    private gameManager gm;
    private bool cleared = false;

	void Start () {
        gm = GameObject.FindObjectOfType<gameManager>();
        Screen.autorotateToLandscapeLeft = true;
        Screen.autorotateToLandscapeRight = true;
        Screen.autorotateToPortrait = true;
        Screen.autorotateToPortraitUpsideDown = false;
        Screen.orientation = ScreenOrientation.AutoRotation;
    }
	
	void Update () {
        if (!cleared) {
            if (targetOrientation == TargetOrientation.Landscape && Screen.width > Screen.height) {
                Screen.autorotateToPortrait = false;
                Screen.autorotateToPortraitUpsideDown = false;
                Screen.orientation = ScreenOrientation.AutoRotation;
                cleared = true;
                horizontalTexts.SetActive(false);
                verticalTexts.SetActive(true);
                gm.CompleteGame();
            }
            else if (targetOrientation == TargetOrientation.Portrait && Screen.width < Screen.height) {
                Screen.autorotateToLandscapeLeft = false;
                Screen.autorotateToLandscapeRight = false;
                Screen.orientation = ScreenOrientation.AutoRotation;
                cleared = true;
                horizontalTexts.SetActive(false);
                verticalTexts.SetActive(true);
                gm.CompleteGame();
            }
        }
    }
}

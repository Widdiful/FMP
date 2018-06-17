using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RotateManager : MonoBehaviour {

    public enum TargetOrientation { Portrait, Landscape }
    public TargetOrientation targetOrientation;

    private gameManager gm;
    private bool cleared = false;

	void Start () {
        gm = GameObject.FindObjectOfType<gameManager>();
        Screen.autorotateToLandscapeLeft = true;
        Screen.autorotateToLandscapeRight = true;
        Screen.autorotateToPortrait = true;
        Screen.autorotateToPortraitUpsideDown = true;
        Screen.orientation = ScreenOrientation.AutoRotation;
    }
	
	void Update () {
        if (!cleared) {
            if (targetOrientation == TargetOrientation.Landscape && Screen.width > Screen.height) {
                Screen.autorotateToPortrait = false;
                Screen.autorotateToPortraitUpsideDown = false;
                Screen.orientation = ScreenOrientation.AutoRotation;
                cleared = true;
                gm.CompleteGame();
            }
            else if (targetOrientation == TargetOrientation.Portrait && Screen.width < Screen.height) {
                Screen.autorotateToLandscapeLeft = false;
                Screen.autorotateToLandscapeRight = false;
                Screen.orientation = ScreenOrientation.AutoRotation;
                print(Screen.autorotateToLandscapeLeft);
                cleared = true;
                gm.CompleteGame();
            }
        }
        GameObject.Find("Text").GetComponent<Text>().text = cleared.ToString();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestRoom : MonoBehaviour {

    public Scrollbar micScrollBar;
    public Sprite defaultSprite;
    public Sprite calibrationSprite;
    public Image calibrationImage;
    public Renderer lightbulbMesh;
    public Material lightbulbOffMat, lightbulbOnMat;
    bool calibrating;
    float calibrationLevel = 0;
    Vector2 defaultGravity;
    float defaultGravityMagnitude;

    private void Start() {
        defaultGravity = Physics2D.gravity;
        defaultGravityMagnitude = defaultGravity.magnitude;
    }

    void Update () {
        micScrollBar.size = MicManager.instance.levelMaxRaw / gameManager.instance.micSensitivity;
        if (calibrating) {
            if (MicManager.instance.levelMaxRaw > calibrationLevel) {
                calibrationLevel = MicManager.instance.levelMaxRaw;
            }
        }

        if (ProximitySensor.instance.nearby)
            lightbulbMesh.material = lightbulbOnMat;
        else
            lightbulbMesh.material = lightbulbOffMat;

        Quaternion rotation = Quaternion.Euler(0, 0, (Input.gyro.gravity.x) * 90);
        Vector2 newGravity = rotation * Vector2.down * defaultGravityMagnitude;
        Physics2D.gravity = newGravity;
    }

    public void ToggleCalibration() {
        calibrating = !calibrating;

        switch (calibrating) {
            case true:
                calibrationImage.sprite = calibrationSprite;
                calibrationLevel = 0;
                break;
            case false:
                calibrationImage.sprite = defaultSprite;
                if (calibrationLevel == 0)
                    calibrationLevel = 0.01f;
                gameManager.instance.micSensitivity = calibrationLevel;
                PlayerPrefs.SetFloat("micSensitivity", calibrationLevel);
                break;
        }
    }

    private void OnDestroy() {
        Physics2D.gravity = defaultGravity;
    }
}

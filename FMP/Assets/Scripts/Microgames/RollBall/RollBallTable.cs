using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollBallTable : MonoBehaviour {

    public float rotateScale;
    public Rigidbody ballRb;
    public Timer timer;
    public Canvas pauseCanvas;
    private bool active = false;
    private float startTimer;
    private bool firstFrameDrawn;

	void Start () {
        Input.gyro.enabled = false;
        Input.gyro.enabled = true;

        startTimer = 0.2f;
    }
	
	void Update () {

        if (Time.timeScale > 0)
            transform.localRotation = Quaternion.Euler(Input.gyro.gravity.x * rotateScale, 0, Input.gyro.gravity.y * rotateScale);


        if (!active) {
            if (firstFrameDrawn)
                startTimer -= Time.unscaledDeltaTime;

            if (startTimer <= 0) {
                if (Mathf.Abs(transform.localRotation.x) < 0.05f && Mathf.Abs(transform.localRotation.z) < 0.05f) {
                    active = true;
                    ballRb.useGravity = true;
                    timer.active = true;
                    pauseCanvas.enabled = false;
                    return;
                }
            }
            ballRb.useGravity = false;
            ballRb.velocity = Vector3.zero;
            timer.active = false;
        }
        firstFrameDrawn = true;
	}
}

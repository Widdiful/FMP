using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToiletPaper : MonoBehaviour {

    public float speedMultiplier;
    public float minRollSize;
    public float rollScaleSpeed;
    public Transform roll;

    private float startRollSize;
    private float lastTouchYPos;
    private AudioSource audioSource;

    void Start() {
        audioSource = GetComponent<AudioSource>();
        startRollSize = roll.transform.localScale.x;
        lastTouchYPos = 1;
        Roll(0);
    }

	void Update () {
        if (Input.touchCount > 0 && Time.timeScale > 0) {
            if (Input.touches[0].phase == TouchPhase.Began) {
                lastTouchYPos = Input.touches[0].position.y / Screen.height;
            }
            if (Input.touches[0].phase == TouchPhase.Moved) {
                float yPos = Input.touches[0].position.y / Screen.height;
                if (yPos < lastTouchYPos) Roll(Mathf.Abs(yPos - lastTouchYPos) * speedMultiplier);
                lastTouchYPos = yPos;
            }
        }
	}

    private void Roll(float speed) {
        if (roll.localScale.x >= minRollSize) {
            transform.Translate(new Vector3(speed * 5f, 0, 0));
            transform.localPosition = new Vector3(transform.localPosition.x, (roll.transform.localScale.x - startRollSize) * 0.5f, 0);
            transform.localScale += new Vector3(speed, 0, 0);
            roll.localScale = new Vector3(roll.localScale.x * (1f - (speed * rollScaleSpeed)), roll.localScale.y, roll.localScale.z * (1f - (speed * rollScaleSpeed)));
            roll.Rotate(new Vector3(0, -speed * 360, 0));
            GetComponent<Renderer>().materials[0].mainTextureScale = new Vector2(transform.localScale.x * 2, 1);
            if (!audioSource.isPlaying && speed > 0) {
                audioSource.Play();
            }
        }
        else {
            GetComponent<Rigidbody>().AddForce(Vector3.down * 500);
            gameManager.instance.CompleteGame();
        }
    }
}

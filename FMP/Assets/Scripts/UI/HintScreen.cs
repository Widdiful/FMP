using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HintScreen : MonoBehaviour {

    private float timer;
    private gameManager gm;

    public float speed = 10;
    public Transform hintText;
    public Transform inputPanel;

    public Sprite tapIcon;
    public Sprite motionIcon;
    public Sprite micIcon;
    public Sprite proxIcon;

    AudioSource audioSource;

	void Start () {
        gm = gameManager.instance;
        if (gm) {
            timer = gm.hintScreenDuration;
            if (gm.currentGame.useTap) inputPanel.Find("TapIcon").GetComponent<Image>().sprite = tapIcon;
            if (gm.currentGame.useMotion) inputPanel.Find("MotionIcon").GetComponent<Image>().sprite = motionIcon;
            if (gm.currentGame.useMic) inputPanel.Find("MicIcon").GetComponent<Image>().sprite = micIcon;
            if (gm.currentGame.useProximity) inputPanel.Find("ProxIcon").GetComponent<Image>().sprite = proxIcon;
        }
        else
            timer = 1;
	}
	
	void Update () {
        timer -= Time.unscaledDeltaTime;

        if (timer <= 0) {
            if (hintText) {
                hintText.transform.Translate(Vector3.right * speed * Time.unscaledDeltaTime);
            }

            if (inputPanel) {
                inputPanel.transform.Translate(-Vector3.up * speed * Time.unscaledDeltaTime);
            }
        }
	}
}

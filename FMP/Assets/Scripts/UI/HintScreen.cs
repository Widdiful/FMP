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
    public Text hintTextText, livesText;

    public Sprite tapIcon;
    public Sprite motionIcon;
    public Sprite micIcon;
    public Sprite proxIcon;

    public Image tapIconImage, motionIconImage, micIconImage, proxIconImage;

    AudioSource audioSource;

    public static HintScreen instance;
    bool active;

    private void Awake() {
        if (!instance)
            instance = this;
        else
            Destroy(this);
    }

    void Start () {
        gm = gameManager.instance;
        if (gm) {
            timer = gm.hintScreenDuration;
            if (gm.currentGame.useTap) tapIconImage.sprite = tapIcon;
            if (gm.currentGame.useMotion) motionIconImage.sprite = motionIcon;
            if (gm.currentGame.useMic) micIconImage.sprite = micIcon;
            if (gm.currentGame.useProximity) proxIconImage.sprite = proxIcon;
        }
        else
            timer = 1;
	}
	
	void Update () {
        if (active) {
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
        else {
            active = true;
        }
	}
}

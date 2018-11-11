using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour {

    public bool winOnTimeOver;
    public float seconds;
    public float timer;
    public Font font1;
    public Font font2;
    private gameManager gm;
    private RectTransform timerBar;
    private Text countdown;
    private Text lives;
    private Text hintText;
    private float timerWidth = 600;
    private string prevTime;
    public bool active = true;

	void Start () {
        timer = seconds;
        gm = GameObject.FindObjectOfType<gameManager>();

        if (GameObject.Find("TimerBar")) timerBar = GameObject.Find("TimerBar").GetComponent<RectTransform>();
        if (GameObject.Find("Countdown")) countdown = GameObject.Find("Countdown").GetComponent<Text>();
        if (GameObject.Find("Lives")) lives = GameObject.Find("Lives").GetComponent<Text>();
        if (GameObject.Find("HintText")) hintText = GameObject.Find("HintText").GetComponent<Text>();

        if (gm) {
            if (hintText)
                hintText.text = gm.currentGame.hint;

            if (gm.currentDifficulty == gameManager.DifficultyLevels.Chill) {
                foreach (Transform trans in transform) {
                    if (trans.name == "Canvas") {
                        trans.gameObject.SetActive(false);
                        break;
                    }
                }
            }
        }
	}
	
	void Update () {
        if (gm && active) {
            if (gm.currentDifficulty != gameManager.DifficultyLevels.Chill && !gm.endingGame) {
                if (!gm.startingGame) timer -= Time.deltaTime;

                if (timerBar) timerBar.sizeDelta = new Vector2(timerWidth * (timer / seconds), timerBar.sizeDelta.y);

                if (countdown) {
                    countdown.text = timer.ToString("#");

                    if (countdown.text == "1") {
                        countdown.color = Color.red;
                    }
                    if (countdown.text == "") {
                        countdown.font = font2;
                        countdown.text = "!!!";
                        countdown.fontSize = 130;
                    }
                    else {
                        countdown.font = font1;
                        countdown.fontSize = 100;
                    }
                    if (timer.ToString("#") != prevTime) countdown.gameObject.GetComponent<squish>().Squish(new Vector2(1, 1));
                    prevTime = timer.ToString("#");
                }

                if (lives) {
                    lives.text = gm.livesLeft.ToString();
                    if (lives.text == "1") {
                        lives.color = Color.red;
                    }
                }

                if (timer <= 0) {
                    active = false;
                    if (winOnTimeOver) gm.CompleteGame();
                    else gm.FailGame();
                }
            }
        }
	}
}

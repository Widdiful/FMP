using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour {

    public float seconds;
    public float timer;
    public Font font1;
    public Font font2;
    private gameManager gm;
    private RectTransform timerBar;
    private Text countdown;
    private float timerWidth = 600;
    private string prevTime;

	void Start () {
        timer = seconds;
        gm = GameObject.FindObjectOfType<gameManager>();
        timerBar = GameObject.Find("TimerBar").GetComponent<RectTransform>();
        countdown = GameObject.Find("Countdown").GetComponent<Text>();
        if (gm) {
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
        if (gm) {
            if (gm.currentDifficulty != gameManager.DifficultyLevels.Chill) {
                timer -= Time.deltaTime;

                timerBar.sizeDelta = new Vector2(timerWidth * (timer / seconds), timerBar.sizeDelta.y);
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

                if (timer <= 0) {
                    gm.FailGame();
                }
            }
        }
	}
}

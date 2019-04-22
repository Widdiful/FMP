using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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
    private float timerWidth = 512;
    private string prevTime;
    public bool active = true;

    public Canvas pauseMenu, quitMenu;
    public Text streakText;
    public Button pauseButton;

	void Start () {
        timer = seconds;
        gm = gameManager.instance;

        if (GameObject.Find("TimerBar")) timerBar = GameObject.Find("TimerBar").GetComponent<RectTransform>();
        if (GameObject.Find("Countdown")) countdown = GameObject.Find("Countdown").GetComponent<Text>();
        if (GameObject.Find("Lives")) lives = GameObject.Find("Lives").GetComponent<Text>();
        if (GameObject.Find("HintText")) hintText = GameObject.Find("HintText").GetComponent<Text>();

        if (gm) {
            if (SceneManager.GetActiveScene().name != "RotateHorizontal" && SceneManager.GetActiveScene().name != "RotateVertical") {
                if (hintText)
                    hintText.text = gm.currentGame.hint;

                if (gm.currentGame) {
                    if (gm.currentGame.isLandscape) {
                        if (Screen.width < Screen.height) {
                            Screen.orientation = gm.previousLandscapeOrientation;
                        }
                        else {
                            gm.previousLandscapeOrientation = Screen.orientation;
                        }
                    }
                    else if (!gm.currentGame.isLandscape && Screen.width > Screen.height) {
                        Screen.orientation = ScreenOrientation.Portrait;
                    }
                }
            }

            if (gm.currentDifficulty == gameManager.DifficultyLevels.Relax) {
                foreach (Transform trans in transform) {
                    if (trans.name == "Canvas") {
                        trans.gameObject.SetActive(false);
                        break;
                    }
                }
            }

            if (lives) {
                lives.text = gm.livesLeft.ToString();
                if (lives.text == "1") {
                    lives.color = Color.red;
                }
            }
        }
	}
	
	void Update () {
        if (gm && active) {
            if (gm.currentDifficulty != gameManager.DifficultyLevels.Relax && !gm.endingGame) {
                timer -= Time.deltaTime;

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
                    if (timer.ToString("#") != prevTime) countdown.gameObject.GetComponent<Squish>().Pulse(new Vector2(1, 1));
                    prevTime = timer.ToString("#");
                }

                if (timer <= 0) {
                    active = false;
                    if (winOnTimeOver) gm.CompleteGame();
                    else gm.FailGame();
                }
            }
        }
	}

    public void Pause() {
        Time.timeScale = 0;
        pauseMenu.enabled = true;
        pauseButton.interactable = false;
        streakText.text = "Current streak: " + gm.gamesCompleted.Count;
    }

    public void Resume() {
        Time.timeScale = gm.gameSpeed;
        pauseMenu.enabled = false;
        pauseButton.interactable = true;
    }

    public void QuitMenu() {
        pauseMenu.enabled = false;
        quitMenu.enabled = true;
    }

    public void DontQuit() {
        quitMenu.enabled = false;
        pauseMenu.enabled = true;
    }

    public void QuitGame() {
        quitMenu.enabled = false;
        Time.timeScale = gm.gameSpeed;
        if (gm.gameType == gameManager.GameTypes.Practice)
            gm.gameType = gameManager.GameTypes.Endless;
        gm.livesLeft = 0;
        gm.FailGame();
    }
}

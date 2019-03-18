using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RockPaperScissors : MonoBehaviour {

	public enum HandTypes { Rock, Paper, Scissors };
    public HandTypes enemyType;
    public Text text;

    private void Start() {
        enemyType = (HandTypes)Random.Range(0, 3);
        text.text = enemyType.ToString();
    }

    public void PlayRock() {
        if (enemyType == HandTypes.Scissors) {
            GetComponent<Canvas>().enabled = false;
            gameManager.instance.CompleteGame();
        }
        else {
            StartCoroutine(FailGame());
        }
    }

    public void PlayPaper() {
        if (enemyType == HandTypes.Rock) {
            GetComponent<Canvas>().enabled = false;
            gameManager.instance.CompleteGame();
        }
        else {
            StartCoroutine(FailGame());
        }
    }

    public void PlayScissors() {
        if (enemyType == HandTypes.Paper) {
            GetComponent<Canvas>().enabled = false;
            gameManager.instance.CompleteGame();
        }
        else {
            StartCoroutine(FailGame());
        }
    }

    IEnumerator FailGame() {
        GetComponent<Canvas>().enabled = false;
        yield return new WaitForSeconds(0.5f);
        gameManager.instance.FailGame();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RockPaperScissors : MonoBehaviour {

	public enum HandTypes { Rock, Paper, Scissors };
    public HandTypes enemyType;
    public GameObject enemyRock, enemyPaper, enemyScissors;
    public GameObject playerRock, playerPaper, playerScissors;
    public AudioClip winClip, loseClip;
    AudioSource audioSource;

    private void Start() {
        audioSource = GetComponent<AudioSource>();

        enemyType = (HandTypes)Random.Range(0, 3);
        switch (enemyType) {
            case HandTypes.Rock:
                enemyRock.SetActive(true);
                break;
            case HandTypes.Paper:
                enemyPaper.SetActive(true);
                break;
            case HandTypes.Scissors:
                enemyScissors.SetActive(true);
                break;
        }
    }

    public void PlayRock() {
        if (Time.timeScale > 0) {
            playerRock.GetComponent<Animator>().SetBool("Play", true);
            if (enemyType == HandTypes.Scissors) {
                audioSource.clip = winClip;
                audioSource.Play();
                GetComponent<Canvas>().enabled = false;
                gameManager.instance.CompleteGame();
            }
            else {
                StartCoroutine(FailGame());
            }
        }
    }

    public void PlayPaper() {
        if (Time.timeScale > 0) {
            playerPaper.GetComponent<Animator>().SetBool("Play", true);
            if (enemyType == HandTypes.Rock) {
                audioSource.clip = winClip;
                audioSource.Play();
                GetComponent<Canvas>().enabled = false;
                gameManager.instance.CompleteGame();
            }
            else {
                StartCoroutine(FailGame());
            }
        }
    }

    public void PlayScissors() {
        if (Time.timeScale > 0) {
            playerScissors.GetComponent<Animator>().SetBool("Play", true);
            if (enemyType == HandTypes.Paper) {
                audioSource.clip = winClip;
                audioSource.Play();
                GetComponent<Canvas>().enabled = false;
                gameManager.instance.CompleteGame();
            }
            else {
                StartCoroutine(FailGame());
            }
        }
    }

    IEnumerator FailGame() {
        audioSource.clip = loseClip;
        audioSource.Play();
        GetComponent<Canvas>().enabled = false;
        yield return new WaitForSeconds(0.5f);
        gameManager.instance.FailGame();
    }
}

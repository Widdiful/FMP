using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menuManager : MonoBehaviour {

    private gameManager gm;
    private GameObject bg;
    private bool startingGame;

	void Start() {
        gm = GameObject.FindObjectOfType<gameManager>();
        bg = GameObject.Find("Timer/Canvas/Background");
    }

    void Update() {
        if (startingGame) {
            bg.transform.localPosition = Vector3.Lerp(bg.transform.localPosition, new Vector3(-400, 0, 0), 0.2f);
            if (Mathf.Abs(-400 - bg.transform.localPosition.x) <= 0.001) {
                startingGame = false;
                bg.transform.localPosition = new Vector3(-400, 0, 0);
                gm.StartGame();
            }
        }
    }

    public void StartGame() {
        startingGame = true;
    }

    public void ReturnToMenu() {
        startingGame = false;
        SceneManager.LoadScene("Scenes/Menu");
    }
}

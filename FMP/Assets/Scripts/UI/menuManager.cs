using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class menuManager : MonoBehaviour {
    private gameManager gm;
    private GameObject bg;
    private GameObject menu;
    private bool startingGame;
    private int targetX;
    private int targetY;
    private bool movingMenu;

	void Start() {
        gm = GameObject.FindObjectOfType<gameManager>();
        bg = GameObject.Find("Timer/Canvas/Background");
        menu = GameObject.Find("Menus/Menus");
        if (menu) loadSettings();

        Screen.autorotateToLandscapeLeft = true;
        Screen.autorotateToLandscapeRight = true;
        Screen.autorotateToPortrait = false;
        Screen.autorotateToPortraitUpsideDown = false;
        Screen.orientation = ScreenOrientation.LandscapeRight;
        Screen.orientation = ScreenOrientation.AutoRotation;
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
        if (movingMenu && menu) {
            menu.GetComponent<RectTransform>().offsetMin = Vector2.Lerp(menu.GetComponent<RectTransform>().offsetMin, new Vector2(targetX, targetY), 0.1f);
            menu.GetComponent<RectTransform>().offsetMax = Vector2.Lerp(menu.GetComponent<RectTransform>().offsetMax, new Vector2(-targetX, -targetY), 0.1f);
        }
    }

    private void loadSettings() {
        gm.orientationMode = (gameManager.OrientationModes) PlayerPrefs.GetInt("orientation");
        gm.useMotion = intToBool(PlayerPrefs.GetInt("motion"));
        gm.useMic = intToBool(PlayerPrefs.GetInt("mic"));
        gm.useProximity = intToBool(PlayerPrefs.GetInt("prox"));
        //gm.money = PlayerPrefs.GetInt("money");

        GameObject.Find("Orientations/Dropdown").GetComponent<Dropdown>().value = PlayerPrefs.GetInt("orientation");
        GameObject.Find("Sensors/Motion").GetComponent<Toggle>().isOn = gm.useMotion;
        GameObject.Find("Sensors/Mic").GetComponent<Toggle>().isOn = gm.useMic;
        GameObject.Find("Sensors/Prox").GetComponent<Toggle>().isOn = gm.useProximity;
        GameObject.Find("Shop/MoneyText").GetComponent<Text>().text = gm.money.ToString();
    }

    public void saveSettings() {
        PlayerPrefs.SetInt("orientation", GameObject.Find("Orientations/Dropdown").GetComponent<Dropdown>().value);
        PlayerPrefs.SetInt("motion", Convert.ToInt32(GameObject.Find("Sensors/Motion").GetComponent<Toggle>().isOn));
        PlayerPrefs.SetInt("mic", Convert.ToInt32(GameObject.Find("Sensors/Mic").GetComponent<Toggle>().isOn));
        PlayerPrefs.SetInt("prox", Convert.ToInt32(GameObject.Find("Sensors/Prox").GetComponent<Toggle>().isOn));
        loadSettings();
        //MenuMain();
    }

    public void StartGame() {
        startingGame = true;
        if (Screen.orientation == ScreenOrientation.LandscapeLeft || Screen.orientation == ScreenOrientation.LandscapeRight)
            gameManager.instance.previousLandscapeOrientation = Screen.orientation;
    }

    public void StartEndless() {
        gameManager.instance.gameType = gameManager.GameTypes.Endless;
        StartGame();
    }

    public void StartChallenge() {
        gameManager.instance.gameType = gameManager.GameTypes.Challenge;
        StartGame();
    }

    public void ReturnToMenu() {
        startingGame = false;
        SceneManager.LoadScene("Scenes/Menu");
    }

    public void MenuMain() {
        loadSettings();
        MoveMenu(0, 0);
    }

    public void MenuOptions() {
        MoveMenu(0, 600);
    }

    private void MoveMenu(int x, int y) {
        targetX = x;
        targetY = y;
        movingMenu = true;
    }

    public void ReloadInventory() {
        InventoryManager.instance.UpdateUI();
    }



    bool intToBool(int i) {
        bool result = false;
        if (i > 0) result = true;
        return result;
    }
}

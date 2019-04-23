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
    private Canvas currentCanvas;
    private Button currentButton;

    public Canvas mainCanvas, practiseCanvas, shopCanvas, scoresCanvas, optionsCanvas, practiseMenuCanvas, editUserCanvas, buyGameCanvas;
    public Button mainButton, practiseButton, shopButton, scoresButton, optionsButton;
    public BuyGameMenu buyGameMenu;

    public Dropdown orientationDropdown;
    public Toggle motionToggle, micToggle, proxToggle, hintToggle;

    public static menuManager instance;

    private void Awake() {
        if (!instance)
            instance = this;
        else
            Destroy(this);

        Screen.autorotateToLandscapeLeft = true;
        Screen.autorotateToLandscapeRight = true;
        Screen.autorotateToPortrait = true;
        Screen.autorotateToPortraitUpsideDown = false;
        Screen.orientation = ScreenOrientation.AutoRotation;
    }

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

        if (mainCanvas)
            OpenMain();

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

    private void loadSettings() {
        gm.orientationMode = (gameManager.OrientationModes) PlayerPrefs.GetInt("orientation");
        gm.useMotion = intToBool(PlayerPrefs.GetInt("motion", 1));
        gm.useMic = intToBool(PlayerPrefs.GetInt("mic", 1));
        gm.useProximity = intToBool(PlayerPrefs.GetInt("prox", 1));
        gm.enableHints = intToBool(PlayerPrefs.GetInt("hints", 1));
        gm.micSensitivity = PlayerPrefs.GetFloat("micSensitivity", 1);
        //gm.money = PlayerPrefs.GetInt("money");

        orientationDropdown.value = PlayerPrefs.GetInt("orientation");
        motionToggle.isOn = gm.useMotion;
        micToggle.isOn = gm.useMic;
        proxToggle.isOn = gm.useProximity;
        hintToggle.isOn = gm.enableHints;
        GameObject.Find("Money/MoneyText").GetComponent<Text>().text = gm.money.ToString();
        GameObject.Find("Points/PointsText").GetComponent<Text>().text = gm.score.ToString();
    }

    public void saveSettings() {
        PlayerPrefs.SetInt("orientation", orientationDropdown.value);
        PlayerPrefs.SetInt("motion", Convert.ToInt32(motionToggle.isOn));
        PlayerPrefs.SetInt("mic", Convert.ToInt32(micToggle.isOn));
        PlayerPrefs.SetInt("prox", Convert.ToInt32(proxToggle.isOn));
        PlayerPrefs.SetInt("hints", Convert.ToInt32(hintToggle.isOn));
        loadSettings();
        //MenuMain();
    }

    public void StartGame() {
        startingGame = true;
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

    public void OpenInputTest() {
        startingGame = false;
        SceneManager.LoadScene("Scenes/TestLevel");
    }

    public void ReloadInventory() {
        InventoryManager.instance.UpdateUI();
    }

    public void ChangePage(int amount) {
        DatabaseManager.instance.ChangePage(amount);
    }

    public void BuyGame(Game game) {
        if (currentCanvas)
            currentCanvas.enabled = false;

        buyGameCanvas.enabled = true;
        currentCanvas = buyGameCanvas;
        buyGameMenu.OpenGame(game);
    }

    public void OpenMain() {
        if (currentCanvas)
            currentCanvas.enabled = false;
        if (currentButton)
            currentButton.interactable = true;
        mainCanvas.enabled = true;
        currentCanvas = mainCanvas;
        mainButton.interactable = false;
        currentButton = mainButton;

        InventoryManager.instance.UpdateUI();
    }

    public void OpenPractise() {
        if (currentCanvas)
            currentCanvas.enabled = false;
        if (currentButton)
            currentButton.interactable = true;
        practiseCanvas.enabled = true;
        currentCanvas = practiseCanvas;
        practiseButton.interactable = false;
        currentButton = practiseButton;

        GameSelect.instance.UpdateMenu();
    }

    public void OpenShop() {
        if (currentCanvas)
            currentCanvas.enabled = false;
        if (currentButton)
            currentButton.interactable = true;
        shopCanvas.enabled = true;
        currentCanvas = shopCanvas;
        shopButton.interactable = false;
        currentButton = shopButton;
    }

    public void OpenScores() {
        if (currentCanvas)
            currentCanvas.enabled = false;
        if (currentButton)
            currentButton.interactable = true;
        scoresCanvas.enabled = true;
        currentCanvas = scoresCanvas;
        scoresButton.interactable = false;
        currentButton = scoresButton;

        DatabaseManager.instance.UpdateScore(gameManager.instance.score);
        DatabaseManager.instance.UpdatePlayerData();
    }

    public void OpenOptions() {
        if (currentCanvas)
            currentCanvas.enabled = false;
        if (currentButton)
            currentButton.interactable = true;
        optionsCanvas.enabled = true;
        currentCanvas = optionsCanvas;
        optionsButton.interactable = false;
        currentButton = optionsButton;
    }

    public void OpenPractiseMenu() {
        currentCanvas = practiseMenuCanvas;
    }

    public void OpenProfileMenu() {
        if (currentCanvas)
            currentCanvas.enabled = false;
        currentCanvas = editUserCanvas;
        editUserCanvas.enabled = true;

        DatabaseManager.instance.UpdatePlayerData();
    }



    bool intToBool(int i) {
        bool result = false;
        if (i > 0) result = true;
        return result;
    }
}

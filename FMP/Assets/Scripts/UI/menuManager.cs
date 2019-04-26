using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class menuManager : MonoBehaviour {
    private gameManager gm;
    private GameObject bg;
    private GameObject menu;
    private bool startingGame;
    private Canvas currentCanvas;
    private Canvas currentPopup;
    private Button currentButton;
    public AudioMixer mixer;

    public Canvas mainCanvas, practiseCanvas, shopCanvas, scoresCanvas, optionsCanvas, practiseMenuCanvas, editUserCanvas, buyGameCanvas, clearDataCanvas;
    public Button mainButton, practiseButton, shopButton, scoresButton, optionsButton;
    public BuyGameMenu buyGameMenu;
    public GridLayoutGroup mainGrid;
    public InputField hintDurationText;

    public Dropdown orientationDropdown;
    public Toggle motionToggle, micToggle, proxToggle, hintToggle;
    public Slider masterAudio, sfxAudio, musicAudio;

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

        if (mainGrid) {
            if (Screen.width > Screen.height) {
                mainGrid.constraintCount = 2;
            }
            else {
                mainGrid.constraintCount = 1;
            }
        }
    }

    public void loadSettings() {
        gm.orientationMode = (gameManager.OrientationModes) PlayerPrefs.GetInt("orientation");
        gm.useMotion = intToBool(PlayerPrefs.GetInt("motion", 1));
        gm.useMic = intToBool(PlayerPrefs.GetInt("mic", 1));
        gm.useProximity = intToBool(PlayerPrefs.GetInt("prox", 1));
        gm.enableHints = intToBool(PlayerPrefs.GetInt("hints", 1));
        gm.micSensitivity = PlayerPrefs.GetFloat("micSensitivity", 1);
        masterAudio.value = PlayerPrefs.GetFloat("masterVol", 0);
        sfxAudio.value = PlayerPrefs.GetFloat("sfxVol", 0);
        musicAudio.value = PlayerPrefs.GetFloat("musicVol", 0);
        hintDurationText.text = PlayerPrefs.GetFloat("hintDuration", 1.0f).ToString();
        //gm.money = PlayerPrefs.GetInt("money");

        orientationDropdown.value = PlayerPrefs.GetInt("orientation");
        motionToggle.isOn = gm.useMotion;
        micToggle.isOn = gm.useMic;
        proxToggle.isOn = gm.useProximity;
        hintToggle.isOn = gm.enableHints;
        gameManager.instance.hintScreenDuration = float.Parse(hintDurationText.text);
        GameObject.Find("Money/MoneyText").GetComponent<Text>().text = gm.money.ToString();
        GameObject.Find("Points/PointsText").GetComponent<Text>().text = gm.score.ToString();

        mixer.SetFloat("masterVol", masterAudio.value);
        mixer.SetFloat("sfxVol", sfxAudio.value);
        mixer.SetFloat("musicVol", musicAudio.value);
    }

    public void saveSettings() {
        PlayerPrefs.SetInt("orientation", orientationDropdown.value);
        PlayerPrefs.SetInt("motion", Convert.ToInt32(motionToggle.isOn));
        PlayerPrefs.SetInt("mic", Convert.ToInt32(micToggle.isOn));
        PlayerPrefs.SetInt("prox", Convert.ToInt32(proxToggle.isOn));
        PlayerPrefs.SetInt("hints", Convert.ToInt32(hintToggle.isOn));
        PlayerPrefs.SetFloat("masterVol", masterAudio.value);
        PlayerPrefs.SetFloat("sfxVol", sfxAudio.value);
        PlayerPrefs.SetFloat("musicVol", musicAudio.value);
        PlayerPrefs.SetFloat("hintDuration", float.Parse(hintDurationText.text));
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

    private void CloseCanvases() {
        if (currentCanvas)
            currentCanvas.enabled = false;
        if (currentButton)
            currentButton.interactable = true;
        if (currentPopup)
            currentPopup.enabled = false;
    }

    public void BuyGame(Game game) {
        buyGameCanvas.enabled = true;
        currentPopup = buyGameCanvas;
        buyGameMenu.OpenGame(game);
    }

    public void OpenMain() {
        CloseCanvases();
        mainCanvas.enabled = true;
        currentCanvas = mainCanvas;
        mainButton.interactable = false;
        currentButton = mainButton;

        InventoryManager.instance.UpdateUI();
    }

    public void OpenPractise() {
        CloseCanvases();
        practiseCanvas.enabled = true;
        currentCanvas = practiseCanvas;
        practiseButton.interactable = false;
        currentButton = practiseButton;

        GameSelect.instance.UpdateMenu();
    }

    public void OpenShop() {
        CloseCanvases();
        shopCanvas.enabled = true;
        currentCanvas = shopCanvas;
        shopButton.interactable = false;
        currentButton = shopButton;
    }

    public void OpenScores() {
        CloseCanvases();
        scoresCanvas.enabled = true;
        currentCanvas = scoresCanvas;
        scoresButton.interactable = false;
        currentButton = scoresButton;

        DatabaseManager.instance.UpdateScore(gameManager.instance.score);
        DatabaseManager.instance.UpdatePlayerData();
    }

    public void OpenOptions() {
        CloseCanvases();
        optionsCanvas.enabled = true;
        currentCanvas = optionsCanvas;
        optionsButton.interactable = false;
        currentButton = optionsButton;
    }

    public void OpenPractiseMenu() {
        if (currentPopup)
            currentPopup.enabled = false;
        currentPopup = practiseMenuCanvas;
    }

    public void OpenProfileMenu() {
        if (currentCanvas)
            currentCanvas.enabled = false;
        currentCanvas = editUserCanvas;
        editUserCanvas.enabled = true;

        DatabaseManager.instance.UpdatePlayerData();
    }

    public void OpenClearMenu() {
        if (currentPopup)
            currentPopup.enabled = false;
        currentPopup = clearDataCanvas;
        clearDataCanvas.enabled = true;
    }

    public void CloseClearMenu() {
        if (currentPopup)
            currentPopup.enabled = false;
    }

    public void ClearData() {
        if (currentPopup)
            currentPopup.enabled = false;
        SaveData.instance.ClearData();
    }

    public void DecreaseHintDuration() {
        if (float.Parse(hintDurationText.text) > 0.1f) {
            hintDurationText.text = (float.Parse(hintDurationText.text) - 0.1000f).ToString();
        }
    }

    public void IncreaseHintDuration() {
        if (float.Parse(hintDurationText.text) < 10) {
            hintDurationText.text = (float.Parse(hintDurationText.text) + 0.1000f).ToString();
        }
    }



    bool intToBool(int i) {
        bool result = false;
        if (i > 0) result = true;
        return result;
    }
}

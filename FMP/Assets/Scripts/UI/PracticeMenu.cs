using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PracticeMenu : MonoBehaviour {

    public Microgame selectedGame;
    public Canvas selectCanvas;

    public Image image;
    public Text gameName;
    public Transform inputPanel;
    public Sprite tapIcon;
    public Sprite motionIcon;
    public Sprite micIcon;
    public Sprite proxIcon;
    public Sprite blankIcon;

    private Image tapIconImage;
    private Image motionIconImage;
    private Image micIconImage;
    private Image proxIconImage;

    private Canvas thisCanvas;

    private void Start() {
        thisCanvas = GetComponent<Canvas>();

        tapIconImage = inputPanel.Find("TapIcon").GetComponent<Image>();
        motionIconImage = inputPanel.Find("MotionIcon").GetComponent<Image>();
        micIconImage = inputPanel.Find("MicIcon").GetComponent<Image>();
        proxIconImage = inputPanel.Find("ProxIcon").GetComponent<Image>();
    }

    public void SelectGame(Microgame game) {
        selectedGame = game;
        selectCanvas.enabled = false;
        thisCanvas.enabled = true;

        gameName.text = selectedGame.displayName;
        if (selectedGame.useTap) tapIconImage.sprite = tapIcon;
        else tapIconImage.sprite = blankIcon;
        if (selectedGame.useMotion) motionIconImage.sprite = motionIcon;
        else motionIconImage.sprite = blankIcon;
        if (selectedGame.useMic) micIconImage.sprite = micIcon;
        else micIconImage.sprite = blankIcon;
        if (selectedGame.useProximity) proxIconImage.sprite = proxIcon;
        else proxIconImage.sprite = blankIcon;

        image.sprite = Resources.Load<Sprite>("Screens/" + name);
    }

    public void PlayGame() {
        gameManager.instance.PractiseGame(selectedGame.name);
    }
}

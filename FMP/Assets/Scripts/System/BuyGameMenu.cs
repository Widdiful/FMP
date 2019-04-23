using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyGameMenu : MonoBehaviour {


    public Text titleText;
    public Text costText;
    public Button buyButton;

    public Sprite tapIcon;
    public Sprite motionIcon;
    public Sprite micIcon;
    public Sprite proxIcon;
    public Sprite blankIcon;

    public Image tapIconImage;
    public Image motionIconImage;
    public Image micIconImage;
    public Image proxIconImage;

    Game game;

    public void OpenGame(Game newGame) {
        game = newGame;
        titleText.text = game.displayName;
        costText.text = game.unlockCost.ToString();

        if (newGame.useTap) tapIconImage.sprite = tapIcon;
        else tapIconImage.sprite = blankIcon;
        if (newGame.useMotion) motionIconImage.sprite = motionIcon;
        else motionIconImage.sprite = blankIcon;
        if (newGame.useMic) micIconImage.sprite = micIcon;
        else micIconImage.sprite = blankIcon;
        if (newGame.useProximity) proxIconImage.sprite = proxIcon;
        else proxIconImage.sprite = blankIcon;

        if (gameManager.instance.money < game.unlockCost) {
            buyButton.interactable = false;
        }
    }

    public void BuyGame() {
        if (gameManager.instance.SpendMoney(game.unlockCost)) {
            game.unlocked.easy = true;
            menuManager.instance.OpenPractise();
            SaveData.instance.Save();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MicrogameButton : MonoBehaviour {

    public Game game;
    public Image icon;
    public Image lockSprite;

    void Start() {
        //transform.Find("Image").GetComponent<Image>().sprite = Resources.Load<Sprite>("GameIcons/" + game.name);
        if (!game.IsUnlocked()) {
            lockSprite.enabled = true;
            icon.sprite = game.lockedImage;
        }
        else {
            icon.sprite = game.icon;
        }
    }

    public void PlayGame() {
        menuManager.instance.OpenPractiseMenu();
        //FindObjectOfType<gameManager>().PractiseGame(name);
        if (game.IsUnlocked())
            FindObjectOfType<PracticeMenu>().SelectGame(game);
        else
            menuManager.instance.BuyGame(game);
    }
}

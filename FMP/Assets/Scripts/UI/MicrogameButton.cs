using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MicrogameButton : MonoBehaviour {

    public Game game;
    public Image icon;

    void Start() {
        //transform.Find("Image").GetComponent<Image>().sprite = Resources.Load<Sprite>("GameIcons/" + game.name);
        icon.sprite = game.icon;
        //GetComponent<Button>().interactable = game.IsUnlocked();
    }

    public void PlayGame() {
        menuManager.instance.OpenPractiseMenu();
        //FindObjectOfType<gameManager>().PractiseGame(name);
        FindObjectOfType<PracticeMenu>().SelectGame(game);
    }
}

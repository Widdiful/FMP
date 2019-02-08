using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MicrogameButton : MonoBehaviour {

    public Microgame game;

    void Start() {
        transform.Find("Image").GetComponent<Image>().sprite = Resources.Load<Sprite>("GameIcons/" + game.name);
    }

    public void PlayGame() {
        //FindObjectOfType<gameManager>().PractiseGame(name);
        FindObjectOfType<PracticeMenu>().SelectGame(game);
    }
}

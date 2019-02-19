using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class GameSelect : MonoBehaviour {

    public GameList gameList;
    public GameObject microgameButton;
    //private GameContainer gc;

	void Start () {
        //gc = GameContainer.Load("Engine/Microgames");
        UpdateMenu();
	}

    private void UpdateMenu() {
        // Clears list
        foreach(Transform child in transform) {
            Destroy(child);
        }

        foreach(Game game in gameList.GetAllGames()) {
            MicrogameButton newButton = Instantiate(microgameButton, transform).GetComponent<MicrogameButton>();
            newButton.game = game;
        }
    }
}

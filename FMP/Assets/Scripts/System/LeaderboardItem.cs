using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeaderboardItem : MonoBehaviour {

    public Text nameText;
    public Text scoreText;
    public Text rankText;
    public Image avatar;
    private string playerName;
    private int score;
    private int rank;

    public void UpdateUI(string newName, int newScore, int newRank, int avatarID, string colour) {
        playerName = newName;
        score = newScore;
        rank = newRank;
        nameText.text = playerName;
        scoreText.text = score.ToString();
        rankText.text = rank.ToString();

        colour = "#" + colour;
        Color convertedColour;
        if (ColorUtility.TryParseHtmlString(colour, out convertedColour)) {
            avatar.color = convertedColour;
        }

        if (DatabaseManager.instance.avatars.Count > avatarID)
            avatar.sprite = DatabaseManager.instance.avatars[avatarID];
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DatabaseManager : MonoBehaviour {

    public static string hostURL = "widdiful.co.uk/php/widgamesCollection/";

    public GameObject leaderboardItem;
    public Transform leaderboardTransform;
    public LeaderboardItem playerInfo;
    public int pageNumber;
    public List<Sprite> avatars;

    public string dbLines;
    private bool fetchingComplete = true;
    private const int perPage = 10;
    private List<LeaderboardItem> leaderboardItems = new List<LeaderboardItem>();
    private bool hasUploadedScore;

    public static DatabaseManager instance;

    private void Awake() {
        
        if (!instance)
            instance = this;
        else
            Destroy(this);
    }

    private void Start() {
        for (int i = 0; i < perPage; i++) {
            LeaderboardItem newItem = Instantiate(leaderboardItem, leaderboardTransform).GetComponent<LeaderboardItem>();
            newItem.gameObject.SetActive(false);
            leaderboardItems.Add(newItem);
        }
    }

    public void UpdateScore(int score) {
        StartCoroutine(UploadScore(score));
    }

    public void UpdatePlayerData() {
        StartCoroutine(GetPlayerData(SaveData.userID));
    }

    public void EditUserInfo(string newName, int newAvatar, string newColour) {
        StartCoroutine(EditUser(newName, newAvatar, newColour));
    }

    public void UpdateLeaderboard(int pageNum) {
        foreach (LeaderboardItem item in leaderboardItems) {
            item.gameObject.SetActive(false);
        }
        StartCoroutine(UpdateLeaderboardAsync(pageNum));
    }

    public void ChangePage(int pageNumAdjust) {
        pageNumber += pageNumAdjust;
        if (pageNumber < 1)
            pageNumber = 1;
        UpdateLeaderboard(pageNumber);
    }


    IEnumerator GetPlayerData(string userID) {
        while (!fetchingComplete) {
            yield return new WaitForEndOfFrame();
        }

        fetchingComplete = false;

        WWWForm form = new WWWForm();
        form.AddField("id", userID);

        WWW www = new WWW(hostURL + "GetPlayerStats.php", form);
        yield return www;

        dbLines = www.text;
        fetchingComplete = true;

        string name = GetDBLineValue(dbLines, "name");
        int score = int.Parse(GetDBLineValue(dbLines, "score"));
        int rank = int.Parse(GetDBLineValue(dbLines, "rank"));
        int avatar = int.Parse(GetDBLineValue(dbLines, "avatar"));
        string colour = GetDBLineValue(dbLines, "colour");
        playerInfo.UpdateUI(name, score, rank, avatar, colour);
        ProfileEditor.instance.Initialise(name, avatar, colour);
        pageNumber = 1;
        UpdateLeaderboard(pageNumber);
    }

    IEnumerator EditUser(string newName, int newAvatar, string newColour) {
        while (!fetchingComplete) {
            yield return new WaitForEndOfFrame();
        }

        fetchingComplete = false;

        WWWForm form = new WWWForm();
        form.AddField("id", SaveData.userID);
        form.AddField("name", newName);
        form.AddField("avatar", newAvatar.ToString());
        form.AddField("colour", newColour);

        WWW www = new WWW(hostURL + "EditUser.php", form);
        yield return www;

        dbLines = www.text;
        fetchingComplete = true;
    }

    IEnumerator GetLeaderboardData(int pageNum) {
        while (!fetchingComplete) {
            yield return new WaitForEndOfFrame();
        }

        fetchingComplete = false;

        WWWForm form = new WWWForm();
        form.AddField("pageNum", pageNum);
        form.AddField("perPage", perPage);

        WWW www = new WWW(hostURL + "GetLeaderboards.php", form);
        yield return www;

        dbLines = www.text;
        fetchingComplete = true;
    }

    IEnumerator UpdateLeaderboardAsync(int pageNum) {
        yield return GetLeaderboardData(pageNum);

        string[] lines = dbLines.Split(';');
        for (int i = 0; i < lines.Length - 1; i++) {
            LeaderboardItem newItem = leaderboardItems[i];
            newItem.UpdateUI(GetDBLineValue(lines[i], "name"), int.Parse(GetDBLineValue(lines[i], "score")), int.Parse(GetDBLineValue(lines[i], "rank")), int.Parse(GetDBLineValue(lines[i], "avatar")), GetDBLineValue(lines[i], "colour"));
            newItem.gameObject.SetActive(true);
            if (GetDBLineValue(lines[i], "id") == SaveData.userID) {
                newItem.nameText.text += " (You)";
            }
        }
    }

    IEnumerator UploadScore(int score) {
        while (!fetchingComplete) {
            yield return new WaitForEndOfFrame();
        }

        fetchingComplete = false;

        WWWForm form = new WWWForm();
        form.AddField("id", SaveData.userID);
        form.AddField("score", score);

        WWW www = new WWW(hostURL + "EditScore.php", form);
        yield return www;

        dbLines = www.text;
        fetchingComplete = true;
    }

    public string GetDBLineValue(string line, string index) {
        index += ":";
        string value = line.Substring(line.IndexOf(index) + index.Length);
        if (value.Contains("|")) value = value.Remove(value.IndexOf("|"));
        if (value.EndsWith(";")) value = value.Substring(0, value.Length - 1);
        return value;
    }
}

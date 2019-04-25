using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.UI;

public class SaveData : MonoBehaviour {

    public GameList gameList;

    public static SaveData instance;

    public static string userID;

    private void Awake() {
        if (instance == null) instance = this;
        else if (instance != this) Destroy(this);
    }

    [System.Serializable]
	public class Data {
        public string userID;
        public int money;
        public int score;
        public ScreenOrientation landscapeOrientation;
        public Dictionary<string, Game.UnlockedDifficulties> gameList;
        public Dictionary<InventoryItem.ItemType, int> inventory;
    }

    public void Save() {
        Data data = new Data();
        Dictionary<string, Game.UnlockedDifficulties> unlocks = new Dictionary<string, Game.UnlockedDifficulties>();
        foreach(Game game in gameList.GetAllGames()) {
            unlocks.Add(game.name, game.unlocked);
        }
        data.gameList = unlocks;
        data.inventory = InventoryManager.instance.inventory.items;
        data.money = gameManager.instance.money;
        data.score = gameManager.instance.score;
        data.userID = userID;
        data.landscapeOrientation = gameManager.instance.previousLandscapeOrientation;

        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/saveFile.dat");
        bf.Serialize(file, data);
        file.Position = 0;
        file.Close();
    }

    public void Load() {
        Data data = new Data();
        if (File.Exists(Application.persistentDataPath + "/saveFile.dat")) {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/saveFile.dat", FileMode.Open);
            data = (Data)bf.Deserialize(file);
            file.Position = 0;
            file.Close();

            if (data.gameList != null) {
                foreach (string gameName in data.gameList.Keys) {
                    Game game = gameList.GetGame(gameName);

                    if (game != null) {
                        game.unlocked = data.gameList[gameName];
                    }
                }
            }
            if (data.inventory != null && InventoryManager.instance) {
                InventoryManager.instance.inventory.items = data.inventory;
            }
            gameManager.instance.money = data.money;
            gameManager.instance.score = data.score;
            if (data.landscapeOrientation == ScreenOrientation.LandscapeLeft || data.landscapeOrientation == ScreenOrientation.LandscapeRight)
                gameManager.instance.previousLandscapeOrientation = data.landscapeOrientation;
            userID = data.userID;

            GameObject moneyText = GameObject.Find("Money/MoneyText");
            if (moneyText) {
                Text moneyTextText = moneyText.GetComponent<Text>();
                if (moneyTextText) moneyTextText.text = data.money.ToString();
            }

            GameObject pointsText = GameObject.Find("Points/PointsText");
            if (moneyText) {
                Text pointsTextText = pointsText.GetComponent<Text>();
                if (pointsTextText) pointsTextText.text = data.score.ToString();
            }

            if (DatabaseManager.instance) {
                DatabaseManager.instance.UpdateScore(data.score);
            }
        }
        if (userID == null || userID == "") {
            StartCoroutine(Registration());
        }
    }

    public void ClearData() {
        if (File.Exists(Application.persistentDataPath + "/saveFile.dat")) {
            File.Delete(Application.persistentDataPath + "/saveFile.dat");

            foreach(Game game in gameList.GetAllGames()) {
                game.unlocked = new Game.UnlockedDifficulties();
            }
        }
    }

    public void UnlockAll() {
        foreach (Game game in gameList.GetAllGames()) {
            game.unlocked.easy = true;
            game.unlocked.normal = true;
            game.unlocked.hard = true;
            game.unlocked.relax = true;
        }
        Save();
    }

    IEnumerator Registration() {
        WWW www = new WWW(DatabaseManager.hostURL + "RegisterUser.php");
        yield return www;

        if (www.text.StartsWith("ID")) { // SUCCESSFUL REGISTRATION
            Debug.Log("Registration successful");
            userID = www.text.Replace("ID", "");
            Save();
        }
        else {
            Debug.Log("Registration failed. Error " + www.text);
            var file = File.CreateText(Application.persistentDataPath + "/log.txt");
            file.WriteLine(www.text);
            file.WriteLine("end of log");
            file.WriteLine(www.error);
            file.WriteLine("end of error");
            file.Close();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveData : MonoBehaviour {

    public GameList gameList;

    public static SaveData instance;

    private void Awake() {
        if (instance == null) instance = this;
        else if (instance != this) Destroy(gameObject);
    }

    [System.Serializable]
	public class Data {
        public Dictionary<string, Game.UnlockedDifficulties> gameList;
    }

    public void Save() {
        Data data = new Data();
        Dictionary<string, Game.UnlockedDifficulties> unlocks = new Dictionary<string, Game.UnlockedDifficulties>();
        foreach(Game game in gameList.GetAllGames()) {
            unlocks.Add(game.name, game.unlocked);
        }
        data.gameList = unlocks;

        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/saveFile.dat");
        bf.Serialize(file, data);
        file.Position = 0;
        file.Close();
    }

    public void Load() {
        if (File.Exists(Application.persistentDataPath + "/saveFile.dat")) {
            Data data = new Data();
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/saveFile.dat", FileMode.Open);
            data = (Data)bf.Deserialize(file);
            file.Position = 0;
            file.Close();

            if (data.gameList != null) {
                foreach (string gameName in data.gameList.Keys) {
                    Game game = gameList.GetGame(gameName);
                    game.unlocked = data.gameList[gameName];
                }
            }
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
}

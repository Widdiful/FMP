using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class GameList : ScriptableObject {

    public List<Game> tapGames;
    public List<Game> motionGames;
    public List<Game> micGames;
    public List<Game> proximityGames;

    public Game GetGame(string name) {
        foreach (Game game in GetAllGames()) {
            if (game.name == name) {
                return game;
            }
        }
        return null;
    }

    public List<Game> GetAllGames() {
        List<Game> allGames = new List<Game>();
        allGames.AddRange(tapGames);
        allGames.AddRange(motionGames);
        allGames.AddRange(micGames);
        allGames.AddRange(proximityGames);

        return allGames;
    }
}

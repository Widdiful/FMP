using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gameManager : MonoBehaviour {
    public enum GameTypes { Endless, Challenge, Practice };
    public enum OrientationModes { Portrait, Landscape, Both };
    public enum DifficultyLevels { Chill, Easy, Normal, Hard, Extra };

    private const string path = "Engine/Microgames";

    // Settings
    public OrientationModes orientationMode;
    public bool useMotion;
    public bool useMic;
    public bool useProximity;
    private GameContainer gc;

    // Generic variables
    public GameTypes gameType;
    public DifficultyLevels currentDifficulty;
    private List<Microgame> gamesCompleted = new List<Microgame>();
    private Microgame currentGame;
    private int livesLeft;
    private int gamesSinceLastOrientationChange;
    private bool currentLandscape;

    // Endless variables

    
    // Challenge variables
    public int challengeLength;

    void Start() {
        switch (orientationMode) {
            case OrientationModes.Both:
                currentLandscape = true;
                gamesSinceLastOrientationChange = 5;
                break;
            case OrientationModes.Landscape:
                currentLandscape = true;
                break;
            case OrientationModes.Portrait:
                currentLandscape = false;
                break;
        }
        gc = GameContainer.Load(path);
        LoadGame();
    }

    public void CompleteGame() {
        gamesCompleted.Add(currentGame);
        UnlockGame(currentGame, currentDifficulty);
        LoadGame();
    }

    public void LoadGame() {
        // Change orientation
        switch (orientationMode) {
            case OrientationModes.Landscape:
                currentLandscape = true;
                break;
            case OrientationModes.Portrait:
                currentLandscape = false;
                break;
        }

        if (orientationMode == OrientationModes.Both) {
            gamesSinceLastOrientationChange++;
        }

        bool canChangeOrientation = false;
        if (gamesSinceLastOrientationChange >= 5) {
            canChangeOrientation = true;
        }

        // Manage difficulty
        if (gameType == GameTypes.Challenge) {
            float completionPercentage = (float)gamesCompleted.Count / (float)challengeLength;
            if (completionPercentage >= 0.9f) {
                currentDifficulty = DifficultyLevels.Extra;
            }
            if (completionPercentage >= 0.75f) {
                currentDifficulty = DifficultyLevels.Hard;
            }
            if (completionPercentage >= 0.5f) {
                currentDifficulty = DifficultyLevels.Normal;
            }
            if (completionPercentage >= 0.25f) {
                currentDifficulty = DifficultyLevels.Easy;
            }
        }

        // Generate game list
        GameContainer currentGames = GameContainer.Load(path);
        int id = -1;
        foreach (Microgame game in gc.microgames) {
            id++;
            /*if (gamesCompleted.GetRange(gamesCompleted.Count - 5, gamesCompleted.Count).Contains(game)) {
                currentGames.microgames.RemoveAt(id);
                id--;
            }

            else */
            if (currentDifficulty == DifficultyLevels.Extra && !game.hasExtra) {
                currentGames.microgames.RemoveAt(id);
                id--;
            }

            else if (!canChangeOrientation && game.isLandscape != currentLandscape) {
                currentGames.microgames.RemoveAt(id);
                id--;
            }

            else if (!useMotion && game.useMotion) {
                currentGames.microgames.RemoveAt(id);
                id--;
            }

            else if (!useMic && game.useMic) {
                currentGames.microgames.RemoveAt(id);
                id--;
            }

            else if (!useProximity && game.useProximity) {
                currentGames.microgames.RemoveAt(id);
                id--;
            }
        }
        Microgame nextGame = currentGames.microgames[Random.Range(0, currentGames.microgames.Count)];
        SceneManager.LoadScene("Scenes/" + nextGame.name);
    }

    public void UnlockGame(Microgame game, DifficultyLevels difficulty) {

    }
}

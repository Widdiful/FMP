using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class gameManager : MonoBehaviour {

    public static gameManager instance = null;

    public enum GameTypes { Endless, Challenge, Practice };
    public enum OrientationModes { Both, Landscape, Portrait};
    public enum DifficultyLevels { Relax, Easy, Normal, Hard, Extra };

    private const string path = "Engine/Microgames";

    public GameList gameList;

    // Settings
    public OrientationModes orientationMode;
    public bool useMotion;
    public bool useMic;
    public bool useProximity;
    private GameContainer gc;
    public int money;
    public float hintScreenDuration = 1;

    // Generic variables
    public GameTypes gameType;
    public DifficultyLevels currentDifficulty;
    public int totalLives;
    public List<Game> gamesCompleted = new List<Game>();
    private List<Game> gamesPlayed = new List<Game>();
    private List<Game> gamesQueue = new List<Game>();
    public Game currentGame;
    public int livesLeft;
    private int gamesSinceLastOrientationChange;
    private bool currentLandscape;
    public bool startingGame;
    public bool endingGame;
    private bool completedGame;
    private bool failedGame;
    private float startTimer;
    private float endTimer;
    public float gameSpeed = 1;
    public float moneyMultiplier = 1;

    // Endless variables


    // Challenge variables
    public int challengeLength;

    void Awake() {
        if (instance == null) instance = this;
        else if (instance != this) Destroy(this);

        DontDestroyOnLoad(gameObject);

        if (SaveData.instance) SaveData.instance.Load();
    }

    void Start() {
        gc = GameContainer.Load(path);
    }

    void Update() {
        if (startingGame || endingGame) {
            GameObject bg = GameObject.Find("Timer/Canvas/Background");
            if (bg) {
                if (startingGame) {
                    startTimer += Time.unscaledDeltaTime;
                    if (startTimer >= hintScreenDuration) {
                        bg.transform.localPosition = Vector3.Lerp(bg.transform.localPosition, new Vector3(1600, 0, 0), 0.2f);
                        Time.timeScale = gameSpeed;
                        if (1600 - bg.transform.localPosition.x <= 0.01) {
                            bg.transform.localPosition = new Vector3(1600, 0, 0);
                            startingGame = false;
                            startTimer = 0;
                            //if (GameObject.Find("HintText")) GameObject.Find("HintText").SetActive(false);
                        }
                    }
                }
                else if (endingGame) {
                    if (completedGame && endTimer == 0)
                        endTimer = 1.0f;
                    endTimer -= Time.deltaTime;

                    if (endTimer < 0) {
                        RectTransform bgRT = bg.GetComponent<RectTransform>();
                        bgRT.offsetMin = Vector2.Lerp(bgRT.offsetMin, new Vector2(0, 0), 0.2f);
                        bgRT.offsetMax = Vector2.Lerp(bgRT.offsetMax, new Vector2(0, 0), 0.2f);
                        if (Mathf.Abs(0 - bgRT.offsetMin.x) <= 0.01) {
                            bgRT.offsetMin = new Vector2(0, 0);
                            bgRT.offsetMax = new Vector2(0, 0);
                            endingGame = false;
                            endTimer = 0;
                            Time.timeScale = 0.0001f;

                            if (completedGame) {
                                LoadGame();
                                completedGame = false;
                                if (currentLandscape) {
                                    Screen.orientation = ScreenOrientation.LandscapeRight;
                                    Screen.autorotateToLandscapeLeft = true;
                                    Screen.autorotateToLandscapeRight = true;
                                    Screen.autorotateToPortrait = false;
                                    Screen.autorotateToPortraitUpsideDown = false;
                                }
                                else {
                                    Screen.orientation = ScreenOrientation.Portrait;
                                    Screen.autorotateToLandscapeLeft = false;
                                    Screen.autorotateToLandscapeRight = false;
                                    Screen.autorotateToPortrait = true;
                                    Screen.autorotateToPortraitUpsideDown = true;
                                }
                                Screen.orientation = ScreenOrientation.AutoRotation;
                            }
                            else if (failedGame) {
                                failedGame = false;
                                if (livesLeft > 0) {
                                    LoadGame();
                                }
                                else {
                                    GameOver();
                                }
                            }
                        }
                    }
                }
            }
        }
    }

    public void StartGame() {
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
        gameSpeed = 1;
        moneyMultiplier = 1;
        Time.timeScale = 0.0001f;
        livesLeft = totalLives;
        gamesCompleted.Clear();
        if (InventoryManager.instance)
            InventoryManager.instance.UseItems();
        LoadGame();
    }

    public void GameOver() {
        SceneManager.LoadScene("Scenes/GameOver");
        startingGame = true;
    }

    public void CompleteGame() {
        if (!endingGame) {
            gamesCompleted.Add(currentGame);
            gamesPlayed.Add(currentGame);
            UnlockGame(currentGame, currentDifficulty);
            endingGame = true;
            completedGame = true;

            if (gamesCompleted.Count % 5 == 0) {
                gameSpeed += 0.1f;
            }
        }
    }

    public void FailGame() {
        if (!endingGame) {
            gamesPlayed.Add(currentGame);
            if (!GameObject.FindObjectOfType<RotateManager>()) livesLeft--;
            endingGame = true;
            failedGame = true;
        }
    }

    public void LoadGame() {
        if (gameType == GameTypes.Practice) {
            SceneManager.LoadScene("Scenes/" + currentGame.name);
            currentLandscape = currentGame.isLandscape;
        }
        else {
            if (gamesQueue.Count == 0) {
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
                    gamesSinceLastOrientationChange = 0;
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
                List<Game> currentGames = gameList.GetAllGames();
                int id = -1;
                foreach (Game game in gameList.GetAllGames()) {
                    id++;
                    if (gamesPlayed.Count > 0) {
                        if (gamesPlayed[gamesPlayed.Count - 1].name == game.name) {
                            currentGames.RemoveAt(id);
                            id--;
                        }
                    }

                    if (currentDifficulty == DifficultyLevels.Extra && !game.hasExtra) {
                        currentGames.RemoveAt(id);
                        id--;
                    }

                    else if (!canChangeOrientation && game.isLandscape != currentLandscape) {
                        currentGames.RemoveAt(id);
                        id--;
                    }

                    else if (!useMotion && game.useMotion) {
                        currentGames.RemoveAt(id);
                        id--;
                    }

                    else if (!useMic && game.useMic) {
                        currentGames.RemoveAt(id);
                        id--;
                    }

                    else if (!useProximity && game.useProximity) {
                        currentGames.RemoveAt(id);
                        id--;
                    }
                }

                Game nextGame = currentGames[Random.Range(0, currentGames.Count)];
                currentGame = nextGame;
                gamesQueue.Add(nextGame);
            }


            if (gamesQueue[0].isLandscape && !currentLandscape) {
                SceneManager.LoadScene("Scenes/Motion/RotateHorizontal");
                currentLandscape = true;
            }
            else if (!gamesQueue[0].isLandscape && currentLandscape) {
                SceneManager.LoadScene("Scenes/Motion/RotateVertical");
                currentLandscape = false;
            }
            else {
                SceneManager.LoadScene("Scenes/" + gamesQueue[0].name);
                gamesQueue.Remove(gamesQueue[0]);
            }
        }
        startingGame = true;
    }

    public void UnlockGame(Game game, DifficultyLevels difficulty) {
        switch (difficulty) {
            case DifficultyLevels.Easy:
                game.unlocked.easy = true;
                break;
            case DifficultyLevels.Normal:
                game.unlocked.normal = true;
                break;
            case DifficultyLevels.Hard:
                game.unlocked.hard = true;
                break;
            case DifficultyLevels.Relax:
                game.unlocked.relax = true;
                break;
        }
    }

    public void PractiseGame(string gameName) {
        gameType = GameTypes.Practice;
        livesLeft = 1;
        gamesCompleted.Clear();
        currentGame = gameList.GetGame(gameName);
        SceneManager.LoadScene("Scenes/" + gameName);
        startingGame = true;
        currentLandscape = currentGame.isLandscape;
        Time.timeScale = 0.0001f;

        if (currentLandscape) {
            Screen.orientation = ScreenOrientation.LandscapeRight;
            Screen.autorotateToLandscapeLeft = true;
            Screen.autorotateToLandscapeRight = true;
            Screen.autorotateToPortrait = false;
            Screen.autorotateToPortraitUpsideDown = false;
        }
        else {
            Screen.orientation = ScreenOrientation.Portrait;
            Screen.autorotateToLandscapeLeft = false;
            Screen.autorotateToLandscapeRight = false;
            Screen.autorotateToPortrait = true;
            Screen.autorotateToPortraitUpsideDown = true;
        }
        Screen.orientation = ScreenOrientation.AutoRotation;
    }

    public bool SpendMoney(int amount) {
        bool result = false;
        if (money >= amount) {
            result = true;
            money -= amount;
            PlayerPrefs.SetInt("money", money);
            GameObject.Find("Shop/MoneyText").GetComponent<Text>().text = money.ToString();
        }
        return result;
    }
}

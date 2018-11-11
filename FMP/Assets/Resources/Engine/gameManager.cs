using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class gameManager : MonoBehaviour {

    public static gameManager instance = null;

    public enum GameTypes { Endless, Challenge, Practice };
    public enum OrientationModes { Both, Landscape, Portrait};
    public enum DifficultyLevels { Chill, Easy, Normal, Hard, Extra };

    private const string path = "Engine/Microgames";

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
    public List<Microgame> gamesCompleted = new List<Microgame>();
    private List<Microgame> gamesPlayed = new List<Microgame>();
    private List<Microgame> gamesQueue = new List<Microgame>();
    public Microgame currentGame;
    public int livesLeft;
    private int gamesSinceLastOrientationChange;
    private bool currentLandscape;
    public bool startingGame;
    public bool endingGame;
    private bool completedGame;
    private bool failedGame;
    private float startTimer;
    private float endTimer;

    // Endless variables


    // Challenge variables
    public int challengeLength;

    void Awake() {
        if (instance == null) instance = this;
        else if (instance != this) Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    void Start() {
        gc = GameContainer.Load(path);
    }

    void Update() {
        if (startingGame || endingGame) {
            GameObject bg = GameObject.Find("Timer/Canvas/Background");
            if (bg) {
                if (startingGame) {
                    startTimer += Time.deltaTime;
                    if (startTimer >= hintScreenDuration) {
                        bg.transform.localPosition = Vector3.Lerp(bg.transform.localPosition, new Vector3(1600, 0, 0), 0.2f);
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

                            if (completedGame) {
                                LoadGame();
                                completedGame = false;
                                if (currentLandscape) Screen.orientation = ScreenOrientation.LandscapeRight;
                                else Screen.orientation = ScreenOrientation.Portrait;
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
        livesLeft = totalLives;
        gamesCompleted.Clear();
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
                if (gamesPlayed.Count > 0) {
                    if (gamesPlayed[gamesPlayed.Count - 1].name == game.name) {
                        currentGames.microgames.RemoveAt(id);
                        id--;
                    }
                }

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
        startingGame = true;
    }

    public void UnlockGame(Microgame game, DifficultyLevels difficulty) {

    }
}

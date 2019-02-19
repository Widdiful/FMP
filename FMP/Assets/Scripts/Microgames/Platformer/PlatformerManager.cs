﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformerManager : MonoBehaviour {

    public gameManager.DifficultyLevels difficulty;
    public GameObject enemy;

    private gameManager gm;
    private Timer timer;
    private int enemiesToSpawn;
    private int enemiesSpawned;
    private bool cleared;

	void Start () {
        gm = GameObject.FindObjectOfType<gameManager>();
        timer = GameObject.FindObjectOfType<Timer>();
        if (gm) {
            difficulty = gm.currentDifficulty;
        }

        switch (difficulty) {
            case gameManager.DifficultyLevels.Relax:
                enemiesToSpawn = 1;
                break;
            case gameManager.DifficultyLevels.Easy:
                enemiesToSpawn = 1;
                if (timer) {
                    timer.seconds = 5;
                    timer.timer = timer.seconds;
                }
                break;
            case gameManager.DifficultyLevels.Normal:
                enemiesToSpawn = 3;
                if (timer) {
                    timer.seconds = 7;
                    timer.timer = timer.seconds;
                }
                break;
            case gameManager.DifficultyLevels.Hard:
                enemiesToSpawn = 5;
                if (timer) {
                    timer.seconds = 10;
                    timer.timer = timer.seconds;
                }
                break;
            case gameManager.DifficultyLevels.Extra:
                enemiesToSpawn = 7;
                if (timer) {
                    timer.seconds = 10;
                    timer.timer = timer.seconds;
                }
                break;
        }
        SpawnEnemies();
	}

	void Update () {
        if (enemiesSpawned > 0 && GameObject.FindGameObjectsWithTag("Enemy").Length == 0) {
            if (difficulty != gameManager.DifficultyLevels.Relax && !cleared) {
                cleared = true;
                gm.CompleteGame();
            }
            else if (!cleared) {
                enemiesToSpawn++;
                SpawnEnemies();
            }
        }
	}

    void SpawnEnemies() {
        List<Transform> spawnPoints = new List<Transform>();
        enemiesSpawned = 0;

        foreach (GameObject trans in GameObject.FindGameObjectsWithTag("Spawn")) {
            spawnPoints.Add(trans.transform);
        }

        for (int i = 0; i < Mathf.Clamp(enemiesToSpawn, 0, spawnPoints.Count); i++) {
            int rand = Random.Range(0, spawnPoints.Count);
            Transform spawnPoint = spawnPoints[rand];
            Instantiate(enemy, spawnPoint);
            spawnPoints.RemoveAt(rand);
            enemiesSpawned++;
        }
    }
}

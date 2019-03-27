using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightMonsterManager : MonoBehaviour {

    public List<Transform> heroes = new List<Transform>();
    public List<Transform> enemies = new List<Transform>();
    public List<float> enemyHP = new List<float>();
    public float damage;

    private gameManager gm;
    private List<int> attacking = new List<int>();
    private List<Vector3> startPos = new List<Vector3>();
    private int turnOrder;
    private bool cleared;
    private AudioSource audioSource;
    public AudioClip startAttackSound;
    public List<AudioClip> attackSounds;

    void Start() {
        gm = GameObject.FindObjectOfType<gameManager>();
        audioSource = GetComponent<AudioSource>();
        foreach (Transform hero in heroes) {
            attacking.Add(-1);
            startPos.Add(hero.position);
        }
    }
	
	void Update () {
        for (int i = 0; i < heroes.Count;  i++) {
            if (attacking[i] >= 0) {
                if (enemyHP[attacking[i]] > 0) {
                    heroes[i].position = Vector3.Lerp(heroes[i].position, enemies[attacking[i]].position, Time.deltaTime * 10);
                    if (Vector3.Distance(heroes[i].position, enemies[attacking[i]].position) <= 0.1f) {
                        enemyHP[attacking[i]] -= damage;
                        audioSource.clip = attackSounds[Random.Range(0, attackSounds.Count)];
                        audioSource.Play();
                        if (enemyHP[attacking[i]] <= 0) {
                            enemies[attacking[i]].GetComponent<Monster>().Kill();
                        }
                        attacking[i] = -1;
                    }
                }
                else {
                    attacking[i] = -1;
                }
            }
            else {
                heroes[i].position = Vector3.Lerp(heroes[i].position, startPos[i], Time.deltaTime * 10);
            }
        }

        bool wincheck = true;
        foreach (Transform enemy in enemies) {
            if (enemy.GetComponent<Monster>().alive) {
                wincheck = false;
            }
        }
        if (wincheck && !cleared) {
            gm.CompleteGame();
            cleared = true;
        }
	}

    public void Attack(int enemyID) {
        if (Vector3.Distance(heroes[turnOrder].position, startPos[turnOrder]) < 0.1f) {
            attacking[turnOrder] = enemyID;
            turnOrder = (turnOrder + 1) % heroes.Count;
        }
    }
}

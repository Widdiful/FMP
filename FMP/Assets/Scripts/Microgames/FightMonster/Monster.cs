using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour {

    public bool alive = true;
    private FightMonsterManager fmm;
    private int enemyID;

    void Start() {
        fmm = GameObject.FindObjectOfType<FightMonsterManager>();
        for (int i = 0; i > fmm.enemies.Count; i++) {
            if (fmm.enemies[i] == gameObject) {
                enemyID = i;
                break;
            }
        }
    }

    public void Kill() {
        gameObject.GetComponent<Renderer>().enabled = false;
        foreach (Transform child in transform) {
            if (child.GetComponent<Renderer>()) child.GetComponent<Renderer>().enabled = false;
        }
        alive = false;
    }

    public void OnTap() {
        fmm.Attack(enemyID);
    }
}

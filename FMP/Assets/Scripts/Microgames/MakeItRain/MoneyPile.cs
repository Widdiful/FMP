using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyPile : MonoBehaviour {

    public List<GameObject> moneys = new List<GameObject>();
    public ParticleSystem particles;
    public int requiredMoney;

    float baseHeight;
    int moneyThrown;
    float touchStartY;
    bool canThrow;

    private void Start() {
        baseHeight = transform.localScale.y;
    }

    private void Update() {
        if (Time.timeScale > 0) {
            if (moneyThrown < requiredMoney && Input.touchCount > 0) {
                if (Input.touches[0].phase == TouchPhase.Began) {
                    touchStartY = Input.touches[0].position.y / Screen.height;
                    canThrow = true;
                }
                if (Input.touches[0].phase == TouchPhase.Moved) {
                    float touchCurrentY = Input.touches[0].position.y / Screen.height;
                    if (Mathf.Abs(touchCurrentY - touchStartY) > 0.1f && canThrow) {
                        ThrowMoney();
                        canThrow = false;
                    }
                }
            }
        }
    }

    private void ThrowMoney() {
        foreach(GameObject money in moneys) {
            if (!money.activeInHierarchy) {
                money.SetActive(true);
                break;
            }
        }

        moneyThrown++;

        ParticleSystem.EmissionModule emission = particles.emission;
        emission.rateOverTime = ((float)moneyThrown / (float)requiredMoney) * 20;

        transform.localScale = new Vector3(transform.localScale.x, (1.0f - ((float)moneyThrown / (float)requiredMoney)) * baseHeight, transform.localScale.z);
        if (moneyThrown == requiredMoney) {
            GetComponentInChildren<Renderer>().enabled = false;
            FindObjectOfType<gameManager>().CompleteGame();
        }
    }
}

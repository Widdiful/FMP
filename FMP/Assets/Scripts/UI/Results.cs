using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Results : MonoBehaviour {

    public int noCleared;
    private int money;
    public gameManager.DifficultyLevels difficulty;
    public List<Sprite> results = new List<Sprite>();

    private gameManager gm;
    private float startTimer;
    private Text clearedCount;
    private int clearInt;
    private Text moneyEarned;
    private int moneyInt;
    private Image result;
    private Image sparkle;

	void Start () {
        gm = GameObject.FindObjectOfType<gameManager>();
        if (gm) {
            noCleared = gm.gamesCompleted.Count;
        }
        clearedCount = GameObject.Find("ClearedCount").GetComponent<Text>();
        moneyEarned = GameObject.Find("MoneyEarned").GetComponent<Text>();
        result = GameObject.Find("Ranking").GetComponent<Image>();
        result.enabled = false;
        sparkle = GameObject.Find("Sparkle").GetComponent<Image>();
        sparkle.enabled = false;

        if (noCleared >= 30) result.sprite = results[6];
        else if (noCleared >= 25) result.sprite = results[5];
        else if (noCleared >= 20) result.sprite = results[4];
        else if (noCleared >= 15) result.sprite = results[3];
        else if (noCleared >= 10) result.sprite = results[2];
        else if (noCleared >= 5) result.sprite = results[1];
        else if (noCleared >= 0) result.sprite = results[0];

        //switch (difficulty) {
        //    case gameManager.DifficultyLevels.Relax:
        //        money = 0;
        //        break;
        //    case gameManager.DifficultyLevels.Easy:
        //        money = noCleared * 5;
        //        break;
        //    case gameManager.DifficultyLevels.Normal:
        //        money = noCleared * 10;
        //        break;
        //    case gameManager.DifficultyLevels.Hard:
        //        money = noCleared * 15;
        //        break;
        //    case gameManager.DifficultyLevels.Extra:
        //        money = noCleared * 20;
        //        break;
        //}
        money = noCleared * 5;
        gm.money += (int)((float)money * gm.moneyMultiplier);
        gm.score += money;
        //PlayerPrefs.SetInt("money", gm.money);
        SaveData.instance.Save();
    }
	
	void Update () {
        if (startTimer >= 1) {
            if (clearInt < noCleared) {
                clearInt++;
                if ((Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began) || Input.GetMouseButtonDown(0)) {
                    clearInt = noCleared;
                }
                if (clearInt < 1000000)
                    clearedCount.text = clearInt.ToString();
                else
                    clearedCount.text = "Loads";
            }
            else if (moneyInt < money) {
                moneyInt++;
                if ((Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began) || Input.GetMouseButtonDown(0)) {
                    moneyInt = money;
                }
                if (moneyInt < 1000000)
                    moneyEarned.text = moneyInt.ToString();
                else
                    moneyEarned.text = "Loads";
            }
            else {
                result.enabled = true;
                result.GetComponent<RectTransform>().sizeDelta = Vector2.Lerp(result.GetComponent<RectTransform>().sizeDelta, new Vector2(200, 200), 0.05f);
                result.color = Color.Lerp(result.color, Color.white, 0.05f);
                if (result.color.a >= 0.8f && result.sprite == results[6]) {
                    sparkle.enabled = true;
                    sparkle.GetComponent<RectTransform>().localPosition = Vector2.Lerp(sparkle.GetComponent<RectTransform>().localPosition, new Vector2(50, 68.5f), 0.01f);
                    sparkle.GetComponent<RectTransform>().Rotate(new Vector3(0, 0, -1));
                    sparkle.color = Color.Lerp(sparkle.color, new Color(255, 255, 255, 0), 0.01f);
                }
            }
        }
        else {
            startTimer += Time.deltaTime;
        }
    }
}

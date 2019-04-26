using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LanguageChanger : MonoBehaviour {

    public string TextName;
    private Text text;
	
	void Start () {
        text = GetComponent<Text>();
        LanguageManager.instance.AddChanger(this);
        UpdateUI();
	}

    public void UpdateUI() {
        if (text) {
            string line = LanguageManager.instance.GetLine(TextName);
            if (line != "") {
                text.text = line;
            }
        }
    }
}

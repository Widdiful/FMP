using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LanguageChanger : MonoBehaviour {

    public string TextName;
	
	void Start () {
        LanguageManager.instance.AddChanger(this);
        UpdateUI();
	}

    public void UpdateUI() {
        string line = LanguageManager.instance.GetLine(TextName);
        GetComponent<Text>().text = line;
    }
}

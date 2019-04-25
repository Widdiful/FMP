using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanguageManager : MonoBehaviour {

    public static LanguageManager instance;
    public enum Languages { English, Japanese };
    public Languages language;
    private LanguageContainer lc;
    private List<LanguageChanger> languageChangers = new List<LanguageChanger>();

    private void Awake() {
        if (instance == null)
            instance = this;
        else
            Destroy(this);

        lc = LanguageContainer.Load("Engine/Languages");
    }

    public string GetLine(string lineName) {
        TextLine line = lc.GetLine(lineName);
        string result = "";

        if (line != null) {
            switch (language) {
                case Languages.English:
                    result = line.english;
                    break;
                case Languages.Japanese:
                    result = line.japanese;
                    break;
            }
        }

        return result;
    }

    public void AddChanger(LanguageChanger lc) {
        languageChangers.Add(lc);
    }

    public void UpdateAll() {
        foreach(LanguageChanger lc in languageChangers) {
            if (lc)
                lc.UpdateUI();
        }
    }
	
}

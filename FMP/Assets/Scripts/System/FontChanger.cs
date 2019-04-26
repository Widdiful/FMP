using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FontChanger : MonoBehaviour {

    private Dictionary<Text, FontInfo> defaultData = new Dictionary<Text, FontInfo>();

    public static FontChanger instance;

    private void Awake() {
        if (!instance)
            instance = this;
        else
            Destroy(this);
    }

    private class FontInfo {
        public Font font;
        public int size;
    }

    public void Start() {
        SceneManager.activeSceneChanged += ChangedActiveScene;
        ChangeFonts();
    }

    private void ChangedActiveScene(Scene current, Scene next) {
        ChangeFonts();
    }

    private void ChangeFonts() {
        List<Text> texts = new List<Text>();
        SceneManager.GetActiveScene().GetRootGameObjects().ToList().ForEach(i => texts.AddRange(i.GetComponentsInChildren<Text>(true)));
        if (gameManager.instance.currentFont) {
            foreach (Text text in texts) {
                if (!defaultData.ContainsKey(text)) {
                    FontInfo info = new FontInfo();
                    info.font = text.font;
                    info.size = text.fontSize;
                    defaultData[text] = info;
                }

                text.font = gameManager.instance.currentFont.font;
                text.fontSize = (int)((float)defaultData[text].size * gameManager.instance.currentFont.scale);
            }
        }
        else {
            foreach (Text text in texts) {
                if (defaultData.ContainsKey(text)) {
                    text.font = defaultData[text].font;
                    text.fontSize = defaultData[text].size;
                }
            }
        }
    }
}

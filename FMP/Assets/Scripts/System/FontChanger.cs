using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FontChanger : MonoBehaviour {

    // Font and font size for default text setting
    private class TextInfo {
        public Font font;
        public int size;
    }

    private Dictionary<Text, TextInfo> defaultData = new Dictionary<Text, TextInfo>();

    public static FontChanger instance;

    public FontData currentFont;
    public List<FontData> fontData = new List<FontData>();

    private void Awake() {
        if (!instance)
            instance = this;
        else
            Destroy(this);
    }

    public void Start() {
        SceneManager.activeSceneChanged += ChangedActiveScene;
        StartCoroutine(DelayedChangeFonts());
    }

    private void ChangedActiveScene(Scene current, Scene next) {
        StartCoroutine(DelayedChangeFonts());
    }

    // Change font and font size of all text according to selected font
    private void ChangeFonts() {
        List<Text> texts = new List<Text>();
        SceneManager.GetActiveScene().GetRootGameObjects().ToList().ForEach(i => texts.AddRange(i.GetComponentsInChildren<Text>(true)));
        if (currentFont) {
            foreach (Text text in texts) {
                if (!defaultData.ContainsKey(text)) {
                    TextInfo info = new TextInfo();
                    info.font = text.font;
                    info.size = text.fontSize;
                    defaultData[text] = info;
                }

                text.font = currentFont.font;
                text.fontSize = (int)((float)defaultData[text].size * currentFont.scale);
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

    // Change fonts after one frame to set pooled items
    IEnumerator DelayedChangeFonts() {
        yield return new WaitForEndOfFrame();
        ChangeFonts();
    }

    public void SetFont(FontData font) {
        currentFont = font;
        ChangeFonts();
    }

    public void SetFont(int index) {
        if (index >= 0 && index < fontData.Count) {
            currentFont = fontData[index];
        }
        else {
            currentFont = null;
        }
        ChangeFonts();
    }
}

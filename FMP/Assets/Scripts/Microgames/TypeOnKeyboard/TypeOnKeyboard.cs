using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TypeOnKeyboard : MonoBehaviour {

    public List<string> messages = new List<string>();
    public Text text;
    int lettersTyped;
    string message;
    bool complete;
    AudioSource audioSource;

    private void Start() {
        message = messages[Random.Range(0, messages.Count)];
        audioSource = GetComponent<AudioSource>();
    }

    public void Type() {
        if (!complete) {
            if (audioSource)
                audioSource.Play();
            if (text.text.Length < message.Length) {
                text.text += message[lettersTyped];
                StartCoroutine(ClickEffect(GameObject.Find(message[lettersTyped].ToString())));
                lettersTyped++;
            }
            else {
                complete = true;
                gameManager.instance.CompleteGame();
            }
        }
    }

    IEnumerator ClickEffect(GameObject obj) {
        if (obj) {
            Image img = obj.GetComponent<Image>();
            if (img) {
                img.color = new Color(0.9f, 0.9f, 0.9f);
                yield return new WaitForSeconds(0.05f);
                img.color = new Color(1, 1, 1);
            }
        }
    }
}

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

    private void Start() {
        message = messages[Random.Range(0, messages.Count)];
    }

    public void Type() {
        if (!complete) {
            if (text.text.Length < message.Length) {
                text.text += message[lettersTyped];
                lettersTyped++;
            }
            else {
                complete = true;
                gameManager.instance.CompleteGame();
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CodeManager : MonoBehaviour {

    public string code;
    public string inputtedCode;
    public bool allowMultiple;
    public Text inputField, hintField;
    int codeLength = 4;
    List<string> valueStrings = new List<string>();

    private void Start() {
        for(int i = 1; i <= codeLength; i++) {
            valueStrings.Add(i.ToString());
        }

        for (int i = 0; i < codeLength; i++) {
            int rand = Random.Range(0, valueStrings.Count);
            code += valueStrings[rand];
            if (allowMultiple)
                valueStrings.RemoveAt(rand);
        }

        hintField.text = code;
    }

    public void InputCode(int number) {
        inputtedCode += number;
        inputField.text = inputtedCode;

        if (inputtedCode.Length == codeLength) {
            if (code == inputtedCode)
                gameManager.instance.CompleteGame();
            //else
                //gameManager.instance.FailGame();
        }
    }

    public void Backspace() {
        inputtedCode = inputtedCode.Substring(0, inputtedCode.Length - 1);
        inputField.text = inputtedCode;
    }
}

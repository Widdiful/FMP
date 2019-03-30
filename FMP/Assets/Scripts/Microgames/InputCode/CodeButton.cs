using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CodeButton : MonoBehaviour {

    public int value;
    public bool backspace;
    public CodeManager codeManager;
    bool pushed;
    float startY;
    AudioSource audioSource;

    private void Start() {
        startY = transform.localPosition.y;
        audioSource = GetComponent<AudioSource>();
    }

    void Update () {
		if (!pushed && transform.localPosition.y >= 0) {
            pushed = true;

            if (backspace)
                codeManager.Backspace();
            else
                codeManager.InputCode(value);

            audioSource.Play();
        }

        if (pushed && transform.localPosition.y < startY / 2) {
            pushed = false;
        }
	}
}

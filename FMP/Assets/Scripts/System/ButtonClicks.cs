using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonClicks : MonoBehaviour {

    public AudioSource audioSource;

    private void Start() {
        StartCoroutine(AddClicks());
    }

    IEnumerator AddClicks() {
        yield return new WaitForEndOfFrame();
        List<Button> buttons = new List<Button>();
        SceneManager.GetActiveScene().GetRootGameObjects().ToList().ForEach(i => buttons.AddRange(i.GetComponentsInChildren<Button>(true)));

        foreach(Button button in buttons) {
            button.onClick.AddListener(() => Click());
        }
    }

    public void Click() {
        audioSource.Play();
    }
}

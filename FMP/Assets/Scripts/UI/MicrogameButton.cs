using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MicrogameButton : MonoBehaviour {

    public string displayName;
    public string name;
    public bool isLandscape;
    public bool useTap;
    public bool useMotion;
    public bool useMic;
    public bool useProximity;
    public bool hasExtra;

    void Start() {
        transform.Find("Image").GetComponent<Image>().sprite = Resources.Load<Sprite>("GameIcons/" + name);
    }

    public void PlayGame() {
        FindObjectOfType<gameManager>().PractiseGame(name);
    }
}

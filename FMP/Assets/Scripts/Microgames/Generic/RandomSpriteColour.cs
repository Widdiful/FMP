using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpriteColour : MonoBehaviour {

    private void Start() {
        GetComponent<SpriteRenderer>().color = Color.HSVToRGB(Random.Range(0.0f, 1.0f), 1, 1);
    }
}

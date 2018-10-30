using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundColourChange : MonoBehaviour {

    public float increment;
    public bool randomAtStart;
    private SpriteRenderer sprite;

    void Start() {
        sprite = GetComponent<SpriteRenderer>();
        if (randomAtStart) {
            sprite.color = Color.HSVToRGB(Random.Range(0.0f, 1.0f), 0.5f, 0.75f);
        }
    }

	void Update () {
        float H, S, V;
        Color.RGBToHSV(sprite.color, out H, out S, out V);
        H = (H + increment) % 1;
        sprite.color = Color.HSVToRGB(H, S, V);
	}
}

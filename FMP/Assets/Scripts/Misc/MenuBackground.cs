using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuBackground : MonoBehaviour {

    RectTransform rect;
    Vector2 startingRect;
    public float moveSpeed;

    private void Start() {
        rect = GetComponent<RectTransform>();
        startingRect = rect.anchoredPosition;
    }

    void Update () {
        Vector2 newPosition = rect.anchoredPosition + new Vector2(moveSpeed * Time.deltaTime, 0);
        if (newPosition.x >= 400)
            newPosition = startingRect;
        rect.anchoredPosition = newPosition;
	}
}

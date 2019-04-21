using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterDroplet : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.name == "DeathZone")
            gameObject.SetActive(false);
    }
}

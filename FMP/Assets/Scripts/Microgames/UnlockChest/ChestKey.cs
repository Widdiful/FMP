using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestKey : MonoBehaviour {

    public Vector3 pickupOffset;
    public GameObject particles;
    Transform target;
    bool complete;
    bool pickedUp;

    private void LateUpdate() {
        if (target) {
            transform.position = target.position + pickupOffset;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Player") && !pickedUp) {
            transform.parent = null;
            target = collision.transform;
            transform.GetComponent<CircleCollider2D>().enabled = false;
            transform.GetComponent<BoxCollider2D>().enabled = true;
            pickedUp = true;
        }
        else if (collision.name == "Chest" && !complete) {
            complete = true;
            HideKey();
            particles.SetActive(true);
            gameManager.instance.CompleteGame();
        }
    }

    public void HideKey() {
        GetComponent<SpriteRenderer>().enabled = false;
    }
}

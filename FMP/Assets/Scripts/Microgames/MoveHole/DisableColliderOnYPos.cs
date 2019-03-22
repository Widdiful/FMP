using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableColliderOnYPos : MonoBehaviour {

    public float killY;
    Collider[] colliders;
    public bool disabled;

    private void Start() {
        colliders = GetComponents<Collider>();
    }

    void Update () {
		if (!disabled && transform.position.y <= killY) {
            disabled = true;
            foreach(Collider collider in colliders) {
                collider.enabled = false;
            }

            bool complete = true;
            foreach(DisableColliderOnYPos i in FindObjectsOfType<DisableColliderOnYPos>()) {
                if (!i.disabled) {
                    complete = false;
                }
            }

            if (complete) {
                gameManager.instance.CompleteGame();
            }
        }
	}
}

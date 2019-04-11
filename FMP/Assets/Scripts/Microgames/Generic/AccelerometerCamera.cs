using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccelerometerCamera : MonoBehaviour {

    public float speed;
    public Transform anchor;
    public Transform characterAnchor;
    public Transform character;
    Quaternion rot;
    bool scared;

    private void Start() {
        rot = new Quaternion(0, 0, 1, 0);

        Input.gyro.enabled = false;
        Input.gyro.enabled = true;
    }

    void Update () {
        transform.localRotation = Input.gyro.attitude * rot;

        if (!scared) {
            characterAnchor.Rotate(0, 0, speed * Time.deltaTime);

            if (MicManager.instance.levelMax >= 0.75f) {
                RaycastHit hit;
                if (Physics.SphereCast(transform.position, 1.0f, transform.forward, out hit, 10)) {
                    if (hit.transform.CompareTag("Enemy")) {
                        scared = true;
                        character = hit.transform;
                        character.gameObject.GetComponent<AudioSource>().Play();
                        gameManager.instance.CompleteGame();
                    }
                }
            }
        }
        else {
            character.Translate(-speed * Time.deltaTime, 0, 0, Space.Self);
        }
	}
}

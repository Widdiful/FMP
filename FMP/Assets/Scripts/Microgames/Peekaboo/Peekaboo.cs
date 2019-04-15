using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Peekaboo : MonoBehaviour {

    public Animator charAnim, leftHand, rightHand;
    AudioSource audioSource;
    int stage = 0;

    private void Start() {
        audioSource = GetComponent<AudioSource>();
    }

    void Update () {
		if (stage == 0 && !ProximitySensor.instance.nearby) {
            stage = 1;
            charAnim.SetInteger("Stage", stage);
        }

        else if (stage == 1 && ProximitySensor.instance.nearby) {
            stage = 2;
            leftHand.SetBool("Covering", true);
            rightHand.SetBool("Covering", true);
            charAnim.SetInteger("Stage", stage);
        }

        else if (stage == 2 && !ProximitySensor.instance.nearby) {
            stage = 3;
            leftHand.SetBool("Covering", false);
            rightHand.SetBool("Covering", false);
            charAnim.SetInteger("Stage", stage);
            audioSource.Play();
            gameManager.instance.CompleteGame();
        }
	}
}

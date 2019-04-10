using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProximityWave : MonoBehaviour {

    public float closeRotation;
    public float farRotation;
    public float lerpSpeed;
    public int wavesDone;
    public int wavesRequired;
    public Animator anim;
    public AudioSource audioSource;

    private bool complete;
    private bool passedCentre;
    private bool isLeft;
    private enum Stages { MovingLeft, MovingRight };
    private Stages stage = Stages.MovingLeft;

    private void Update() {
        if (ProximitySensor.instance.nearby) {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, 0), lerpSpeed);
            if (passedCentre == false) {
                wavesDone++;
                if (wavesDone >= wavesRequired && !complete) {
                    if (anim) {
                        anim.SetBool("Play", true);
                    }
                    if (audioSource)
                        audioSource.Play();
                    complete = true;
                    gameManager.instance.CompleteGame();
                }
            }
            passedCentre = true;
        }
        else {
            if (stage == Stages.MovingLeft && passedCentre) {
                stage = Stages.MovingRight;
                isLeft = true;
                passedCentre = false;
            }
            if (stage == Stages.MovingRight && passedCentre) {
                stage = Stages.MovingLeft;
                isLeft = false;
                passedCentre = false;
            }
        }

        if (!passedCentre) {
            if (isLeft) {
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, closeRotation), lerpSpeed);
            }
            else {
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, farRotation), lerpSpeed);
            }
        }

    }

}

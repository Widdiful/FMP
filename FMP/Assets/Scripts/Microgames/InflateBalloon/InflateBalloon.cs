using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InflateBalloon : MonoBehaviour {

    public float lerpSpeed;
    public float sizeIncrease;
    public int numberOfPumps;
    Vector3 nextScale = Vector3.one;
    float nextAngle;
    Vector3 nextGoal;
    int pumps;
    bool pumping;
    bool completed;

    private void Update() {
        if (!pumping && ProximitySensor.instance.nearby && !completed) {
            pumping = true;
            pumps++;

            if (pumps >= numberOfPumps) {
                completed = true;
                gameManager.instance.CompleteGame();
            }

            else {
                nextScale = transform.localScale * sizeIncrease;
                nextGoal = transform.localScale + ((nextScale - transform.localScale) / 2);
                nextAngle += (-90 / (numberOfPumps - 1));
            }
        }
        else if (!ProximitySensor.instance.nearby && transform.localScale.x >= nextGoal.x) {
            pumping = false;
        }

        transform.localScale = Vector3.Lerp(transform.localScale, nextScale, lerpSpeed * Time.deltaTime);
        Quaternion angle = Quaternion.Euler(nextAngle, 0, 0);
        transform.rotation = Quaternion.Lerp(transform.rotation, angle, lerpSpeed * Time.deltaTime);
    }
}

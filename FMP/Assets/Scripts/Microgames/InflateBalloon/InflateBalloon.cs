using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InflateBalloon : MonoBehaviour {

    public float lerpSpeed;
    public float maxSize;
    bool pumping;

    private void Update() {
        if (!pumping && ProximitySensor.instance.nearby) {
            pumping = true;
            transform.localScale = Vector3.Lerp(transform.localScale, transform.localScale * maxSize, lerpSpeed);
        }
        else if (!ProximitySensor.instance.nearby) {
            pumping = false;
        }
    }
}

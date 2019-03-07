using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TanningController : MonoBehaviour {

    public float tanPercentage;
    public float increasePerSecond;
    public float decreasePerSecond;
    public Gradient skinTone;

    public Slider slider;
    public SpriteRenderer character;
    public Timer timer;

	void Update () {
        if (!ProximitySensor.instance.nearby)
            tanPercentage += increasePerSecond * Time.deltaTime;
        else
            tanPercentage -= decreasePerSecond * Time.deltaTime;

        tanPercentage = Mathf.Clamp01(tanPercentage);

        if (tanPercentage >= 0.4f && tanPercentage <= 0.6f)
            timer.winOnTimeOver = true;
        else
            timer.winOnTimeOver = false;
    }

    private void FixedUpdate() {
        slider.value = tanPercentage;
        character.color = skinTone.Evaluate(tanPercentage);
    }
}

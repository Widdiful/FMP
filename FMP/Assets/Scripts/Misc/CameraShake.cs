using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour {

    public float shakeAmount;
    public Transform target;
    private Vector3 basePosition;

    private void Start() {
        basePosition = transform.localPosition;
    }

    void Update () {
        if (target)
            basePosition = new Vector3(target.position.x, target.position.y, transform.position.z);
        Shake();
	}

    private void Shake() {
        float x = Random.Range(-1f, 1f) * shakeAmount;
        float y = Random.Range(-1f, 1f) * shakeAmount;

        transform.localPosition = new Vector3(basePosition.x + x, basePosition.y + y, transform.position.z);
    }
}

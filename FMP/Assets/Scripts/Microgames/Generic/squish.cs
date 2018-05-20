using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class squish : MonoBehaviour {

    private float squishX;
    private float squishY;
    public bool doSquish = true;
    private Vector3 baseScale;
    public int flip = 1;
    public bool pulse;
    public float pulseTime;
    public Vector2 pulseSize;
    private float pulseCD;
    private float speed = 0.2f;

    void Start() {
        baseScale = transform.localScale;
        //pulseCD = pulseTime;
    }

    void Update() {
        if (doSquish) {
            transform.localScale = Vector3.Lerp(transform.localScale,
                new Vector3((transform.localScale.z + squishX) * flip,
                transform.localScale.z + squishY,
                transform.localScale.z), speed);
            squishX = Mathf.Lerp(squishX, 0, speed);
            squishY = Mathf.Lerp(squishY, 0, speed);
            if (Mathf.Abs(((transform.localScale.z + squishX) * flip) - transform.localScale.x) < 0.00000001f &&
                Mathf.Abs((transform.localScale.z + squishY) - transform.localScale.y) < 0.00000001f) {
                transform.localScale = baseScale;
                doSquish = false;
            }
        }
        else transform.localScale = baseScale;
        if (pulse) {
            pulseCD -= Time.deltaTime;
            if (pulseCD <= 0) {
                pulseCD = pulseTime;
                Squish(pulseSize);
            }
        }
    }

    public void Squish(Vector2 scale, float _speed = 0.2f) {
        doSquish = true;
        squishX = scale.x;
        squishY = scale.y;
        speed = _speed;
    }
}

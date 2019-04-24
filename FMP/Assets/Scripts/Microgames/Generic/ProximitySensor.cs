using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProximitySensor : MonoBehaviour {

    public static ProximitySensor instance;

    public bool nearby;

    void Awake() {
        if (instance == null) instance = this;
        else if (instance != this) Destroy(gameObject);
    }

    void Start() {
        PAProximity.onProximityChange += SetScaleByProximity;

        SetScaleByProximity(PAProximity.proximity);
    }

    void OnDestroy() {
        PAProximity.onProximityChange -= SetScaleByProximity;
    }

    void SetScaleByProximity(PAProximity.Proximity arg) {
        if (Time.timeScale > 0) {
            if (arg == PAProximity.Proximity.NEAR) nearby = true;
            else nearby = false;
        }
    }
}

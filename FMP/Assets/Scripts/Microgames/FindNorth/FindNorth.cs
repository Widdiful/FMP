using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindNorth : MonoBehaviour {

    private void Start() {
        //Input.compass.enabled = true;
        //Input.location.Start();
    }

    void Update () {
        transform.rotation = Quaternion.Euler(0, 0, -Input.compass.magneticHeading);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour {

    public float seconds;
    private float timer;

	void Start () {
        timer = seconds;
	}
	
	void Update () {
        timer -= Time.deltaTime;
	}
}

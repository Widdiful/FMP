using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkingMouth : MonoBehaviour {

    public float speed;
    float timer;

	void Update () {
        timer += Time.deltaTime * speed;
        float yScale = Mathf.Sin(timer);
        transform.localScale = new Vector3(transform.localScale.x, yScale, transform.localScale.z);
	}
}

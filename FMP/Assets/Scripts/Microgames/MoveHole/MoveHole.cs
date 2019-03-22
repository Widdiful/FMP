using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveHole : MonoBehaviour {

    public float speed;
    public Vector2 minPos;
    public Vector2 maxPos;

	void Update () {
        if (Input.touchCount > 0) {
            Vector3 direction = Input.touches[0].deltaPosition;
            Vector3 newDirection = new Vector3(direction.x, -direction.y, 0);
            transform.Translate(newDirection * speed * Time.deltaTime);
        }

        transform.position = new Vector3(
            Mathf.Clamp(transform.position.x, minPos.x, maxPos.x),
            transform.position.y,
            Mathf.Clamp(transform.position.z, minPos.y, maxPos.y));
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawPathFollower : MonoBehaviour {

    public Drawing drawing;
    public float moveSpeed;

    Vector3 targetPoint;
    int targetIndex = 0;
    float targetDistance = 0.1f;
    SpriteRenderer sprite;

    private void Start() {
        sprite = GetComponent<SpriteRenderer>();
        sprite.color = Color.HSVToRGB(Random.Range(0.0f, 1.0f), 1, 1);
    }

    void Update () {
		if (drawing && drawing.finished) {
            float distance = Vector3.Distance(transform.position, targetPoint);
            if (targetIndex == 0 || distance <= targetDistance) {
                if (drawing.points.Count > targetIndex) {
                    targetPoint = drawing.points[targetIndex];
                }
                if (targetIndex == drawing.points.Count - 1) {
                    targetPoint = transform.position + new Vector3(0, 50, 0);
                    return;
                }
                targetIndex++;
            }

            transform.position = Vector3.MoveTowards(transform.position, targetPoint, moveSpeed * Time.deltaTime);
            //transform.up = targetPoint - transform.position;

            if ((targetPoint - transform.position).x < 0)
                sprite.flipX = true;
            else
                sprite.flipX = false;

            targetIndex %= (drawing.points.Count);
        }
	}

    private void OnCollisionStay2D(Collision2D collision) {
        if (drawing.points.Count > targetIndex)
            targetPoint = drawing.points[targetIndex];
        targetIndex++;
    }
}

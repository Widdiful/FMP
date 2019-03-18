using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HonkHorn : MonoBehaviour {

    float baseY;
    float distance;
    Vector3 pos1, pos2;

    private void Start() {
        baseY = transform.localScale.y;
    }

    private void Update() {
        if (Input.touchCount > 1) {
            pos1 = Input.GetTouch(0).position;
            pos2 = Input.GetTouch(1).position;
            pos1.z = 10;
            pos2.z = 10;
            pos1 = Camera.main.ScreenToWorldPoint(pos1);
            pos2 = Camera.main.ScreenToWorldPoint(pos2);

            var worldToLocalMatrix = Matrix4x4.TRS(transform.position, transform.rotation, Vector3.one).inverse;
            pos1 = worldToLocalMatrix.MultiplyPoint3x4(pos1);
            pos2 = worldToLocalMatrix.MultiplyPoint3x4(pos2);

            distance = Mathf.Abs(pos1.y - pos2.y + 0.5f);

            if (distance < baseY) {
                Vector3 localScale = transform.localScale;
                localScale.y = distance;
                transform.localScale = localScale;
            }
        }
    }
}

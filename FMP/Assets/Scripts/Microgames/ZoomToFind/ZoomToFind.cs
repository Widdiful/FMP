using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomToFind : MonoBehaviour {

    public float zoomSpeed, moveSpeed;
    public float minSize, maxSize, detectionSize;
    public Transform sphere;
    public Transform axisX;
    public Transform axisY;
    public Transform axisZ;

    public bool rotateSphere;

    Camera camera;
    float oldAngle;
    bool complete;

    private void Start() {
        camera = GetComponent<Camera>();
    }

    private void Update() {
        if (Input.touchCount > 0 && !complete) {
            Vector2 deltaMove = Input.touches[0].deltaPosition;

            if (Input.touchCount >= 2) {
                Touch touchOne = Input.touches[0];
                Touch touchTwo = Input.touches[1];

                Vector2 posDifference;

                if (touchTwo.phase == TouchPhase.Began) {
                    posDifference = touchOne.position - touchTwo.position;
                    oldAngle = Mathf.Atan2(posDifference.y, posDifference.x) * Mathf.Rad2Deg;
                }

                deltaMove = (touchOne.deltaPosition + touchTwo.deltaPosition) / 2;

                Vector2 prevPosOne = touchOne.position - touchOne.deltaPosition;
                Vector2 prevPosTwo = touchTwo.position - touchTwo.deltaPosition;

                float prevMagnitude = (prevPosOne - prevPosTwo).magnitude;
                float magnitude = (touchOne.position - touchTwo.position).magnitude;

                float difference = prevMagnitude - magnitude;

                camera.fieldOfView += difference * zoomSpeed * Time.deltaTime;
                camera.fieldOfView = Mathf.Clamp(camera.fieldOfView, minSize, maxSize);

                posDifference = touchOne.position - touchTwo.position;
                float angle = Mathf.Atan2(posDifference.y, posDifference.x) * Mathf.Rad2Deg;
                float deltaAngle = Mathf.DeltaAngle(angle, oldAngle);
                //transform.Rotate(0, 0, deltaAngle);
                //axisZ.Rotate(0, 0, deltaAngle);
                oldAngle = angle;

                //sphere.Rotate(0, 0, deltaAngle);
            }

            if (rotateSphere) {
                axisX.Rotate(deltaMove.y * moveSpeed * Time.deltaTime, 0, 0);
                axisY.Rotate(0, -deltaMove.x * moveSpeed * Time.deltaTime, 0);
            }
            else {
                transform.Translate(-deltaMove * moveSpeed * camera.fieldOfView);
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision) {
        if (camera.fieldOfView <= detectionSize && !complete && collision.transform.position.z <= 0) {
            complete = true;
            gameManager.instance.CompleteGame();
        }
    }
}

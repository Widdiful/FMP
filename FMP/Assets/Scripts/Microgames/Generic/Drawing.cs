using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drawing : MonoBehaviour {

    public float stepSize;
    public List<Vector3> points;
    public bool finished;
    public bool allowMultiple;

    Vector3 lastPoint;
    LineRenderer line;

    private void Start() {
        line = GetComponent<LineRenderer>();
    }

    private void Update() {
        if (finished && !allowMultiple) {
            return;
        }

        if (Input.touchCount > 0) {
            if (Input.GetTouch(0).phase == TouchPhase.Began) {
                points.Clear();
                lastPoint = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
                lastPoint.z = transform.position.z;
                points.Add(lastPoint);
                finished = false;
            }
            else if (Input.GetTouch(0).phase == TouchPhase.Moved) {
                Vector3 newPoint = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
                newPoint.z = transform.position.z;
                if (Vector3.Distance(lastPoint, newPoint) >= stepSize) {
                    points.Add(newPoint);
                    lastPoint = newPoint;
                }
            }
            else if (Input.GetTouch(0).phase == TouchPhase.Ended) {
                if (points.Count > 1)
                    finished = true;
            }
        }

        line.positionCount = points.Count;
        line.SetPositions(points.ToArray());
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drawing : MonoBehaviour {

    public float stepSize;
    public List<Vector3> points;
    public bool finished;
    public bool allowMultiple;
    public bool multipleLines;

    List<Vector2> v2points = new List<Vector2>();
    Vector3 lastPoint;
    LineRenderer line;
    EdgeCollider2D edgeCollider;

    private void Start() {
        line = GetComponent<LineRenderer>();
        edgeCollider = GetComponent<EdgeCollider2D>();
        points.Clear();
        v2points.Clear();
        finished = false;
    }

    private void Update() {
        if (finished && !allowMultiple) {
            return;
        }

        if (Input.touchCount > 0) {
            if (Input.GetTouch(0).phase == TouchPhase.Began) {
                points.Clear();
                v2points.Clear();
                lastPoint = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
                lastPoint.z = transform.position.z;
                points.Add(lastPoint);
                v2points.Add(lastPoint);
                finished = false;
            }
            else if (Input.GetTouch(0).phase == TouchPhase.Moved) {
                Vector3 newPoint = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
                newPoint.z = transform.position.z;
                if (Vector3.Distance(lastPoint, newPoint) >= stepSize) {
                    points.Add(newPoint);
                    v2points.Add(newPoint);
                    lastPoint = newPoint;
                }
            }
            else if (Input.GetTouch(0).phase == TouchPhase.Ended) {
                if (points.Count > 1) {
                    finished = true;
                    if (multipleLines) {
                        Instantiate(gameObject, transform.parent);
                        multipleLines = false;
                    }
                }
            }
        }

        line.positionCount = points.Count;
        line.SetPositions(points.ToArray());

        if (edgeCollider) {
            edgeCollider.points = v2points.ToArray();
        }
    }
}

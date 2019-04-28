using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapInfo
{
    public int id;
    public float startTime;
    public float endTime;
    public Vector2 startPos;
    public Vector2 currentPos;
    public Vector2 endPos;

    public float tapDuration;
    public Vector2 tapMovement;

    // Set start information for tap
    public void setStart(float time, Vector2 pos) {
        startTime = time;
        startPos = pos;
        endTime = 0;
        endPos = Vector2.zero;
        tapDuration = 0;
        tapMovement = Vector2.zero;

        //Debug.Log("Start Time " + startTime + "\nStart Pos" + startPos);
    }

    // Set end information for tap
    public void setEnd(float time, Vector2 pos) {
        endTime = time;
        endPos = pos;
        tapDuration = endTime - startTime;
        tapMovement = endPos - startPos;

        //Debug.Log("End Time " + endTime + "\nEnd Pos" + endPos);
        //Debug.Log("Duration " + tapDuration + "\nMovement" + tapMovement);
    }
}

public class MobileInput : MonoBehaviour {

    public enum TapTypes { Press, Hold, Drag, ScreenTap };
    public TapTypes TapType;

    public bool Use3D;

    private List<TapInfo> taps = new List<TapInfo>();

    void Start() {
        for (int i = 0; i < 10; i++) {
            taps.Add(new TapInfo());
        }
    }

    void Update() {
        int i = 0;
        while (i < Input.touchCount) {
            if (Input.GetTouch(i).phase == TouchPhase.Began) {
                InvokeFunction("OnScreenTap", gameObject);
                taps[i].setStart(Time.time, Input.GetTouch(i).position);

                if (!Use3D) {
                    RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.GetTouch(i).position), Vector2.zero);

                    if (hit) {
                        InvokeFunction("OnTapStart", hit.collider.gameObject);
                    }
                }

                else {
                    RaycastHit hit;
                    if (Physics.Raycast(Camera.main.ScreenToWorldPoint(Input.GetTouch(i).position), Camera.main.transform.forward, out hit)) {
                        InvokeFunction("OnTapStart", hit.collider.gameObject);
                    }
                }
            }

            else if (Input.GetTouch(i).phase == TouchPhase.Ended){
                taps[i].setEnd(Time.time, Input.GetTouch(i).position);

                if (!Use3D) {
                    RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.GetTouch(i).position), Vector2.zero);

                    if (hit.collider != null) {
                        InvokeFunction("OnTapEnd", hit.collider.gameObject);
                    }
                }

                else {
                    RaycastHit hit;
                    if (Physics.Raycast(Camera.main.ScreenToWorldPoint(Input.GetTouch(i).position), Camera.main.transform.forward, out hit)) {
                        InvokeFunction("OnTapEnd", hit.collider.gameObject);
                    }
                }
            }

            else if (Input.GetTouch(i).phase == TouchPhase.Moved) {
                if (!Use3D) {
                    RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.GetTouch(i).position), Vector2.zero);

                    if (hit) {
                        InvokeFunction("OnTapRub", hit.collider.gameObject);
                    }
                }

                else {
                    RaycastHit hit;
                    if (Physics.Raycast(Camera.main.ScreenToWorldPoint(Input.GetTouch(i).position), Camera.main.transform.forward, out hit)) {
                        InvokeFunction("OnTapRub", hit.collider.gameObject);
                    }
                }
            }

            taps[i].currentPos = Input.GetTouch(i).position;

            ++i;
        }
    }

    // Run functions on game object
    void InvokeFunction(string function, GameObject obj) {
        foreach (MonoBehaviour component in obj.GetComponents<MonoBehaviour>()) {
            if (component.GetType().GetMethod(function) != null) {
                component.Invoke(function, 0f);
            }
        }
    }
}

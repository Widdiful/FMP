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

    public void setStart(float time, Vector2 pos) {
        startTime = time;
        startPos = pos;
        endTime = 0;
        endPos = Vector2.zero;
        tapDuration = 0;
        tapMovement = Vector2.zero;

        //Debug.Log("Start Time " + startTime + "\nStart Pos" + startPos);
    }

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

    public enum TapTypes { Press, Hold, Drag };
    public TapTypes TapType;

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
                taps[i].setStart(Time.time, Input.GetTouch(i).position);

                RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.GetTouch(i).position), Vector2.zero);
                Debug.DrawRay(Camera.main.ScreenToWorldPoint(Input.GetTouch(i).position), Vector2.zero, Color.yellow, 100);

                if (hit) {
                    InvokeFunction("OnTapStart", hit.collider.gameObject);
                    print("hit!");
                }
                else print("nope");
            }

            else if (Input.GetTouch(i).phase == TouchPhase.Ended){
                taps[i].setEnd(Time.time, Input.GetTouch(i).position);

                RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.GetTouch(i).position), Vector2.down, 0.01f);

                if (hit.collider != null) {
                    InvokeFunction("OnTapEnd", hit.collider.gameObject);
                }
            }

            else if (Input.GetTouch(i).phase == TouchPhase.Moved) {

            }

            taps[i].currentPos = Input.GetTouch(i).position;

            ++i;
        }
    }

    void InvokeFunction(string function, GameObject obj) {
        foreach (MonoBehaviour component in obj.GetComponents<MonoBehaviour>()) {
            if (component.GetType().GetMethod(function) != null) {
                component.Invoke(function, 0f);
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobileInput : MonoBehaviour {

    public enum InputTypes { Tap, Motion, Mic, Proximity };
    public enum TapTypes { Press, Hold, Drag };

    public InputTypes InputType;
    public TapTypes TapType;

	void Start () {
		
	}
	
	void Update () {
        if (InputType == InputTypes.Tap) {
            int i = 0;
            while (i < Input.touchCount) {

                if (TapType == TapTypes.Press) {
                    if (Input.GetTouch(i).phase == TouchPhase.Began) {
                        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.GetTouch(i).position), -Vector2.up);

                        if (hit.collider != null) {
                            foreach (MonoBehaviour component in GetComponents<MonoBehaviour>()) {
                                if (component.GetType().GetMethod("OnTap") != null) {
                                    component.Invoke("OnTap", 0f);
                                }
                            }
                        }
                    }
                }

                ++i;
            }
        }
    }
}

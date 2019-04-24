using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutRope : MonoBehaviour {

    bool hasCut;

	void Update () {
		if (!hasCut && Time.timeScale > 0) {
            if (Input.touchCount > 0) {
                if (Input.GetTouch(0).phase == TouchPhase.Moved) {
                    RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position), Vector2.zero);

                    if (hit) {
                        RopePart rope = hit.transform.GetComponent<RopePart>();
                        if (rope && !rope.start && !rope.GetComponent<HingeJoint2D>().connectedBody.GetComponent<RopePart>().start) {
                            rope.Cut();
                            hasCut = true;
                        }
                    }
                }
            }
        }
	}
}

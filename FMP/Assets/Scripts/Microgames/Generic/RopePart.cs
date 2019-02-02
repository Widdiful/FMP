using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopePart : MonoBehaviour {

    public bool start;
    public int numberOfParts;
    public Transform nextPart;
    public bool attachEnd;
    public Vector3 endPoint;

    LineRenderer line;
    RopePart lastPart;
    List<Transform> parts = new List<Transform>();

	void Start () {
        line = GetComponent<LineRenderer>();
        lastPart = this;
        GameObject baseObject = gameObject;

        if (start) {
            for(int i = 0; i < numberOfParts; i++) {
                GameObject newPart = Instantiate(baseObject, transform.position, transform.rotation);
                newPart.GetComponent<RopePart>().start = false;
                if (lastPart) {
                    newPart.GetComponent<HingeJoint2D>().connectedBody = lastPart.GetComponent<Rigidbody2D>();
                }
                lastPart = newPart.GetComponent<RopePart>();
                parts.Add(newPart.transform);

                if (attachEnd && i == numberOfParts - 1) {
                    newPart.transform.position = endPoint;
                }
                else {
                    newPart.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
                }
            }
        }

        line.positionCount = parts.Count + 1;

        GetComponent<HingeJoint2D>().autoConfigureConnectedAnchor = false;
	}
	
	void Update () {
		if (start) {
            line.SetPosition(0, transform.position);
            for(int i = 0; i < parts.Count; i++) {
                line.SetPosition(i + 1, parts[i].position);
            }

            if (attachEnd)
                lastPart.transform.position = endPoint;
        }
	}
}

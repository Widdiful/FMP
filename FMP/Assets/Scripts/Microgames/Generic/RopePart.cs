using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopePart : MonoBehaviour {

    public bool start;
    public int numberOfParts;
    public bool attachEnd;
    public bool attachEndTransform;
    public Vector3 endPoint;
    public Transform endTransform;
    bool endTransformBody;

    LineRenderer line;
    RopePart firstPart;
    RopePart lastPart;
    RopePart nextPart;
    List<RopePart> parts = new List<RopePart>();

	void Start () {

        line = GetComponent<LineRenderer>();
        lastPart = this;
        GameObject baseObject = gameObject;

        if (start) {
            firstPart = this;
            for(int i = 0; i < numberOfParts; i++) {
                GameObject newPart = Instantiate(baseObject, transform.position, transform.rotation);
                newPart.GetComponent<RopePart>().start = false;
                if (lastPart) {
                    newPart.GetComponent<HingeJoint2D>().connectedBody = lastPart.GetComponent<Rigidbody2D>();
                }
                lastPart.GetComponent<RopePart>().nextPart = newPart.GetComponent<RopePart>();
                lastPart.GetComponent<RopePart>().firstPart = this;
                lastPart = newPart.GetComponent<RopePart>();
                parts.Add(newPart.GetComponent<RopePart>());

                if ((attachEnd || attachEndTransform) && i == numberOfParts - 1) {
                    newPart.transform.position = endPoint;

                    HingeJoint2D endHinge = endTransform.GetComponent<HingeJoint2D>();
                    if (attachEndTransform && endTransform && endHinge) {
                        endHinge.connectedBody = lastPart.GetComponent<Rigidbody2D>();
                        JointAngleLimits2D limits = new JointAngleLimits2D();
                        limits.max = 0;
                        limits.min = 0;
                        lastPart.GetComponent<HingeJoint2D>().limits = limits;
                        endTransformBody = true;
                    }
                }
                else {
                    newPart.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
                }
            }

            GetComponent<HingeJoint2D>().connectedAnchor = transform.position;
        }

        line.positionCount = parts.Count + 1;

        GetComponent<HingeJoint2D>().autoConfigureConnectedAnchor = false;
	}

    public void Cut() {
        GetComponent<HingeJoint2D>().enabled = false;
        parts = GetChildParts();
        line.positionCount = parts.Count + 1;
        start = true;
        for(int i = 0; i < firstPart.parts.Count; i++) {
            RopePart part = firstPart.parts[i];
            if (part == this || parts.Contains(part)) {
                firstPart.parts.Remove(part);
                i--;
            }
        }
        firstPart.line.positionCount = firstPart.parts.Count + 1;
    }
	
	void Update () {
		if (start) {
            line.SetPosition(0, transform.position);
            for(int i = 0; i < parts.Count; i++) {
                line.SetPosition(i + 1, parts[i].transform.position);
            }

            if (attachEnd)
                lastPart.transform.position = endPoint;

            else if (attachEndTransform && !endTransformBody)
                lastPart.transform.position = endTransform.position;
        }
	}

    public List<RopePart> GetChildParts() {
        List<RopePart> list = new List<RopePart>();
        if (nextPart) {
            list.AddRange(nextPart.GetChildParts());
        }
        list.Add(this);

        return list;
    }
}

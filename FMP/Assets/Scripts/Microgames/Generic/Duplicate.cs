using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Duplicate : MonoBehaviour {

    public int copies;

	void Start () {
		for (int i = 0; i < copies; i++) {
            Duplicate copy = Instantiate(gameObject, transform.parent).GetComponent<Duplicate>();
            Destroy(copy);
        }
	}
}

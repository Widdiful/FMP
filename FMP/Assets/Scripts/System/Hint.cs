using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hint : MonoBehaviour {

	void Start () {
        if (!gameManager.instance.enableHints)
            gameObject.SetActive(false);
	}
}

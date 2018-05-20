using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformerEnemy : MonoBehaviour {

	void Update () {
		
	}

    public void Kill() {
        Destroy(gameObject);
    }
}

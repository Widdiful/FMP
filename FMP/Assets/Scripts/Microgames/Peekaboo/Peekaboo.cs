using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Peekaboo : MonoBehaviour {

    int stage = 0;
	
	void Update () {
		if (stage == 0 && !ProximitySensor.instance.nearby) {
            stage = 1;
        }

        else if (stage == 1 && ProximitySensor.instance.nearby) {
            stage = 2;
        }

        else if (stage == 2 && !ProximitySensor.instance.nearby) {
            stage = 3;
            gameManager.instance.CompleteGame();
        }
	}
}

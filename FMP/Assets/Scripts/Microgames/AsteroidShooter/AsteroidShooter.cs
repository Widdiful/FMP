using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidShooter : MonoBehaviour {

    public LineRenderer leftGun, rightGun;
    [Range(0, 1)]
    public float ammo;
    public float shotCost;
    public float regen;
    public float moveSpeed;

	void Update () {
        ammo = Mathf.Clamp01(ammo + (regen * Time.deltaTime));
        if (Input.touchCount > 0) {
            if (Input.touches[0].phase == TouchPhase.Began) {
                if (ammo >= shotCost) {
                    ammo -= shotCost;
                    leftGun.enabled = true;
                    rightGun.enabled = true;
                    RaycastHit hit;
                    if (Physics.Raycast(leftGun.transform.position, transform.forward, out hit)) {
                        if (hit.collider.CompareTag("Enemy")) {
                            hit.collider.gameObject.SetActive(false);
                        }
                    }
                    if (Physics.Raycast(rightGun.transform.position, transform.forward, out hit)) {
                        if (hit.collider.CompareTag("Enemy")) {
                            hit.collider.gameObject.SetActive(false);
                        }
                    }
                }
            }
            else {
                leftGun.enabled = false;
                rightGun.enabled = false;
            }
        }

        transform.Translate(transform.forward * moveSpeed * Time.deltaTime);
	}
}

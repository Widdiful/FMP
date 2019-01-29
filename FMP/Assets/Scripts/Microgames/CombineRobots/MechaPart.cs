using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechaPart : MonoBehaviour {

    public float spawnRange;
    public bool moving;
    public float moveSpeed;

    bool movingRight;
	
	void Start () {
        transform.position = new Vector3(Random.Range(-spawnRange, spawnRange), transform.position.y, transform.position.z);
	}

    private void Update() {
        if (moving) {
            if (!movingRight) {
                transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
                if (transform.position.x < -spawnRange) {
                    movingRight = true;
                }
            }
            else {
                transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
                if (transform.position.x > spawnRange) {
                    movingRight = false;
                }
            }
        }
    }
}

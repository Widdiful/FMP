using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteerCar : MonoBehaviour {

    public float turnSpeed;
    public Transform cameraTransform;
    public Timer timer;
    bool lost;

	void Update () {
        if (!lost)
            transform.Translate(Input.acceleration.x * turnSpeed * Time.deltaTime, 0, 0);
	}

    private void FixedUpdate() {
        cameraTransform.localPosition = new Vector3(cameraTransform.position.x, 2, cameraTransform.position.z);
        cameraTransform.position = new Vector3(0, cameraTransform.position.y, -10);
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        GetComponent<MoveInDirection>().enabled = false;
        collision.transform.Find("Wheels").gameObject.SetActive(true);
        if (!lost) {
            timer.winOnTimeOver = false;
            transform.Find("Wheels").gameObject.SetActive(true);
            StartCoroutine(EndGame());
        }
        lost = true;
    }

    IEnumerator EndGame() {
        yield return new WaitForSeconds(1.0f);
        gameManager.instance.FailGame();
    }
}

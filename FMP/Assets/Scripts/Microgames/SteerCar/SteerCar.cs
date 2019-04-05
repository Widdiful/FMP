using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteerCar : MonoBehaviour {

    public float turnSpeed;
    public Transform cameraTransform;
    public Timer timer;
    public float rotateAmount;
    bool lost;
    Rigidbody2D rb;

    private void Start() {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update () {
        if (!lost) {
            transform.Translate(Input.acceleration.x * turnSpeed * Time.deltaTime, 0, 0);
            transform.rotation = Quaternion.Euler(0, 0, Input.acceleration.x * rotateAmount);
        }
	}

    private void FixedUpdate() {
        //cameraTransform.localPosition = new Vector3(cameraTransform.position.x, 2, cameraTransform.position.z);
        //cameraTransform.position = new Vector3(0, cameraTransform.position.y, -10);
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        GetComponent<MoveInDirection>().enabled = false;

        MoveInDirection otherMove = collision.gameObject.GetComponent<MoveInDirection>();
        if (otherMove) otherMove.enabled = false;

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

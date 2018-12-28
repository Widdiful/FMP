using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Money : MonoBehaviour {

    public float rotateSpeed;
    public float moveSpeed;
    public float maxY;
    public Transform moneyPile;

    private float timer = 0;

    private void OnEnable() {
        transform.rotation = Quaternion.identity;
        transform.position = new Vector3(moneyPile.position.x, moneyPile.position.y + moneyPile.localScale.y + 1.0f, moneyPile.position.z);
        maxY = moneyPile.position.y + moneyPile.localScale.y + 2.0f;
    }

    private void Update() {
        transform.Rotate(-rotateSpeed, 0, 0);
        transform.Translate(0, moveSpeed, 0, Space.World);
        if (transform.position.y >= maxY) gameObject.SetActive(false);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DistanceToTarget : MonoBehaviour {

    public Transform target;
    public Text distanceText;
    Rigidbody2D rb;
    bool complete;

    private void Start() {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update () {
        float distance = Vector3.Distance(transform.position, target.position);
        distanceText.text = distance.ToString("F2");
        if (complete)
            transform.position = Vector3.MoveTowards(transform.position, target.position, 1 * Time.deltaTime);
    }

    private void OnTriggerStay2D(Collider2D collision) {
        transform.localScale *= 0.95f;
        if (rb.gravityScale != 0) {
            rb.gravityScale = 0;
            rb.velocity = Vector2.zero;
            complete = true;
        }
        
    }
}

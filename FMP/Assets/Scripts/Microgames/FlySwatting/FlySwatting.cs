using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlySwatting : MonoBehaviour {

    public float speed;
    public float speedAdjust;
    public float wanderSize;
    public float maxSpeed;
    public GameObject splat;
    Vector2 direction;

    private void Start() {
        direction += new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
        direction = direction.normalized;
    }

    void Update () {
        transform.Translate(direction * speed * Time.deltaTime);
        transform.right = direction;

        direction += new Vector2(Random.Range(-wanderSize, wanderSize), Random.Range(-wanderSize, wanderSize));
        direction = direction.normalized;

        speed += Random.Range(-speedAdjust, speedAdjust);
        speed = Mathf.Clamp(speed, 0, maxSpeed);

        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.y * 0.01f);

        if (Input.touchCount > 0) {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.touches[0].position), Vector2.zero);

            if (hit && hit.transform == transform) {
                if (FindObjectsOfType<FlySwatting>().Length <= 1) {
                    gameManager.instance.CompleteGame();
                }
                GameObject newSplat = Instantiate(splat, transform.position, Quaternion.identity);
                newSplat.GetComponent<AudioSource>().Play();
                Destroy(gameObject);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        direction = Vector2.Reflect(direction, collision.contacts[0].normal);
    }
}

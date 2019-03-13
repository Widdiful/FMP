using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SneakAndStealPlayer : MonoBehaviour {

    public SneakAndStealGuard guard;
    public float speed;
    public float targetX;
    public Transform treasure;
    bool runAway;
    SpriteRenderer sprite;

    private void Start() {
        sprite = GetComponent<SpriteRenderer>();
    }

    private void Update() {
        if (Input.touchCount > 0) {
            Run();
        }

        if (runAway) {
            transform.Translate(-speed * 3 * Time.deltaTime, 0, 0);
        }
    }

    void RunAway() {
        runAway = true;
        guard.gameEnd = true;
        sprite.flipX = !sprite.flipX;
    }

    public void Run() {
        if (!runAway) {
            transform.Translate(speed * Time.deltaTime, 0, 0);
            if (guard.looking) {
                RunAway();
                print("lost");
                StartCoroutine(FailGame());
            }
            else if (transform.position.x >= targetX) {
                treasure.SetParent(transform);
                treasure.localPosition = new Vector3(0, 1, 0);
                RunAway();
                print("won");
                gameManager.instance.CompleteGame();
            }
        }
    }

    IEnumerator FailGame() {
        FindObjectOfType<Timer>().active = false;
        yield return new WaitForSeconds(1);
        gameManager.instance.FailGame();
    }
}

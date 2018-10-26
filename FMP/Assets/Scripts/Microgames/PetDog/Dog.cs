using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dog : MonoBehaviour {

    public List<Color> colours = new List<Color>();
    private float rubTime;
    private float jumpSpeed = 2;

    void Start() {
        GetComponentInChildren<Renderer>().material.color = colours[Random.Range(0, colours.Count)];
        if (Random.Range(0, 2) == 0) {
            GetComponentInChildren<Renderer>().materials[1].color = colours[Random.Range(0, colours.Count)];
        }
        else {
            GetComponentInChildren<Renderer>().materials[1].color = GetComponentInChildren<Renderer>().material.color;
        }
    }

    public void Update() {
        rubTime += Time.deltaTime * jumpSpeed;
        transform.position = new Vector3(transform.position.x, Mathf.Abs(Mathf.Sin(rubTime) * 0.2f), transform.position.z);
    }

    public void OnTapRub() {
        jumpSpeed += Time.deltaTime * 5;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableColliderOnYPos : MonoBehaviour {

    public float killY;
    Collider[] colliders;
    public bool disabled;
    public bool randomYRotation;
    public MeshRenderer randomColourMesh;
    AudioSource audioSource;

    private void Start() {
        colliders = GetComponents<Collider>();
        audioSource = GetComponent<AudioSource>();
        audioSource.pitch = Random.Range(0.5f, 0.75f);
        if (randomYRotation) {
            transform.rotation = Quaternion.Euler(0, Random.Range(0f, 360f), 0);
        }
        if (randomColourMesh) {
            randomColourMesh.materials[0].color = Color.HSVToRGB(Random.Range(0f, 1f), 0.9f, 0.9f);
        }
    }

    void Update () {
		if (!disabled && transform.position.y <= killY) {
            disabled = true;
            foreach(Collider collider in colliders) {
                collider.enabled = false;
            }
            if (audioSource)
                audioSource.Play();

            bool complete = true;
            foreach(DisableColliderOnYPos i in FindObjectsOfType<DisableColliderOnYPos>()) {
                if (!i.disabled) {
                    complete = false;
                }
            }

            if (complete) {
                gameManager.instance.CompleteGame();
            }
        }
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetLandTexture : MonoBehaviour {

    public float minCutoff, maxCutoff;
    Renderer renderer;

    void Start() {
        renderer = GetComponent<Renderer>();
        renderer.materials[0].SetFloat("_Cutoff", Random.Range(minCutoff, maxCutoff));
        renderer.materials[0].SetTextureOffset("_MainTex", new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)));
        
    }
}

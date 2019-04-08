using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ZoomAndEnhance : MonoBehaviour {

    public RectTransform zoomBar;
    public Canvas enhanceCanvas;
    public Slider enhanceSlider;
    public Vector2 zoomMinMax;
    public Vector2 textureHeightMinMax;
    public float zoomSpeed;
    public RenderTexture renderTexture;
    public Camera zoomCamera;
    public Camera enhanceCamera;
    public RawImage renderImage;
    public Gradient enhanceGradient;
    float zoomBarWidth;
    float zoomAmount;
    public float zoomPercent;
    bool complete;
    float aspectRatio;
    int textureHeight;
    float previousTextureHeight = -1;
    AudioSource zoomAudio, enhanceAudio;
    float zoomPrev, enhancePrev;
    float soundResetTimer;

	void Start () {
        zoomBarWidth = zoomBar.sizeDelta.x;
        zoomAmount = zoomMinMax.y;
        aspectRatio = (float)Screen.width / (float)Screen.height;
        textureHeight = Mathf.FloorToInt(textureHeightMinMax.x);
        zoomAudio = zoomCamera.GetComponent<AudioSource>();
        enhanceAudio = enhanceCamera.GetComponent<AudioSource>();

        renderTexture = new RenderTexture(Mathf.FloorToInt(textureHeight * aspectRatio), textureHeight, 24);
	}
	
	void FixedUpdate () {
        if (!complete) {
            zoomPercent = 1 - (zoomAmount - zoomMinMax.x) / (zoomMinMax.y - zoomMinMax.x);
            zoomBar.sizeDelta = new Vector2(zoomPercent * zoomBarWidth, zoomBar.sizeDelta.y);

            if (textureHeight != previousTextureHeight) {
                renderTexture.Release();
                renderTexture = new RenderTexture(Mathf.FloorToInt(textureHeight * aspectRatio), textureHeight, 24);
                enhanceCamera.targetTexture = renderTexture;
                renderImage.texture = renderTexture;
            }
            previousTextureHeight = textureHeight;

            if (Input.touchCount > 0) {
                Vector2 deltaMove = Input.touches[0].deltaPosition;

                if (Input.touchCount >= 2) {
                    Touch touchOne = Input.touches[0];
                    Touch touchTwo = Input.touches[1];

                    Vector2 posDifference;

                    if (touchTwo.phase == TouchPhase.Began) {
                        posDifference = touchOne.position - touchTwo.position;
                    }


                    Vector2 prevPosOne = touchOne.position - touchOne.deltaPosition;
                    Vector2 prevPosTwo = touchTwo.position - touchTwo.deltaPosition;

                    float prevMagnitude = (prevPosOne - prevPosTwo).magnitude;
                    float magnitude = (touchOne.position - touchTwo.position).magnitude;

                    float difference = prevMagnitude - magnitude;

                    zoomAmount += difference * zoomSpeed;
                    zoomAmount = Mathf.Clamp(zoomAmount, zoomMinMax.x, zoomMinMax.y);
                    //zoomCamera.orthographicSize = zoomAmount;
                    enhanceCamera.fieldOfView = zoomAmount;
                }
            }

            if (zoomAmount == zoomMinMax.x) {
                enhanceCanvas.enabled = true;

                textureHeight = Mathf.FloorToInt((enhanceSlider.value * (textureHeightMinMax.y - textureHeightMinMax.x)) + textureHeightMinMax.x);


                if (enhanceSlider.value == 1) {
                    complete = true;
                    gameManager.instance.CompleteGame();
                }
            }
            else {
                enhanceCanvas.enabled = false;
            }
        }
        soundResetTimer -= Time.deltaTime;

        if (zoomPercent != zoomPrev && !zoomAudio.isPlaying)
            zoomAudio.Play();
        else if (zoomPercent == zoomPrev && soundResetTimer <= 0 && zoomAudio.isPlaying) {
            zoomAudio.Pause();
            soundResetTimer = 0.1f;
        }
        zoomPrev = zoomPercent;

        if (enhanceSlider.value != enhancePrev && !enhanceAudio.isPlaying)
            enhanceAudio.Play();
        else if (enhanceSlider.value == enhancePrev && soundResetTimer <= 0 && enhanceAudio.isPlaying) {
            enhanceAudio.Pause();
            soundResetTimer = 0.1f;
        }

        enhancePrev = enhanceSlider.value;

    }
}

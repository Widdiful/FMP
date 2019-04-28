﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MicManager : MonoBehaviour {

    public float levelMax;
    public float levelMaxRaw;
    public bool enablePlayback;

    private string deviceName;
    private AudioClip recording;
    private int sampleWindow = 128;

    private AudioSource source;

    public static MicManager instance;

    private void Awake() {
        if (instance == null)
            instance = this;
        if (instance != this)
            Destroy(this);
    }

    private void Start() {
        source = GetComponent<AudioSource>();
        StartMic();
    }

    private void Update() {
        // Get wave data of current position of microphone recording
        float tempMax = 0;
        float[] waveData = new float[sampleWindow];
        int micPosition = Microphone.GetPosition(null) - (sampleWindow + 1);
        if (micPosition < 0) {
            return;
        }
        recording.GetData(waveData, micPosition);

        // Get volume of audio peak
        for (int i = 0; i < sampleWindow; ++i) {
            float wavePeak = waveData[i] * waveData[i];
            if (tempMax < wavePeak) {
                tempMax = wavePeak;
            }
        }

        // Update raw and max values if game isn't paused
        if (Time.timeScale > 0) {
            levelMax = tempMax;
            levelMaxRaw = levelMax;
            if (gameManager.instance)
                levelMax /= gameManager.instance.micSensitivity;
        }

        // Playback audio
        if (source && !source.isPlaying && enablePlayback) {
            source.clip = recording;
            source.Play();
        }
    }

    void StartMic() {
        // Get default microphone information and start recording for 10 seconds
        deviceName = Microphone.devices[0];
        recording = Microphone.Start(deviceName, true, 10, 44100);
        if (source && enablePlayback) source.clip = recording;
    }

    void StopMic() {

    }
}

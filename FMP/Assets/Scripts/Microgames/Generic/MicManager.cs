using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MicManager : MonoBehaviour {

    public float levelMax;
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
        float tempMax = 0;
        float[] waveData = new float[sampleWindow];
        int micPosition = Microphone.GetPosition(null) - (sampleWindow + 1);
        if (micPosition < 0) {
            return;
        }
        recording.GetData(waveData, micPosition);
        for (int i = 0; i < sampleWindow; ++i) {
            float wavePeak = waveData[i] * waveData[i];
            if (tempMax < wavePeak) {
                tempMax = wavePeak;
            }
        }
        levelMax = tempMax;

        if (source && !source.isPlaying && enablePlayback) {
            source.clip = recording;
            source.Play();
        }
    }

    void StartMic() {
        deviceName = Microphone.devices[0];
        recording = Microphone.Start(deviceName, true, 10, 44100);
        if (source) source.clip = recording;
    }

    void StopMic() {

    }
}

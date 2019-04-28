using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu]
[System.Serializable]
public class Game : ScriptableObject {

    [System.Serializable]
    public struct UnlockedDifficulties {
        public bool easy;
        public bool normal;
        public bool hard;
        public bool relax;
    }

    public string name;
    public string displayName;
    public bool isLandscape;

    public bool useTap;
    public bool useMotion;
    public bool useMic;
    public bool useProximity;

    public bool hasExtraDifficulty;
    public string hint;

    public UnlockedDifficulties unlocked;

    public int unlockCost;

    public Sprite icon;
    public Sprite previewImage;
    public Sprite lockedImage;

    public bool IsUnlocked() {
        return unlocked.easy || unlocked.normal || unlocked.hard || unlocked.relax;
    }
}

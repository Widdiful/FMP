using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProfileEditor : MonoBehaviour {

    public Image avatarPreview;
    public Transform avatarTransform;
    public GameObject avatarPrefab;
    public Slider redSlider, greenSlider, blueSlider;
    public InputField nameField;
    private int avatarID;
    private string playerName;
    private string colour;

    public static ProfileEditor instance;

    private void Awake() {
        if (!instance)
            instance = this;
        else
            Destroy(this);
    }

    private void Start() {
        for (int i = 0; i < DatabaseManager.instance.avatars.Count; i++) {
            AvatarSelectButton newButton = Instantiate(avatarPrefab, avatarTransform).GetComponent<AvatarSelectButton>();
            newButton.avatarID = i;
            newButton.UpdateUI();
        }
    }

    public void Initialise(string newName, int newAvatar, string newColour) {
        playerName = newName;
        colour = newColour;
        nameField.text = playerName;

        SelectAvatar(newAvatar);

        Color convertedColour;
        if (ColorUtility.TryParseHtmlString("#" + colour, out convertedColour)) {
            avatarPreview.color = convertedColour;
        }

        redSlider.value = convertedColour.r;
        greenSlider.value = convertedColour.g;
        blueSlider.value = convertedColour.b;
    }

    public void UpdateColours() {
        float r, g, b;
        r = redSlider.value;
        g = greenSlider.value;
        b = blueSlider.value;

        Color newColour = new Color(r, g, b);
        avatarPreview.color = newColour;
        colour = ColorUtility.ToHtmlStringRGB(newColour);
    }

    public void SelectAvatar(int id) {
        avatarID = id;
        avatarPreview.sprite = DatabaseManager.instance.avatars[avatarID];
    }

    public void SaveChanges() {
        DatabaseManager.instance.EditUserInfo(nameField.text, avatarID, colour);
    }
}

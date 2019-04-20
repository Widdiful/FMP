using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AvatarSelectButton : MonoBehaviour {

    public Image image;
    public int avatarID;

    public void UpdateUI() {
        image.sprite = DatabaseManager.instance.avatars[avatarID];
    }

    public void Click() {
        ProfileEditor.instance.SelectAvatar(avatarID);
    }
}

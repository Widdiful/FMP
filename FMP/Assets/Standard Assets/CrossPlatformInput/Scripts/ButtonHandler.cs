using System;
using UnityEngine;
using UnityEngine.UI;

namespace UnityStandardAssets.CrossPlatformInput
{
    public class ButtonHandler : MonoBehaviour
    {

        public string Name;

        public Image img;
        public Sprite upSprite, downSprite;

        void OnEnable()
        {

        }

        public void SetDownState()
        {
            CrossPlatformInputManager.SetButtonDown(Name);
            if (img) {
                img.sprite = downSprite;
            }
        }


        public void SetUpState()
        {
            CrossPlatformInputManager.SetButtonUp(Name);
            if (img) {
                img.sprite = upSprite;
            }
        }


        public void SetAxisPositiveState()
        {
            CrossPlatformInputManager.SetAxisPositive(Name);
        }


        public void SetAxisNeutralState()
        {
            CrossPlatformInputManager.SetAxisZero(Name);
        }


        public void SetAxisNegativeState()
        {
            CrossPlatformInputManager.SetAxisNegative(Name);
        }

        public void Update()
        {

        }
    }
}

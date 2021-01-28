using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Utilities.UI.Audio
{
    public class DropdownAudio : UIAudio
    {
        public Button dropdownOpenButton;
        public TMP_Dropdown dropdown;
        public Utilities.Audio.ClipInfo changeSound;
        public Utilities.Audio.ClipInfo openSound;

        private void Reset()
        {
            dropdown = gameObject.GetComponent<TMP_Dropdown>();
            dropdownOpenButton = gameObject.GetComponentInChildren<Button>();
        }

        private void Awake()
        {/*
            dropdownOpenButton.onClick.AddListener(() =>
            {
                Play(openSound);
            });

          */
            dropdown.onValueChanged.AddListener((val) =>
          {
              Play(changeSound);
          });
        }
    }
}
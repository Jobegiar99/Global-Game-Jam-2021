using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace Utilities.UI
{
    public class FormInputField : FormField
    {
        public TMP_InputField Field => selectable as TMP_InputField;
        public Image icon;
        public TextMeshProUGUI infoLabel;
        public Lean.Localization.LeanLocalizedTextMeshProUGUI infoLocalization;

        public enum State { Unused, Error, Valid };

        public ColorPalette defaultPalette;
        public ColorPalette validPalette;
        public ColorPalette errorPalette;

        public Sprite validSprite;
        public Sprite errorSprite;

        public bool scaleParentForLbl = true;
        public Events.Event OnChangeState { get; set; }
        public State state;

        public ColorPalette CurrentPalette
        {
            get
            {
                switch (state)
                {
                    case State.Unused:
                        return defaultPalette;

                    case State.Error:
                        return errorPalette;

                    case State.Valid:
                        return validPalette;

                    default:
                        return defaultPalette;
                }
            }
        }

        public Sprite CurrentIcon
        {
            get
            {
                switch (state)
                {
                    case State.Unused:
                        return null;

                    case State.Error:
                        return errorSprite;

                    case State.Valid:
                        return validSprite;

                    default:
                        return null;
                }
            }
        }

        public void SetState(bool isValid, string infoText = "")
        {
            var state = isValid ? State.Valid : State.Error;
            if (string.IsNullOrEmpty(Field.text) && !required) { state = State.Unused; infoText = ""; }
            this.state = state;
            infoLocalization.TranslationName = infoText;
            infoLabel.color = CurrentPalette.mainTone2;
            icon.sprite = CurrentIcon;
            icon.color = CurrentPalette.mainTone1;
            icon.enabled = icon.sprite != null;
            OnChangeState?.Invoke();
        }
    }
}
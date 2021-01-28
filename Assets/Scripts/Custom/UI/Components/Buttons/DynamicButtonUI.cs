using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Utilities.UI
{
    public class DynamicButtonUI : MonoBehaviour
    {
        public int firstState = -1;
        public Button button;
        public List<DynamicButtonState> states;
        public DynamicButtonState CurrentState { get; private set; }

        public Image icon;
        public Lean.Localization.LeanLocalizedTextMeshProUGUI localizeTitle;
        public Lean.Localization.LeanLocalizedTextMeshProUGUI localizeSubtitle;

        public void Awake()
        {
            if (firstState > -1) SetState(firstState);
        }

        public void SetState(int id)
        {
            if (CurrentState != null) { }
            CurrentState = states[id];
            icon.sprite = CurrentState.icon;
            localizeTitle.TranslationName = CurrentState.title;
            localizeSubtitle.TranslationName = CurrentState.subtitle;
        }

        public void SetState(string name)
        {
            for (int i = 0; i < states.Count; i++)
            {
                if (states[i].name == name)
                {
                    SetState(i);
                    break;
                }
            }
        }
    }
}
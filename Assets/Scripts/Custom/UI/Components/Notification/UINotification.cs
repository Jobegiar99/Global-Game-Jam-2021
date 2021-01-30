using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;
using Utilities.UI.Animation;
using Lean.Localization;

namespace Utilities.UI
{
    public class UINotification : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] public UIAnimator anim;

        [SerializeField] private RectTransform rt;
        [SerializeField] private TextMeshProUGUI title;

        //[SerializeField] private LeanLocalizedTextMeshProUGUI titleLoc;
        [SerializeField] private TextMeshProUGUI content;

        //[SerializeField] private LeanLocalizedTextMeshProUGUI contentLoc;
        [SerializeField] private TextMeshProUGUI actionButtonLbl;

        [SerializeField] private LeanLocalizedTextMeshProUGUI actionButtonLblLoc;
        [SerializeField] private Image iconImg;
        [SerializeField] private Image headerBckgrnd;
        [SerializeField] private Image contentBckgrnd;
        [SerializeField] private RectTransform headerRT;
        [SerializeField] private RectTransform contentRT;
        [SerializeField] private Button closeButton;
        [SerializeField] private Button actionButton;
        [SerializeField] private float duration;
        [SerializeField] private IEnumerator HideEnumerator;
        private Utilities.Audio.ClipInfo audioClip;
        //private string currentTitle = "";
        //private string currentContent = "";

        private void UpdateText(UINotificationInfo info)
        {
            UINotificationMngr.Instance.ExecuteNextFrame(() =>
            {
                title.text = LeanLocalization.GetTranslationText(info.title, info.title, true);
                content.text = LeanLocalization.GetTranslationText(info.content, info.content, true);
                // currentTitle;
            });
        }

        public void SetValue(UINotificationInfo info)
        {
            duration = info.duration;
            UpdateText(info);

            //titleLoc.TranslationName = info.title;

            title.color = info.palette.textColor;

            //contentLoc.TranslationName = info.content;
            content.color = info.palette.textColor2;

            headerBckgrnd.color = info.palette.mainTone1;
            contentBckgrnd.color = info.palette.mainTone2;
            audioClip = info.audio;

            Vector2 sizedDelta = rt.sizeDelta;
            sizedDelta.y = headerRT.sizeDelta.y;
            bool hasContent = (!string.IsNullOrEmpty(info.actionButtonText) || !string.IsNullOrEmpty(info.content));
            sizedDelta.y +=
               hasContent ?
                contentRT.sizeDelta.y : 0;

            contentRT.gameObject.SetActive(hasContent);
            rt.sizeDelta = sizedDelta;

            actionButton.targetGraphic.color = info.palette.secondaryTone1;
            actionButtonLbl.color = info.palette.textColor;
            closeButton.targetGraphic.color = iconImg.color = info.palette.secondaryTone2;

            actionButton.gameObject.SetActive(!string.IsNullOrEmpty(info.actionButtonText));
            if (actionButton.gameObject.activeSelf)
            {
                actionButtonLblLoc.TranslationName = info.actionButtonText;
                actionButton.onClick.RemoveAllListeners();
                actionButton.onClick.AddListener(info.actionButtonHandler.Invoke);
                actionButton.onClick.AddListener(Hide);
                content.rectTransform.SetRight(190);
            }
            else
            {
                content.rectTransform.SetRight(20);
            }

            iconImg.gameObject.SetActive(info.icon != null);
            iconImg.sprite = info.icon;
            Show();
        }

        private void Awake()
        {
            anim.OnAnimationFinish += value => gameObject.SetActive(value);
            closeButton.onClick.AddListener(Hide);
        }

        public void Show()
        {
            if (InUse) return;
            gameObject.SetActive(true);
            InUse = true;
            anim.Show();
            if (duration > 0)
            {
                HideEnumerator = MonoExtensions.Later(Hide, duration);
                StartCoroutine(HideEnumerator);
            }
            //  PlayAudio(audioClip);
        }

        public void Hide()
        {
            if (!InUse) return;
            if (HideEnumerator != null) StopCoroutine(HideEnumerator);
            InUse = false;
            anim.Hide();
        }

        public UINotification PlayAudio(Utilities.Audio.ClipInfo clip)
        {
            Utilities.Audio.AudioMngr.Player(Utilities.Audio.AudioMngr.Type.UI).Play(clip);
            return this;
        }

        public bool InUse { get; private set; } = false;
    }

    [System.Serializable]
    public struct UINotificationInfo
    {
        public string title;
        public string content;
        public Sprite icon;
        public bool hasCloseButton;
        public string actionButtonText;
        public ColorPalette palette;
        public Events.Event actionButtonHandler;
        public float duration;
        public Utilities.Audio.ClipInfo audio;
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Utilities.UI.Animation;

namespace Utilities.UI
{
    public class Tooltip : MonoBehaviour
    {
        public TextMeshProUGUI growthText;
        public TextMeshProUGUI label;
        public UIScreenConnector connector;
        public UIAnimator anim;

        public static Tooltip Instance { get; private set; }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                connector.onUpdate = false;
            }
            else { Destroy(gameObject); }
        }

        public void Show(string text, RectTransform anchor)
        {
            growthText.text = label.text = text;
            connector.Position = anchor.position;
            connector.offset2D = -Vector2.up * (anchor.sizeDelta.y * 0.5f + growthText.rectTransform.sizeDelta.y * 0.5f);
            connector.SetPosition();
            anim.Show();
        }

        public void Hide()
        {
            anim.Hide();
        }

        public bool Showing => anim.Showing;
    }
}
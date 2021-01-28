using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;
using Utilities.Math;

namespace Utilities.UI
{
    [System.Serializable]
    public class PointsUIInfo : System.ICloneable
    {
        public string text = "";
        public Sprite image = null;
        public Color color = Color.white;
        public bool applyColorToImg = false;
        public float speed = 1;
        public float size = 1;

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }

    public class PointsUI : MonoBehaviour
    {
        public TextMeshProUGUI text;
        public Image image;
        public Animator anim;
        public CanvasGroup cg;
        private float originalFontSize;
        private float originalIconSize;

        [SerializeField] private RectTransform canvasRT;

        public RectTransform CanvasRT
        {
            get
            {
                if (canvasRT == null)
                {
                    canvasRT = GetComponentInParent<Canvas>().GetComponent<RectTransform>();
                }
                return canvasRT;
            }
        }

        public RectTransform rt;
        public float showTime = 0.5f;

        private int hash = Animator.StringToHash("Show");

        private void Awake()
        {
            cg.alpha = 0;
            originalFontSize = text.fontSize;
            originalIconSize = image.rectTransform.sizeDelta.x;
        }

        public void Show(PointsUIInfo info, Transform target)
        {
            Show(info, target.position);
        }

        public void Show(PointsUIInfo info, Vector3 position)
        {
            Show(info, CustomMath.WolrdToCanvas(position, CanvasRT));
        }

        public void Show(PointsUIInfo info, Vector2 pos)
        {
            rt.anchoredPosition = pos;
            Show(info);
        }

        public void Show(PointsUIInfo info)
        {
            transform.localScale = Vector3.one * info.size;
            anim.speed = info.speed;
            text.text = info.text;
            text.color = info.color;
            cg.alpha = 1;
            image.gameObject.SetActive(info.image != null);
            image.sprite = info.image;
            image.color = info.applyColorToImg ? info.color : Color.white;
            anim.SetTrigger(hash);
            this.ExecuteLater(Hide, showTime / info.speed);
        }

        public void Hide()
        {
            cg.alpha = 0;
        }
    }
}
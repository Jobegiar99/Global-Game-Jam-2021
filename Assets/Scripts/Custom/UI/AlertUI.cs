using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Utilities.UI
{
    public class AlertUI : MonoBehaviour
    {
        public float timeToHide = 2f;
        public CanvasGroup cg;
        public Image image;
        public Text text;
        public Transform alertT;
        public UITransformConnector connector;
        IEnumerator oldHideCoroutine;

        [HideInInspector] public static AlertUI ins { get { if (!_ins) _ins = FindObjectOfType<AlertUI>(); return _ins; } }
        static AlertUI _ins = null;




        public void Reset()
        {
            alertT = transform.GetChild(0);
            cg = GetComponent<CanvasGroup>();
            connector = Helper.GetAddComponent<UITransformConnector>(gameObject);
            text = GetComponentInChildren<Text>();
            image = GetComponentInChildren<Image>();
        }

        public void Awake()
        {
            Hide();
        }

        public void Show(Sprite icon, string str, Transform target = null, Vector2 offset = default(Vector2))
        {
            if (target)
                connector.connectTo = target;
            alertT.localPosition = Vector2.zero + offset;
            image.sprite = icon;
            text.text = str;
            Show();
        }

        public void Show()
        {
            EaseInOutAnimation.ins.Animate(true, cg, 0.5f);

            //Desaparecer en n segundos

            if (oldHideCoroutine != null)
                StopCoroutine(oldHideCoroutine);
            oldHideCoroutine = HideInSeconds();
            StartCoroutine(oldHideCoroutine);
        }

        public void Hide()
        {
            EaseInOutAnimation.ins.Animate(false, cg, 0.5f);
        }

        IEnumerator HideInSeconds()
        {
            yield return new WaitForSeconds(timeToHide);
            Hide();
        }
    }
}
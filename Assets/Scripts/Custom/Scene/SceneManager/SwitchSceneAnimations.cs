using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Utilities.UI.Animation;

public class SwitchSceneAnimations : MonoBehaviour
{
    [SerializeField] private CanvasGroup cg;
    [SerializeField] private UIAnimator animator;
    [SerializeField] private Image image;
    private System.Action OnDone;

    public static SwitchSceneAnimations Instance { get; private set; }

    private void Awake()
    {
        cg.blocksRaycasts = cg.interactable = false;
        if (Instance == null)
        {
            animator.OnAnimationFinish += (val) =>
            {
                if (!val) cg.blocksRaycasts = cg.interactable = false;
                image.fillOrigin = val ? (int)Image.OriginHorizontal.Right : (int)Image.OriginHorizontal.Left;
                OnDone?.Invoke();
            };

            animator.OnStartAnimation += (val) =>
            {
                cg.blocksRaycasts = cg.interactable = true;
            };

            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else { Destroy(gameObject); }
    }

    public void In(System.Action onDone = null)
    {
        if (animator.Showing) { onDone?.Invoke(); return; }
        OnDone = onDone;
        animator.Show();
    }

    public void Out(System.Action onDone = null)
    {
        if (!animator.Showing) { onDone?.Invoke(); return; }
        OnDone = onDone;
        animator.Hide();
    }
}
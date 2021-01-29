using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities.UI;
using Utilities.UI.Animation;

public class InteractMenuHUD : MonoBehaviour
{
    public bool IsOpen => animator.Showing;
    public UIAnimator animator;
    public UITransformConnector connector;

    public virtual void Update()
    {
        if (Input.GetMouseButtonDown(1)) Close();
    }

    public virtual void Open()
    {
        animator.Show();
    }

    public virtual void Close()
    {
        animator.Hide();
    }
}
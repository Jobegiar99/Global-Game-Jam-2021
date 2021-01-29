using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities.UI;
using Utilities.UI.Animation;

public class OpenIndicatorUI : MonoBehaviour
{
    public static OpenIndicatorUI Instance { get; private set; }
    public UIAnimator anim;
    public UITransformConnector connector;

    private void Awake()
    {
        if (!Instance) Instance = this;
        else Destroy(gameObject);
    }

    public void SetTarget(Transform target)
    {
        if (target) connector.connectTo = target;
        if (target == null) anim.Hide();
        else anim.Show();
    }

    public void RemoveTarget()
    {
        SetTarget(null);
    }
}
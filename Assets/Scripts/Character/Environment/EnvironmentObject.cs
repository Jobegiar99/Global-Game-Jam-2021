using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnvironmentObject : MonoBehaviour
{
    public struct ClickData
    {
        public Transform transform;
        public Vector3 position;
        public EnvironmentObject environment;
    }

    public bool IsHover { get; protected set; } = false;

    public void Hover(bool val)
    {
        if (val == IsHover) return;
        IsHover = val;
        if (val) HoverActions();
        else UnhoverActions();
    }

    protected abstract void HoverActions();

    protected abstract void UnhoverActions();

    public abstract ClickData OnClick(RaycastHit hit, PlayerController player);
}
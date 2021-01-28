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

    public abstract ClickData OnClick(RaycastHit hit, PlayerController player);
}
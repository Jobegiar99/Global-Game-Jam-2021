using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerEffects : MonoBehaviour
{
    public PlayerController controller;
    public Transform cube;

    private void Start()
    {
        controller.MoveStatus.OnStartMove += (target) =>
        {
            cube.transform.position = target + Vector3.up * 0.1f;
        };
    }
}
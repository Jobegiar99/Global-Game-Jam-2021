using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentInteractable : EnvironmentObject
{
    public Transform usePosition;

    public override ClickData OnClick(RaycastHit hit, PlayerController player)
    {
        var data = new ClickData()
        {
            environment = this,
            position = usePosition.position,
            transform = usePosition
        };
        player.Move(data, null, () =>
        {
            Debug.Log("interact");
        });

        return data;
    }
}
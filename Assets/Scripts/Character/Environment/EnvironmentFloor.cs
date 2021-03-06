using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentFloor : EnvironmentObject
{
    protected override void HoverActions()
    {
    }

    public override ClickData OnClick(RaycastHit hit, PlayerController player)
    {
        var data = new ClickData()
        {
            position = hit.point,
            transform = transform,
            environment = this
        };

        player.Move(data);
        return data;
    }

    protected override void UnhoverActions()
    {
    }
}
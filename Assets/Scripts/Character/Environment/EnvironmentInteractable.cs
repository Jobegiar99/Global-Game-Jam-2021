using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentInteractable : EnvironmentObject
{
    public Transform usePosition;
    public InteractMenuHUD menu;

    private void Awake()
    {
        menu.connector.connectTo = transform;
    }

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
            menu.Open();
            UnhoverActions();
        });

        return data;
    }

    protected override void HoverActions()
    {
        OpenIndicatorUI.Instance.SetTarget(transform);
    }

    protected override void UnhoverActions()
    {
        OpenIndicatorUI.Instance.SetTarget(null);
    }
}
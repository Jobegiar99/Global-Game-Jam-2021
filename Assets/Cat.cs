using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities.Audio;

public class Cat : EnvironmentObject
{
    public Transform usePosition;
    public Animator anim;
    private int petId = Animator.StringToHash("Pet");
    public List<ClipInfo> sounds = new List<ClipInfo>();
    public PlayerController player;

    private void Awake()
    {
        player.OnStopPet += () =>
        {
            if (IsHover) HoverActions();
        };
        //menu.connector.connectTo = transform;
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
            Pet(player);
        });

        return data;
    }

    public void Pet(PlayerController player)
    {
        player.Pet(this);
        AudioMngr.Player(AudioMngr.Type.SFX).Play(
            sounds[Random.Range(0, sounds.Count)]
            );
        UnhoverActions();
    }

    protected override void HoverActions()
    {
        if (player.IsPetting) return;
        OpenIndicatorUI.Instance.SetTarget(transform);
    }

    protected override void UnhoverActions()
    {
        OpenIndicatorUI.Instance.SetTarget(null);
    }
}
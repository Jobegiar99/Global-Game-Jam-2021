using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities.Audio;

public class PlayerAnimation : MonoBehaviour
{
    public PlayerController controller;
    public Animator anim;
    public ClipInfo walkSFX;
    public AudioSourceInfo source;

    private int walkId = Animator.StringToHash("Walk");
    private int petId = Animator.StringToHash("Pet");

    private void Reset()
    {
        controller = GetComponent<PlayerController>();
        anim = GetComponentInChildren<Animator>();
    }

    private void OnDestroy()
    {
        if (source != null) { AudioMngr.Player(AudioMngr.Type.SFX).FreeSource(source, source.playId); }
    }

    private void Start()
    {
        if (!controller) controller = GetComponent<PlayerController>();

        source = AudioMngr.Player(AudioMngr.Type.SFX).Play(walkSFX);
        source.source.volume = 0f;
        controller.MoveStatus.OnStartMove += (data) =>
        {
            if (controller.MoveStatus.Moving) return;//Ya se estaba moviendo
            anim.SetBool(walkId, true);
            targetVolume = walkSFX.volume;

            //if (source != null) { AudioMngr.Player(AudioMngr.Type.SFX).FreeSource(source, source.playId); }
        };
        controller.MoveStatus.OnStopMove += () =>
        {
            anim.SetBool(walkId, false);
            targetVolume = 0f;
            //if (source != null) { AudioMngr.Player(AudioMngr.Type.SFX).FreeSource(source, source.playId); source = null; }
        };

        controller.OnPet += () => { anim.SetTrigger("Pet"); };
    }

    private float targetVolume = 0f;

    // Update is called once per frame
    private void Update()
    {
        source.source.volume = Mathf.Lerp(source.source.volume, targetVolume, Time.deltaTime * 10f);
    }
}
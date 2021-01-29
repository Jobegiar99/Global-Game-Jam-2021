using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class CustomAudioClip : PlayableAsset
{
    public Utilities.Audio.ClipInfo clip;
    public Utilities.Audio.AudioMngr.Type type = Utilities.Audio.AudioMngr.Type.SFX;

    public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
    {
        var playable = ScriptPlayable<CustomAudioBehaviour>.Create(graph);
        CustomAudioBehaviour behaviour = playable.GetBehaviour();
        behaviour.clip = clip;
        behaviour.type = type;
        return playable;
    }
}
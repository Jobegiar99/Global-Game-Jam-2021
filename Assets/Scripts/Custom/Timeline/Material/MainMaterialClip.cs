using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class MainMaterialClip : PlayableAsset
{
    public bool disappear;

    public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
    {
        var playable = ScriptPlayable<MainMaterialBehaviour>.Create(graph);
        MainMaterialBehaviour behaviour = playable.GetBehaviour();
        behaviour.disappear = disappear;
        return playable;
    }
}
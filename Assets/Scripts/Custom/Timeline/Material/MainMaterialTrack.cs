using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[TrackBindingType(typeof(Material))]
[TrackClipType(typeof(MainMaterialClip))]
public class MainMaterialTrack : TrackAsset
{
    public override Playable CreateTrackMixer(PlayableGraph graph, GameObject go, int inputCount)
    {
        return ScriptPlayable<MainMaterialMixer>.Create(graph, inputCount);
    }
}
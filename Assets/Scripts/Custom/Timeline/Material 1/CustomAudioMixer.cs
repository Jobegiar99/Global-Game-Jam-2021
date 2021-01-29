using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using Utilities.Audio;

public class CustomAudioMixer : PlayableBehaviour
{
    public ClipInfo oldClip;
    public ClipInfo clip;
    public AudioMngr.Type type;

    public override void ProcessFrame(Playable playable, FrameData info, object playerData)
    {
        int inputCount = playable.GetInputCount();
        clip = null;

        for (int i = 0; i < inputCount; i++)
        {
            float inputWeight = playable.GetInputWeight(i);
            if (inputWeight > 0)
            {
                ScriptPlayable<CustomAudioBehaviour> inputPlayable = (ScriptPlayable<CustomAudioBehaviour>)playable.GetInput(i);
                CustomAudioBehaviour input = inputPlayable.GetBehaviour();
                this.clip = input.clip;
                this.type = input.type;
            }
        }

        if (clip == null) { oldClip = null; }
        if (clip != null && clip != oldClip)
        {
            oldClip = clip;
            if (Application.isPlaying) AudioMngr.Player(type).Play(clip);
        }
    }
}
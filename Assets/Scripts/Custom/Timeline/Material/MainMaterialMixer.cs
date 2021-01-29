using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class MainMaterialMixer : PlayableBehaviour
{
    public override void ProcessFrame(Playable playable, FrameData info, object playerData)
    {
        Material mat = playerData as Material;

        if (!mat || !mat.HasProperty("Amount")) return;

        float finalAmount = mat.GetFloat("Amount");
        int inputCount = playable.GetInputCount();

        for (int i = 0; i < inputCount; i++)
        {
            float inputWeight = playable.GetInputWeight(i);
            if (inputWeight > 0)
            {
                ScriptPlayable<MainMaterialBehaviour> inputPlayable = (ScriptPlayable<MainMaterialBehaviour>)playable.GetInput(i);
                MainMaterialBehaviour input = inputPlayable.GetBehaviour();
                if (input.disappear) finalAmount = inputWeight;
                else finalAmount = 1 - inputWeight;
            }
        }

        mat.SetFloat("Amount", finalAmount);
    }
}
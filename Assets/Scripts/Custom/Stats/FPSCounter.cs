using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Utilities.Stats
{
    public class FPSCounter : Stats
    {
        public TMPro.TextMeshProUGUI label;
        public float Count { get; private set; }
        public double Average { get; private set; }
        private double Total { get; set; } = 0;

        private long RecordedTicks { get; set; } = 0;

        public override void SetState(StatState state)
        {
            label.color = state.color;
        }

        public override void UpdateStat()
        {
            Count = (1 / Time.deltaTime);
            label.text = (Mathf.Round(Count)) + " FPS";

            RecordedTicks++;
            Total += Count;
            Average = Total / RecordedTicks;
            Value = Count;
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utilities.Stats
{
    public abstract class Stats : MonoBehaviour
    {
        public virtual float UpdateRate { get => 0.5f; }
        public float Value { get; protected set; }
        [Tooltip("Bad->Good")] public StatState[] state;

        public virtual void SetState(StatState state)
        {
        }

        public void UpdateState()
        {
            if (state.Length == 0) return;

            for (int i = state.Length - 1; i >= 0; i--)
            {
                if (Value > state[i].minValue) { SetState(state[i]); return; }
            }
        }

        private IEnumerator Start()
        {
            while (true)
            {
                UpdateStat();
                UpdateState();
                yield return new WaitForSeconds(UpdateRate);
            }
        }

        public abstract void UpdateStat();
    }

    [System.Serializable]
    public struct StatState
    {
        public float minValue;
        public Color color;
    }
}
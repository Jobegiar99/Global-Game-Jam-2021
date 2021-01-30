using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utilities.UI.Audio
{
    public class HoldButtonAudio : UIAudio
    {
        public HoldButtonUI holdButton;
        public Utilities.Audio.ClipInfo holdAudio;
        public Utilities.Audio.ClipInfo finishAudio;

        private void Reset()
        {
            holdButton = GetComponent<HoldButtonUI>();
        }

        public void Awake()
        {
            holdButton.onHoldStart.AddListener(() => Play(holdAudio));
            holdButton.onHoldFinish.AddListener(() => Play(finishAudio));
            holdButton.onHoldCancel.AddListener(() => CurrentSource?.source.Stop());
        }
    }
}
using Utilities.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Utilities.UI.Animation;
using Utilities.Audio;

namespace Utilities.UI
{
    public class SlotEffects<T> : MonoBehaviour where T : SlotElement
    {
        public SlotUI<T> slot;
        public UIAnimator hoverAnim;
        public UIAnimator pressAnim;

        public Image hover;
        public Image icon;

        public Color hoverColor;
        public Color goodColor;
        public Color badColor;

        public ClipInfo overAudio;

        private void Awake()
        {
            _Awake();
        }

        public void _Awake()
        {
            slot = GetComponent<SlotUI<T>>();

            hoverAnim.OnAnimationFinish += (val) =>
            {
                //    if (val && overAudio.clip != null) Audio.AudioMngr.Player(Audio.AudioMngr.Type.UI).Play(overAudio);
            };

            slot.OnAdd.AddListener((val) =>
            {
                if (hover) hover.color = hoverColor;
            });
            slot.OnUpdate.AddListener((val, val1) =>
            {
                if (hover) hover.color = hoverColor;
            });

            slot.OnRemove.AddListener((val) =>
            {
                if (hover) hover.color = hoverColor;
            });

            slot.OnHoverEvent.AddListener(() =>
            {
                if (hover) hover.color = hoverColor;

                if (slot.Group.Dragging)
                {
                    var dragging = SlotGroup<T>.DraggingSlot;
                    if (slot.CanAddFrom(dragging) && (!slot.InUse || dragging.CanAddFrom(slot)))
                    {
                        if (hover) hover.color = goodColor;
                    }
                    else
                    {
                        if (hover) hover.color = badColor;
                    }
                }
                hoverAnim?.Show();

                if (hover) hover.enabled = true;
            });

            slot.OnUnhoverEvent.AddListener(() =>
            {
                hoverAnim?.Hide();
                if (hover) hover.enabled = false;
            });
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

namespace Utilities.UI
{
    public class ButtonGroup : MonoBehaviour
    {
        //public List<Button> buttons = new List<Button>();
        public Button selected;

        public void SelectButton(Button button)
        {
            selected = button;
            this.ExecuteLater(button.Select, 0.33f);
        }

        /*
        private readonly Dictionary<Button, ButtonInGroup> buttons = new Dictionary<Button, ButtonInGroup>();
        public List<ButtonGroupRow> rows = new List<ButtonGroupRow>();
        private ButtonInGroup selected = null;
        private bool alreadyAwake = false;

        public void Awake()
        {
            if (alreadyAwake) return;

            rows.ForEach(el =>
            {
                el.columns.ForEach(btn =>
                {
                    buttons.Add(btn.button, btn);
                    Events.BoolEvent hover = (val) =>
                      {
                          if (val)
                          {
                              if (selected != null)
                              {
                                  selected.Enable(false);
                              }
                              btn.Enable(true);
                              selected = btn;
                          }
                      };

                    btn.Init(hover);
                });
            });

            rows[0].columns[0].Enable(true);
            selected = rows[0].columns[0];
            alreadyAwake = true;
        }

        public void LockButton(Button btn, bool lockValue)
        {
            ButtonInGroup bg = buttons[btn];
            bg.Lock(lockValue, bg.enabled);
        }

        public void LockButton(Button btn, bool lockValue, bool enabled)
        {
            buttons[btn].Lock(lockValue, enabled);
        }*/
    }

    [System.Serializable]
    public class ButtonGroupRow
    {
        public List<ButtonInGroup> columns = new List<ButtonInGroup>();

        public void Init(Events.BoolEvent hover)
        {
            columns.ForEach(el => el.Init(hover));
        }
    }

    [System.Serializable]
    public class ButtonInGroup
    {
        public bool enabled = false;
        public CanvasGroup cg;
        public Button button;
        public bool locked = false;

        [HideInInspector] public Events.BoolEvent hover;

        public void Init(Events.BoolEvent hover)
        {
            cg = Helper.GetAddComponent<CanvasGroup>(button.gameObject);

            EventTrigger trigger = Helper.GetAddComponent<EventTrigger>(button.gameObject);
            EventTrigger.Entry enter = new EventTrigger.Entry();
            enter.eventID = EventTriggerType.PointerEnter;
            enter.callback.AddListener(e => { hover?.Invoke(true); });

            EventTrigger.Entry exit = new EventTrigger.Entry();
            exit.eventID = EventTriggerType.PointerExit;
            exit.callback.AddListener(e => { hover?.Invoke(false); });

            trigger.triggers.Add(enter);
            trigger.triggers.Add(exit);

            Enable(false);
        }

        public void Enable(bool value)
        {
            if (locked) return;
            enabled = value;
            cg.alpha = value ? 1 : 0.15f;
        }

        public void Lock(bool locked, bool enabled)
        {
            bool initialLock = this.locked;
            if (!this.locked) { Enable(enabled); }
            this.locked = locked;
            if (initialLock) { Enable(enabled); }
        }
    }
}
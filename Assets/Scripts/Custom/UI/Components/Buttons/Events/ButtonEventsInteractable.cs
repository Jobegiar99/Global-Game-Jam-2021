using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

namespace Utilities.UI
{
    public class ButtonEventsInteractable : MonoBehaviour
    {
        public Button button;
        public UnityEvent onEnable;
        public UnityEvent onDisable;
        private bool interactable = false;

        public void Reset()
        {
            button = GetComponent<Button>();
        }

        public void Awake()
        {
            Trigger();
        }

        public void Trigger()
        {
            if (button.interactable) onEnable.Invoke();
            else onDisable.Invoke();
            interactable = button.interactable;
        }

        public void Update()
        {
            if (interactable != button.interactable)
            {
                Trigger();
            }
        }
    }
}
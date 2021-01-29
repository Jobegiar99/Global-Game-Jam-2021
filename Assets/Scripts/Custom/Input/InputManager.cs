using System.Collections.Generic;
using UnityEngine;

namespace Utilities._Input
{
    public class InputManager : MonoBehaviour
    {
        public static InputManager Instance;
        public string CurrentFocus { get; private set; } = null;
        public List<IFocus> inputs = new List<IFocus>();

        private void Awake()
        {
            if (Instance == null) { Instance = this; }
            else if (Instance != this) { Destroy(this); }
        }

        private void _Select(string focus)
        {
            CurrentFocus = focus;
        }

        public void _Select(IFocus focus)
        {
            CurrentFocus = focus?.FocusGroup;
        }

        public bool _HasFocus(IFocus focus)
        {
            return focus.FocusGroup == CurrentFocus;
        }

        public static void Select(string focus)
        {
            Instance._Select(focus);
        }

        public static void Select(IFocus focus)
        {
            Instance._Select(focus);
        }

        public static bool HasFocus(IFocus focus)
        {
            return Instance._HasFocus(focus);
        }

        public static bool HasFocus(string focus)
        {
            return Instance.CurrentFocus == focus;
        }
    }
}
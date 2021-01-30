using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utilities._Input
{
    public static class InputHelper
    {
        public static string MenuFocus => "menu";
        public static string WindowFocus => "menu_window";
        public static string GameFocus => "game";
        public static string PauseFocus => "pause";
        public static string MiniGameFocus => "minigame";

        public static void Select(this IFocus focus)
        {
            InputManager.Instance._Select(focus);
        }

        public static bool HasFocus(this IFocus focus)
        {
            return InputManager.Instance._HasFocus(focus);
        }
    }
}
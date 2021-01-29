using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities._Input;

namespace Utilities.UI.Menu
{
    public class WindowMenu : MenuBase
    {
        public override string FocusGroup => "window";
        private string previousFocus;

        public override void UpdateActions()
        {
            if (Input.GetKeyDown(KeyCode.Escape)) { Hide(); }
        }

        public override void Show()
        {
            if (!IsShowing) previousFocus = InputManager.Instance.CurrentFocus;
            base.Show();
            this.Select();
        }

        public override void Hide()
        {
            if (IsShowing)
                this.ExecuteNextFrame(() =>
                {
                    InputManager.Select(previousFocus);
                });

            base.Hide();
        }
    }
}
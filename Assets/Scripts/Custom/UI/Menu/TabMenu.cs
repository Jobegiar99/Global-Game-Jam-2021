using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Utilities.UI.Animation;

namespace Utilities.UI.Menu
{
    public class TabMenu : MonoBehaviour
    {
        public Tab[] tabs;
        [SerializeField] private int defaultIndex = 0;
        public int SelectedIndex { get; private set; } = -1;
        public MenuBase CurrentMenu => tabs[SelectedIndex].menu;

        [SerializeField] private bool useKeysToChange = false;
        [SerializeField] private bool ignoreMenuNavigation = false;
        private MenuNavigation menuNavigation;

        [System.Serializable]
        public struct Tab
        {
            public MenuBase menu;
            public Button button;
            public UIAnimator tabAnim;
        }

        private void Update()
        {
            if (useKeysToChange)
            {
                bool ctrl = Input.GetKey(KeyCode.LeftControl);
                bool shift = Input.GetKey(KeyCode.LeftShift);
                if (Input.GetKeyDown(KeyCode.Tab)) { Previous(); }
                if (Input.GetKeyDown(KeyCode.Tab)) { Next(); }
            }
        }

        private void Previous()
        {
            if (SelectedIndex - 1 >= 0) SelectMenu(SelectedIndex - 1);
        }

        private void Next()
        {
            if (SelectedIndex + 1 < tabs.Length) SelectMenu(SelectedIndex + 1);
        }

        private void SetNavigation(Selectable element)
        {
            for (int i = 0; i < tabs.Length; i++)
            {
                var navigation = tabs[i].button.navigation;
                navigation.selectOnDown = element;
                tabs[i].button.navigation = navigation;
            }
        }

        private void Awake()
        {
            for (int i = 0; i < tabs.Length; i++)
            {
                tabs[i].menu.Hide();
                int index = i;
                tabs[index].button.onClick?.AddListener(() => { SelectMenu(index); });
                tabs[index].menu.onShow?.AddListener(() =>
                {
                    if (!useKeysToChange)
                    {
                        SetNavigation(tabs[index].menu.defaultUIElement);
                    }
                    tabs[index].tabAnim.Show();
                });
                tabs[index].menu.onHide?.AddListener(tabs[index].tabAnim.Hide);
            }
        }

        public void Start()
        {
            menuNavigation = MenuNavigation.Instance;
            EventSystem.current.SetSelectedGameObject(null);
            SelectMenu(defaultIndex);
        }

        public void SelectMenu(int index)
        {
            if (!enabled) return;
            if (index == SelectedIndex)
            {
                tabs[SelectedIndex].menu.defaultUIElement?.Select();
                return;
            }

            if (index >= 0 && index < tabs.Length)
            {
                if (menuNavigation == null || ignoreMenuNavigation)
                {
                    if (SelectedIndex >= 0) tabs[SelectedIndex].menu.Hide();
                    SelectedIndex = index;
                    tabs[SelectedIndex].menu.Show();
                }
                else
                {
                    SelectedIndex = index;
                    menuNavigation.ShowMenu(tabs[SelectedIndex].menu);
                }
            }
        }
    }
}
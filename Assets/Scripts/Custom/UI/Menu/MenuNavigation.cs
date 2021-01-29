using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Utilities.UI.Menu
{
    public class MenuNavigation : MonoBehaviour
    {
        public MenuNavigationInfo GraphData;
        public static MenuNavigation Instance { get; private set; }

        [SerializeField] private List<MenuBase> allMenus = new List<MenuBase>();

        private Dictionary<string, MenuBase> _db = null;

        public Dictionary<string, MenuBase> MenusDB
        {
            get
            {
                if (_db == null)
                {
                    _db = new Dictionary<string, MenuBase>();
                    allMenus.ForEach(el => { _db.Add(el.menuName, el); });
                }
                return _db;
            }
        }

        public MenuBase CurrentParent
        {
            get
            {
                if (!string.IsNullOrEmpty(currentNode.parentId))
                    return MenusDB[currentNode.parentId];
                return null;
            }
        }

        public MenuBase currentMenu;
        public MenuNavigationInfo.Node currentNode;
        public Action<MenuBase, MenuBase> OnChangeMenu { get; set; }
        public Action OnBack { get; set; }

        protected virtual void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(this);
            }
        }

        public void ShowMenu(MenuBase menu)
        {
            var path = GraphData.GetPath(currentMenu, menu);
            if (path == null) return;
            var old = currentMenu;
            currentMenu = menu;
            currentNode = GraphData.GetNode(menu);
            foreach (var menuNode in path)
            {
                if (string.IsNullOrEmpty(menuNode.menuId)) continue;
                if (menuNode.menuId == menu.menuId) { MenusDB[menuNode.menuId].Show(); }
                else { MenusDB[menuNode.menuId].Hide(); }
            }

            OnChangeMenu?.Invoke(old, currentMenu);
        }

        public virtual void ShowPrevious()
        {
            if (string.IsNullOrEmpty(currentNode.parentId)) return;
            ShowMenu(MenusDB[currentNode.parentId]);
            OnBack?.Invoke();
        }
    }
}
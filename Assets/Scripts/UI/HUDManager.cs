using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities.UI;
using Utilities.UI.Animation;

public class HUDManager : MonoBehaviour
{
    public List<InteractMenuHUD> menus;
    private HashSet<InteractMenuHUD> activeMenus = new HashSet<InteractMenuHUD>();

    public bool HasOpenMenus => activeMenus.Count > 0;

    public InteractMenuHUD CurrentMenu { get; private set; }

    private void Awake()
    {
        menus.ForEach(menu =>
        {
            menu.animator.OnStartAnimation += (val) =>
            {
                if (val) { activeMenus.Add(menu); CurrentMenu = menu; }
                else if (activeMenus.Contains(menu))
                {
                    activeMenus.Remove(menu);
                    CurrentMenu = null;
                }
            };
        });
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipeMenuUI : InteractMenuHUD
{
    public List<RecipeSlotUI> Slots = new List<RecipeSlotUI>();
    public RecipeTooltip tooltip;

    private void Awake()
    {
        Slots.ForEach(el => el.Setup(this));
    }

    public override void Close()
    {
        base.Close();
        tooltip.Hide();
    }
}
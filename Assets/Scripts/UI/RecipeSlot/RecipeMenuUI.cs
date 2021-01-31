using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities.Audio;

public class RecipeMenuUI : InteractMenuHUD
{
    public CraftingRecipeList Recipes;
    public List<TierUI> Slots = new List<TierUI>();
    public RecipeTooltip tooltip;

    [Header("Audio")]
    public ClipInfo openSFX;

    public ClipInfo closeSFX;

    [System.Serializable]
    public class TierUI
    {
        public List<RecipeSlotUI> slots = new List<RecipeSlotUI>();
        public int tier;
    }

    private void Start()
    {
        int total = 0;
        for (int i = 0; i < Slots.Count; i++)
        {
            int j = 0;
            Slots[i].slots.ForEach(slot =>
            {
                slot.Setup(this, Recipes.TotalIngredients[total]);
                j++;
                total++;
            });
        }
    }

    public override void Open()
    {
        if (!IsOpen) { AudioMngr.Player(AudioMngr.Type.SFX).Play(openSFX); }
        base.Open();
    }

    public override void Close()
    {
        if (IsOpen) { AudioMngr.Player(AudioMngr.Type.SFX).Play(closeSFX); }
        base.Close();
        tooltip.Hide();
    }
}
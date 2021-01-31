using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Utilities.Audio;

public class CraftingUI : InteractMenuHUD
{
    public CraftingRecipeList crafting;
    public PlayerInventory inventory;
    public CraftingItemSlot slot1;
    public CraftingItemSlot slot2;
    public CraftingCollectSlot collectSlot;
    public TimedMeter meter;
    public Button button;

    [Header("Audio")] public ClipInfo openSFX;
    public ClipInfo validCraftSFX;
    public ClipInfo invalidCraftSFX;
    public ClipInfo trashItemSFX;
    public ClipInfo meterSFX;
    public ClipInfo collectSFX;

    private void Awake()
    {
        button.onClick.AddListener(() =>
        {
            if (!IsCrafting) return;
            meter.StopWithAction(Craft, Invalid);
        });

        Clear();
    }

    public void Invalid()
    {
        AudioMngr.Player(AudioMngr.Type.SFX).Play(invalidCraftSFX);

        if (source != null) { AudioMngr.Player(AudioMngr.Type.SFX).FreeSource(source, source.playId); source = null; }

        Clear();
        inventory.TestLost();
        CancelCraft();
    }

    private void OnDestroy()
    {
        if (source != null) { AudioMngr.Player(AudioMngr.Type.SFX).FreeSource(source, source.playId); source = null; }
    }

    public void Collect()
    {
        if (collectSlot.ingredient == null) return;
        inventory.AddItem(collectSlot.ingredient);
        collectSlot.Clear();
        AudioMngr.Player(AudioMngr.Type.SFX).Play(collectSFX);
        inventory.TestLost();
    }

    public void RemoveItemFromSlot(CraftingItemSlot slot)
    {
        if (slot.ingredient == null) return;
        inventory.AddItem(slot.ingredient);
        slot.Clear();
        CancelCraft();
    }

    public bool AddIngredient(GameIngredientInfo ingredient)
    {
        if (collectSlot.ingredient != null) return false;
        if (slot1.ingredient == null)
        {
            slot1.AddItem(ingredient);
            inventory.RemoveItem(ingredient);
            StartCraft();

            return true;
        }
        if (slot2.ingredient == null && slot1.ingredient != ingredient)
        {
            slot2.AddItem(ingredient);
            inventory.RemoveItem(ingredient);
            StartCraft();
            return true;
        }

        return false;
    }

    public bool IsCrafting = false;

    private AudioSourceInfo source;

    public void StartCraft()
    {
        if (slot1.ingredient == null || slot2.ingredient == null) return;
        var tier1 = crafting.GetIngredient(slot1.ingredient).tier;
        var tier2 = crafting.GetIngredient(slot2.ingredient).tier;
        var tier = Mathf.Max(tier1, tier2);
        IsCrafting = true;
        meter.Move(0.6f + tier * 0.2f);
        if (source != null) { AudioMngr.Player(AudioMngr.Type.SFX).FreeSource(source, source.playId); source = null; }
        source = AudioMngr.Player(AudioMngr.Type.SFX).Play(meterSFX);
    }

    public void CancelCraft()
    {
        if (source != null) { AudioMngr.Player(AudioMngr.Type.SFX).FreeSource(source, source.playId); source = null; }
        IsCrafting = false;
        meter.Stop();
    }

    public void Craft()
    {
        if (source != null) { AudioMngr.Player(AudioMngr.Type.SFX).FreeSource(source, source.playId); }
        if (slot1.ingredient == null || slot2.ingredient == null) return;
        var result = crafting.CheckIfRecipeExists(slot1.ingredient, slot2.ingredient);
        collectSlot.AddItem(result);

        if (result)
        {
            AudioMngr.Player(AudioMngr.Type.SFX).Play(validCraftSFX);
        }
        else
        {
            AudioMngr.Player(AudioMngr.Type.SFX).Play(trashItemSFX);
        }
        //OnCraft?.Invoke(result);
        slot1.Clear();
        slot2.Clear();
        inventory.TestLost();
    }

    public void Clear()
    {
        slot1.Clear();
        slot2.Clear();
        collectSlot.Clear();
    }

    public override void Open()
    {
        AudioMngr.Player(AudioMngr.Type.SFX).Play(openSFX);
        base.Open();
    }

    public override void Close()
    {
        if (IsOpen)
        {
            if (collectSlot.ingredient != null) Collect();
            else { RemoveItemFromSlot(slot1); RemoveItemFromSlot(slot2); }
        }
        base.Close();
    }
}
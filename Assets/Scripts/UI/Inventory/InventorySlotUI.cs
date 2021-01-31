using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Utilities.Audio;

public class InventorySlotUI : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public Image icon;
    public TMPro.TextMeshProUGUI text;
    public int amount;
    public GameIngredientInfo ingredient;
    public RectTransform rt;

    public InventoryUI inventory;

    private void Awake()
    {
        rt = GetComponent<RectTransform>();
    }

    public void Set(GameIngredientInfo ingredient, int amount)
    {
        this.ingredient = ingredient;
        icon.sprite = ingredient.IngredientSprite;
        UpdateAmount(amount);
    }

    public void UpdateAmount(int newAmount)
    {
        text.text = newAmount.ToString();
    }

    public void Clear()
    {
        ingredient = null;
        text.text = "";
        TooltipManager.Instance.Hide();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!inventory || inventory.hud.CurrentMenu == null) return;

        if (inventory.hud.CurrentMenu is HarvestSpotUI)
        {
            var ui = inventory.hud.CurrentMenu as HarvestSpotUI;
            if (ui.farmSpot.PlantedIngredient == null)
            {
                ui.farmSpot.Plant(ingredient);
                AudioMngr.Player(AudioMngr.Type.SFX).Play(inventory.plantSFX);
            }
        }

        if (inventory.hud.CurrentMenu is CraftingUI)
        {
            var ui = inventory.hud.CurrentMenu as CraftingUI;
            var val = ui.AddIngredient(ingredient);
            if (val) AudioMngr.Player(AudioMngr.Type.SFX).Play(inventory.craftSFX);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (inventory.hud.CurrentMenu is HarvestSpotUI)
        {
            TooltipManager.Instance.Show(rt.position, "Plant", -1);
        }
        else if (inventory.hud.CurrentMenu is CraftingUI)
        {
            TooltipManager.Instance.Show(rt.position, "Craft", -1);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        TooltipManager.Instance.Hide();
    }
}
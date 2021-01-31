using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CraftingItemSlot : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public CraftingUI ui;
    public Image icon;
    public GameIngredientInfo ingredient;
    public RectTransform rt;

    void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
    {
        ui.RemoveItemFromSlot(this);
    }

    public void AddItem(GameIngredientInfo ingredient)
    {
        if (ingredient == null) return;
        this.ingredient = ingredient;
        icon.sprite = ingredient.IngredientSprite;
        icon.gameObject.SetActive(true);
    }

    public void Clear()
    {
        icon.gameObject.SetActive(false);
        ingredient = null;
        TooltipManager.Instance.Hide();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (ingredient != null) TooltipManager.Instance.Show(rt.position, "Collect");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        TooltipManager.Instance.Hide();
    }
}
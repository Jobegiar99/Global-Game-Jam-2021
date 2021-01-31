using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CraftingCollectSlot : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public CraftingUI ui;
    public Image image;
    public GameIngredientInfo ingredient;
    public RectTransform rt;

    public void AddItem(GameIngredientInfo ingredient)
    {
        if (ingredient == null) return;

        this.ingredient = ingredient;
        image.sprite = ingredient.IngredientSprite;
        image.gameObject.SetActive(true);
    }

    public void Clear()
    {
        image.gameObject.SetActive(false);
        ingredient = null;
        TooltipManager.Instance.Hide();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (ingredient == null) return;
        ui.Collect();
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
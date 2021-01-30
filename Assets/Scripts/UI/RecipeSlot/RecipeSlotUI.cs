using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Utilities.UI;
using Utilities.UI.Animation;

public class RecipeSlotUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public RectTransform rt;
    public RecipeMenuUI MenuUI { get; private set; }

    public void Setup(RecipeMenuUI menu)
    {
        MenuUI = menu;
        if (!rt) rt = GetComponent<RectTransform>();
    }

    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
        MenuUI.tooltip.Show(this);
    }

    void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
    {
        MenuUI.tooltip.Hide();
    }
}
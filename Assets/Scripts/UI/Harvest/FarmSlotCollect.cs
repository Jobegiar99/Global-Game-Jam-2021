using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class FarmSlotCollect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public HarvestSpotUI ui;
    public Image image;
    public TMPro.TextMeshProUGUI text;
    public Button button;
    public RectTransform rt;
    private GameIngredientInfo ingredient;

    private void Awake()
    {
        button.onClick.AddListener(() =>
        {
            if (ingredient)
            {
                ui.farmSpot.Collect();
                Clear();
            }
        });
    }

    public void Grow(GameIngredientInfo ingredient, int amount)
    {
        image.gameObject.SetActive(true);
        this.ingredient = ingredient;
        image.sprite = ingredient.IngredientSprite;
        text.text = amount.ToString();
    }

    public void Clear()
    {
        image.gameObject.SetActive(false);
        this.ingredient = null;
        text.text = "";
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
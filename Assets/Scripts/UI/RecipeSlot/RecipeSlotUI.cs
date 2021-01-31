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
    public Image image;
    public RecipeMenuUI MenuUI { get; private set; }
    public CraftingRecipe recipe;
    public GameIngredientInfo ingredient;
    public int tier = 0;

    private bool isDiscovered = false;

    public void Setup(RecipeMenuUI menu, GameIngredient ingredient = null)
    {
        MenuUI = menu;
        this.ingredient = ingredient.Info;
        recipe = menu.Recipes.FindRecipe(ingredient);
        if (!rt) rt = GetComponent<RectTransform>();
        image = transform.GetChild(0).GetComponent<Image>();
        image.preserveAspect = true;
        if (recipe != null) MenuUI.Recipes.OnDiscover += (ing) => { if (ing == ingredient.Info) OnDiscover(); };
        else OnDiscover();
    }

    public void OnDiscover()
    {
        image.color = Color.white; image.sprite = ingredient.IngredientSprite;
        isDiscovered = true;
    }

    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
        if (!isDiscovered || recipe == null) return;
        MenuUI.tooltip.Show(this);
    }

    void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
    {
        if (!isDiscovered || recipe == null) return;
        MenuUI.tooltip.Hide();
    }
}
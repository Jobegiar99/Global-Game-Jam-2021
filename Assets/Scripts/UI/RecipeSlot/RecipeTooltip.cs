using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Utilities.UI;
using Utilities.UI.Animation;

public class RecipeTooltip : MonoBehaviour
{
    public Image ingredient1Icon;
    public Image ingredient2Icon;
    public UIAnimator anim;
    public UIScreenConnector connector;

    public void Show(RecipeSlotUI slot)
    {
        if (slot.recipe == null) return;
        connector.Position = slot.rt.position;
        ingredient1Icon.sprite = slot.recipe.FirstIngredient.Info.IngredientSprite;
        ingredient2Icon.sprite = slot.recipe.SecondIngredient.Info.IngredientSprite;
        anim.Show();
    }

    public void Hide()
    {
        anim.Hide();
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FarmSlotPlant : MonoBehaviour
{
    public HarvestSpotUI ui;
    public Image image;
    public Utilities.UI.BarUI timeBarUI;
    public Utilities.UI.BarUI arrow;

    private float time = 0f;
    public GameIngredientInfo ingredient;

    public void Plant(GameIngredientInfo ingredient)
    {
        this.ingredient = ingredient;
        image.sprite = ingredient.IngredientSprite;
        time = 0f;
        image.gameObject.SetActive(true);
    }

    private void Update()
    {
        if (ingredient == null) return;
        if (!ui.farmSpot.growing)
        {
            if (ui.farmSpot.PlantedIngredient != null)
            {
                timeBarUI.SetValue(1f, 1f);
                arrow.SetValue(1f, 1f);
            }
            return;
        }

        time += Time.deltaTime;

        timeBarUI.SetValue(time, ingredient.HarvestTime);
        arrow.SetValue(time, ingredient.HarvestTime);
    }

    public void Clear()
    {
        image.gameObject.SetActive(false);
        ingredient = null;
        time = 0f;
        timeBarUI.SetValue(0, 1);
        arrow.SetValue(0, 1);
    }
}
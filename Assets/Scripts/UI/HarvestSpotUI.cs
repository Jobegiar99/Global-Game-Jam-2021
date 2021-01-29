using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities.UI;
using Utilities.UI.Animation;

public class HarvestSpotUI : InteractMenuHUD
{
    public BarUI menuTimeIndicator;
    public FarmSpot farmSpot;

    public BarUI barTimeIndicator;
    public UIAnimator barAnim;

    public override void Open()
    {
        base.Open();
        barAnim.Hide();
    }

    public override void Close()
    {
        base.Close();
        barAnim.Show();
        //if (farmSpot.PlantedIngredient != null) { barAnim.Show(); }
    }
}
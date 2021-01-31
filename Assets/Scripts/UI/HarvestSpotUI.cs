using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities.UI;
using Utilities.UI.Animation;
using Utilities.Audio;

public class HarvestSpotUI : InteractMenuHUD
{
    public FarmSpot farmSpot;

    public FarmSlotCollect collect;
    public FarmSlotPlant plant;

    public ClipInfo openSFX;
    public ClipInfo collectSFX;

    private void Start()
    {
        plant.ui = collect.ui = this;
        farmSpot.OnPlant += (ingredient) =>
        {
            plant.Plant(ingredient);
        };

        farmSpot.OnGrowFinish += (ingredient, amount) =>
        {
            collect.Grow(ingredient, amount);
        };

        farmSpot.OnCollect += (ingredient, amount) =>
        {
            AudioMngr.Player(AudioMngr.Type.SFX).Play(collectSFX);
            plant.Clear();
            collect.Clear();
        };
        plant.Clear();
        collect.Clear();
    }

    public void Plant()
    {
    }

    public override void Open()
    {
        AudioMngr.Player(AudioMngr.Type.SFX).Play(openSFX);
        base.Open();
        //      barAnim.Hide();
    }

    public override void Close()
    {
        base.Close();
        farmSpot.Collect();
        //        barAnim.Show();
        //if (farmSpot.PlantedIngredient != null) { barAnim.Show(); }
    }
}
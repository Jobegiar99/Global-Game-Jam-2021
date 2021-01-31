using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities.UI.Animation;

public class Pause : MonoBehaviour
{
    public UIAnimator anim;
    public PlayerController player;

    public UIAnimator lostScreen;
    public UIAnimator wonScreen;

    public void Update()
    {
        if (lostScreen.Showing || wonScreen.Showing)
        {
            if (anim.Showing) { anim.Hide(); player.Stop(); }
            return;
        }
        if (Input.GetKeyDown(KeyCode.Escape))
            if (anim.Showing) { anim.Hide(); player.Init(); }
            else { anim.Show(); player.Stop(); }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities.UI;

namespace Gote.HUD
{
    public class PointsUIHelper : MonoBehaviour
    {
        [Header("Ether")] public Sprite etherIcon;
        public Color etherColor;
        [Header("Health")] public Sprite healthIcon;
        public Color healthColor;

        public static void Ether(float value, Vector3 worldPos)
        {
            var point = new PointsUIInfo()
            {
                image = PointsMngrUI.Instance.helper.etherIcon,
                speed = 0.75f,
                text = $"+{value}",
                color = PointsMngrUI.Instance.helper.etherColor,
                applyColorToImg = PointsMngrUI.Instance.helper.etherIcon
            };

            PointsMngrUI.Instance.Show(point, worldPos);
        }

        public static void Health(float value, Vector3 worldPos, string sign = "-")
        {
            var point = new PointsUIInfo()
            {
                size = 0.75f,
                image = PointsMngrUI.Instance.helper.healthIcon,
                speed = 1f,
                text = $"{sign}{value}",
                color = PointsMngrUI.Instance.helper.healthColor,
                applyColorToImg = PointsMngrUI.Instance.helper.etherIcon
            };

            PointsMngrUI.Instance.Show(point, worldPos);
        }
    }
}
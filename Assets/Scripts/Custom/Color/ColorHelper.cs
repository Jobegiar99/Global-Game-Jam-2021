using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ColorHelper
{
    public static Color ChangeColorBrightness(this Color color, float correctionFactor)
    {
        float red = color.r;
        float green = color.g;
        float blue = color.b;

        if (correctionFactor < 0)
        {
            correctionFactor = 1 + correctionFactor;
            red *= correctionFactor;
            green *= correctionFactor;
            blue *= correctionFactor;
        }
        else
        {
            red = (1 - red) * correctionFactor + red;
            green = (1 - green) * correctionFactor + green;
            blue = (1 - blue) * correctionFactor + blue;
        }
        color = new Color(red, green, blue, color.a);

        return color;
    }

    public static Color ColorFromHex(string hex)
    {
        if (!hex.StartsWith("#")) hex = "#" + hex;
        Color color;
        if (ColorUtility.TryParseHtmlString(hex, out color))
        {
            return color;
        }
        return Color.clear;
    }

    public static Color ApplyTint(this Color original, Color tint, float percentage = 0.25f)
    {
        Color color = original;
        color += tint * percentage;

        return color;
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ColorPalette", menuName = "Utilities/Color/ColorPalette")]
public class ColorPalette : ScriptableObject
{
    public Color mainTone1;
    public Color mainTone2;
    public Color secondaryTone1;
    public Color secondaryTone2;
    public Color textColor;
    public Color textColor2;
}
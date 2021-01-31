using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CraftingRecipe
{
    public GameIngredient FirstIngredient;
    public GameIngredient SecondIngredient;
    public GameIngredient Result;
}
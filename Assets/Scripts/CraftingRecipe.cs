using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/CraftingRecipe", order = 1)]
public class CraftingRecipe : ScriptableObject
{
    public GameIngredient FirstIngredient;
    public GameIngredient SecondIngredient;
    public GameIngredient Result;
    public int Tier;
}
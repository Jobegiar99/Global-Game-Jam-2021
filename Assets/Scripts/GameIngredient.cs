using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/GameIngredient", order = 1)]
public class GameIngredient : ScriptableObject
{
    public string Name;
    public string Description;
    public int HarvestTime;
    public int HarvestAmount;
    public int Tier;
    public Sprite IngredientSprite;
}
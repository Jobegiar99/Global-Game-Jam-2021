using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/GameIngredient", order = 1)]
public class GameIngredientInfo : ScriptableObject
{
    public string Name;
    public string Description;
    public int HarvestTime;
    public int HarvestAmount;
    public Sprite IngredientSprite;
}

[System.Serializable]
public class GameIngredient
{
    public GameIngredientInfo Info;
    public int tier = 0;

    public GameIngredient(GameIngredientInfo info, int tier = 0)
    {
        Info = info;
        this.tier = tier;
    }
}
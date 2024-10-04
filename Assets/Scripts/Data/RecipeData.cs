using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RecipeData", menuName = "ScriptableObjects/RecipeData", order = 1)]
public class RecipeData : ScriptableObject
{
    public List<ItemData> ingredients;
}
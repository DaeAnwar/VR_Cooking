using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "RecipeContainer", menuName = "RecipeContainer")]
public class RecipeContainer : ScriptableObject
{
    public List<RecipeData> recipes = new List<RecipeData>();
}
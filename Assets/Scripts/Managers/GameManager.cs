using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public RecipeContainer recipeContainer;
    public RectTransform recipeHolder;
    public RecipeController recipePrefab;

    public bool initiallizeAutomatically; 


    void Start()
    { 

        if (initiallizeAutomatically)
             InstantiateRecipes();

    }

    void InstantiateRecipes()
    {
        if (recipeContainer == null || recipeHolder == null || recipePrefab == null)
        {
            Debug.LogError("RecipeContainer, recipeHolder, or recipePrefab is not assigned in the GameManager.");
            return;
        }

        foreach (RecipeData recipeData in recipeContainer.recipes)
        {
            var recipeClone = Instantiate(recipePrefab, recipeHolder);
            

            if (recipeClone != null)
            {
                recipeClone.SetData(recipeData);
            }
            else
            {
                Debug.LogError("RecipeController component not found on the recipe prefab.");
            }
        }
    }

   
}

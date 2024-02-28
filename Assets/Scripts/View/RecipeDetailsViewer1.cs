using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecipeDetailsViewer1 : MonoBehaviour
{
    [SerializeField] Text recipeNameTXT;
    [SerializeField] Text recipeDescriptionTXT;

    [Space]
    [SerializeField] RectTransform ingredientsHolder;
    [SerializeField] IngredController ingredientPrefab;
    private RecipeData itemData;

    [SerializeField] GameObject detailsPanel;
  

    private void OnEnable()
    {
        EventManager.OnRequestDetails += HandleRecipeDetailsView;
    }
    private void OnDisable()
    {
        EventManager.OnRequestDetails -= HandleRecipeDetailsView;
    }

    private void HandleRecipeDetailsView(RecipeData data)
    {
        itemData = data;
        recipeNameTXT.text = itemData.name;
        recipeDescriptionTXT.text = itemData.description;

        foreach (Ingredient ingredient in itemData.ingredients)
        {
            var IngredientClone = Instantiate(ingredientPrefab, ingredientsHolder);

            if (IngredientClone != null)
            {
                IngredientClone.SetIngredient(ingredient);
               
            }
            else
            {
                Debug.LogError("IngredientController component not found on the recipe prefab.");
            }
        }


      detailsPanel.SetActive(true);
    }
    public void ShowRecipeSteps()
    {
        if (itemData != null)
        {
            Debug.Log($"Showing recipe steps for {itemData.name}");
            EventManager.OnRequestSteps?.Invoke(itemData);
        }
        else
        {
            Debug.LogError("itemData is null in ShowRecipeSteps");
        }
    }
    public void CloseDetailsPanel()
    {
        detailsPanel.SetActive(false);
    }
    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class RecipeDetailsViewer : MonoBehaviour
{
    [SerializeField] Text recipeNameTitle;
    [SerializeField] Text recipeDesciText;
    [SerializeField] GameObject recipesPanel;
    [SerializeField] GameObject itemPanel;
    [SerializeField] GameObject BackBtn;

    private void OnEnable()
    {
        EventManager.OnRequestDetails += HandleRecipeDetails;
        
    }
    private void OnDisable()
    {
        EventManager.OnRequestDetails -= HandleRecipeDetails;
    }

    private void HandleRecipeDetails(RecipeData data)
    {
        recipeNameTitle.text = data.name;
        recipeDesciText.text = data.description;

        recipesPanel.SetActive(false);
        BackBtn.SetActive(true);
        itemPanel.SetActive(true);

    }

    public void OnBack()
    {
        recipesPanel.SetActive(true);
        BackBtn.SetActive(false);
        itemPanel.SetActive(false);
        recipeNameTitle.text = "Recipes";
    }



}

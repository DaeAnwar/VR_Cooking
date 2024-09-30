using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class RecipeNutiInfo : MonoBehaviour
{
    public RawImage recipeImage;
    [SerializeField] Text recipeName;
    [SerializeField] RectTransform NutiHolder;
    [SerializeField] NutiBox NutiPrefab;
    public RectTransform loadingEffect;
    private void OnEnable()
    {
        EventManager.OnRequestDetails += HandleRecipeCaloriesView;
    }

   

    private void OnDisable()
    {
        EventManager.OnRequestDetails -= HandleRecipeCaloriesView;
    }

    private void HandleRecipeCaloriesView(RecipeData itemData)
    {

        recipeName.text = itemData.name;
       // recipeImage = itemData.image;
        EnableLoadingEffect();

        DownloadHandler.LoadRecipeImage(itemData.name, itemData.image, recipeImage, () => DisableLoadingEffect());

        foreach (Nutrients nuti in itemData.nutrients)
        {
            var NutiClone = Instantiate(NutiPrefab, NutiHolder);

            if (NutiClone != null)
            {
                NutiClone.SetNutBox(nuti);

            }
            else
            {
                Debug.LogError("IngredientController component not found on the recipe prefab.");
            }
        }





    }
    private void EnableLoadingEffect()
    {
        loadingEffect.gameObject.SetActive(true);
    }
    private void DisableLoadingEffect()
    {
        loadingEffect.gameObject.SetActive(false);
    }

}

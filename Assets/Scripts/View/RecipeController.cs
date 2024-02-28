using UnityEngine;

using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;
using System;

public class RecipeController : MonoBehaviour
{
    public RawImage recipeImage;
    public Text recipeNameText;
    public Text cookingTimeText;
    public Text preparationTimeText;
    [Space]
    public RectTransform loadingEffect;


    private RecipeData itemData;

    public void SetData(RecipeData data)
    {
        itemData = data;
        recipeNameText.text = itemData.name;
        cookingTimeText.text = "Cooking Time: " + itemData.cookTime + " mins";
        preparationTimeText.text = "Preparation Time: " + itemData.prepareTime + " mins";
        //StartCoroutine(LoadRecipeImage(itemData.image));

        EnableLoadingEffect();

        DownloadHandler.LoadRecipeImage(data.name,data.image, recipeImage, () => DisableLoadingEffect() );
    }

    private void EnableLoadingEffect()
    {
        loadingEffect.gameObject.SetActive(true);
    }
    private void DisableLoadingEffect()
    {
        loadingEffect.gameObject.SetActive(false);
    }

    public void ShowRecipeDetaills()
    {
        EventManager.OnRequestDetails?.Invoke(itemData);
    }
   

}


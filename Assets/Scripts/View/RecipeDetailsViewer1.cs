using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceLocations;

public class RecipeDetailsViewer1 : MonoBehaviour
{
    [SerializeField] Transform Target;

    [SerializeField] Text recipeNameTXT;
    [SerializeField] Text recipeDescriptionTXT;

    [Space]
    [SerializeField] RectTransform ingredientsHolder;
    [SerializeField] IngredController ingredientPrefab;
    private RecipeData itemData;
    [Space]
    public GameManager gameManager;
    [SerializeField] GameObject detailsPanel;
    [SerializeField] GameObject infoUIPanel;
    [SerializeField] GameObject infoPanel;
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

        foreach (Ingredient ingredient in itemData.Ingredients)
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
        infoUIPanel.SetActive(true);
    }
    public void ShowRecipeSteps()
    {
        if (itemData != null)
        {
            Debug.Log($"Showing recipe steps for {itemData.name}");
            gameManager.TurnNotificationON($"steps for {itemData.name}", Color.green);
            EventManager.OnRequestSteps?.Invoke(itemData);
            infoPanel.transform.position = new Vector3(7.55f, 1.4f, 1.2f);
            infoPanel.transform.localRotation = Quaternion.Euler(new Vector3(0f, 90f, 0f));
            GameManager.Instance.MoveToTarget(Target, 3);
            //InstantiateRecipePrefabs(); 
        }
        else
        {
            Debug.LogError("itemData is null in ShowRecipeSteps");
        }
    }
    public void CloseDetailsPanel()
    {
        detailsPanel.SetActive(false);
        infoUIPanel.SetActive(false);

    }


    /* public void InstantiateRecipePrefabs()
     {
         Addressables.LoadResourceLocationsAsync("Recipe"+itemData.id).Completed += (AsyncOperationHandle<IList<IResourceLocation>> handle) =>
         {
             if (handle.Status == AsyncOperationStatus.Succeeded)
             {
                 foreach (var location in handle.Result)
                 {
                     Addressables.LoadAssetAsync<GameObject>(location).Completed += (AsyncOperationHandle<GameObject> prefabHandle) =>
                     {
                         if (prefabHandle.Status == AsyncOperationStatus.Succeeded)
                         {
                             Instantiate(prefabHandle.Result);
                         }
                         else
                         {
                             Debug.Log("Error loading prefab: " + prefabHandle.OperationException);
                         }
                     };
                 }
             }
             else
             {
                 Debug.Log("Error loading addressables: " + handle.OperationException);
             }
         };
     }*/

    /* private void OnAddressableLabelInstantiated(AsyncOperationHandle<GameObject> obj)
     {
         if (obj.Status == AsyncOperationStatus.Succeeded)
         {
             Debug.Log("Recipe"+itemData.id+" Prefabs instantiated successfully!");
         }
         else
         {
             // Handle the instantiation error
             Debug.Log("Error instantiating Label");
         }


     }*/
}

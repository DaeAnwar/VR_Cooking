using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;


public class KitchenViewr : MonoBehaviour
{
    [SerializeField] GameObject RecipesPanel;
    [SerializeField] GameObject InfoUIPanel;

    public GameManager gameManager;



    private void OnEnable()
    {
        EventManager.OnRequestKitchen += SettingAdrresableKitchenAndRecipeData;
    }
    private void OnDisable()
    {
        EventManager.OnRequestKitchen -= SettingAdrresableKitchenAndRecipeData;
    }



     private void SettingAdrresableKitchenAndRecipeData(AssetReferenceGameObject kitchen)
    {

        kitchen.InstantiateAsync().Completed += OnAddressableKitchenInstantiated;
       

        RecipesPanel.SetActive(true);
        InfoUIPanel.SetActive(true);

        gameManager.SetRecipeData(); 
    }
   
    void OnAddressableKitchenInstantiated(AsyncOperationHandle<GameObject> handle)
    {
        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            Debug.Log("Kitchen instantiated successfully!");
            gameManager.TurnNotificationON("you choosed the right Kitchen!", Color.green);

        }
        else
        {
            // Handle the instantiation error
            Debug.Log("Error instantiating Kitchen");
        }
    }

}

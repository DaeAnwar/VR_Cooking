using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.Events;

public class LowCarbApi : MonoBehaviour
{
    public RecipeContainer recipeContainer; 

    private string apiUrl = "https://low-carb-recipes.p.rapidapi.com/search";
    private string apiKey = "929adc9c1cmshf5be5dd26b34ce4p1bb6b0jsn5ffec8fd5ed3";
    private string rapidApiHost = "low-carb-recipes.p.rapidapi.com";

    public UnityEvent OnRecieveData;

    void Awake()
    {
        StartCoroutine(GetDataFromApi());
    }

    IEnumerator GetDataFromApi()
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(apiUrl))
        {
            webRequest.SetRequestHeader("X-RapidAPI-Key", apiKey);
            webRequest.SetRequestHeader("X-RapidAPI-Host", rapidApiHost);

            yield return webRequest.SendWebRequest();

            if (webRequest.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Error: " + webRequest.error);
            }
            else
            {
                // Parse JSON data here
                string jsonData = webRequest.downloadHandler.text;
                ParseJsonData(jsonData);
            }
        }
    }

    void ParseJsonData(string jsonData)
    {
        List<RecipeData> myDeserializedClass = JsonConvert.DeserializeObject<List<RecipeData>>(jsonData);

        Debug.Log("Number of recipes in ParseJsonData: " + myDeserializedClass.Count);

        recipeContainer.recipes = myDeserializedClass;

        foreach (RecipeData recipeData in recipeContainer.recipes)
        {
            Debug.Log("Recipe Name: " + recipeData.name);
        }

        OnRecieveData?.Invoke();
    }
}

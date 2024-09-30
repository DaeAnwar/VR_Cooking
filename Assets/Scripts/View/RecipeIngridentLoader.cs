using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceLocations;
using System.Threading.Tasks;

[System.Serializable]
public class InstantiatedIngridents
{

    public string Name;
    public GameObject InstantiatedIngridentclone;
    public int Counting;
    public Vector3 DefaultPosition;



}
public class RecipeIngridentLoader : MonoBehaviour
{
    public GameManager gameManager;
    [SerializeField] RectTransform StepsHolder;
    [SerializeField] StepController StepPrefab;
    [SerializeField] RecipeStepsViewer RecipeStepsView;
    public List<InstantiatedIngridents> InstantiatedIngridents = new List<InstantiatedIngridents>();
    public List<StepController> InstantiatedSteps = new List<StepController>();

    private void OnEnable()
    {
        EventManager.OnRequestIngridents += HandleRecipeingridents;
    }
    private void OnDisable()
    {
        EventManager.OnRequestIngridents -= HandleRecipeingridents;
    }

    private async void HandleRecipeingridents(RecipeData data)
    {

        gameManager.Number_of_steps = data.steps.Count;
        gameManager.Infos = data.Infos; 
            // Loading ingredients and steps
        foreach (Steps step in data.steps)
        {

            if ((step.action.Ingredient!="None"))
            {
                await LoadAssetAsync(step);
            }

            InstantiateStepPrefab(step);
           

            RecipeStepsView.InstantiatedTools[step.action.ToolIndex].listOfThisToolSteps.Add(step);

        }
        gameManager.TurnNotificationON("Tools and ingridents are ready for you CHEF", Color.green);

    }
    public async Task LoadAssetAsync(Steps step)
    {
        bool ingredientFound = false;

        // Check if the ingredient is already instantiated
        foreach (InstantiatedIngridents ingredient in InstantiatedIngridents)
        {
            if (ingredient.Name == step.action.Ingredient)
            {
                ingredientFound = true;
                ingredient.Counting++; // Increment the count
                step.IngredientClone = ingredient.InstantiatedIngridentclone;
                break;
            }
        }

        // If the ingredient is not found, instantiate it
        if (!ingredientFound)
        {
            var handle = await Addressables.InstantiateAsync(step.action.Ingredient).Task;

            if (handle != null)
            {
                // The asset is loaded
                InstantiatedIngridents newIngredient = new InstantiatedIngridents();
                newIngredient.Name = step.action.Ingredient;
                newIngredient.InstantiatedIngridentclone = handle;
                newIngredient.Counting = 1;
                newIngredient.DefaultPosition = handle.transform.position;

                InstantiatedIngridents.Add(newIngredient);
                step.IngredientClone = handle;
                Debug.Log("Loading Ingredient with Await: " + step.IngredientClone);
            }
            else
            {
                // The asset failed to load
                Debug.LogError("Failed to load asset at address: " + step.action.Ingredient);
            }
        }
    }
    private void InstantiateStepPrefab(Steps step)
    {
        var stepClone = Instantiate(StepPrefab, StepsHolder);
        if (stepClone != null)
        {
            stepClone.SetStep(step.StepDescription,step.IngredientsCounting);

            step.StepPrefab = stepClone;
            InstantiatedSteps.Add(stepClone);
        }
        else
        {
            Debug.LogError("StepController component not found on the instantiated prefab.");
        }
    }
}
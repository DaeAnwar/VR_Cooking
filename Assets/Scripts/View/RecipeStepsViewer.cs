
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.AddressableAssets;

using System.Threading.Tasks;

public class RecipeStepsViewer : MonoBehaviour
{
    [SerializeField] GameObject StepsPanel;
    [SerializeField] GameManager gameManager;
    [SerializeField] RecipeIngridentLoader RecipeIngrident;
    public List<ToolInteractionDetector> InstantiatedTools;
    private VfxHandler VfxOutcome;

    public RecipeData itemData;



    private void OnEnable()
    {
        EventManager.OnRequestSteps += HandleRecipeStepView;
    }
    private void OnDisable()
    {
        EventManager.OnRequestSteps -= HandleRecipeStepView;
    }

    public void CloseStepsPanel()
    {
        StepsPanel.SetActive(false);
    }

    private async void HandleRecipeStepView(RecipeData data)
    {

        Debug.Log($"Handling recipe steps view for {data.name}");

        if (data != null)
        {
            itemData = data;
            Debug.Log("Handling recipe steps view");
            Debug.Log($"Number of steps: {data.steps.Count}");

            // Loading Tool
            foreach (Tool tool in data.tools)
            {

               await HandleToolLoadComplete(tool);

                tool.toolDetails.toolClone.listOfThisToolSteps = new List<Steps>();
                InstantiatedTools.Add(tool.toolDetails.toolClone);

            }
            EventManager.OnRequestIngridents?.Invoke(itemData);


        }

        else
        {
            Debug.LogWarning("RecipeData is null.");
        }

        StepsPanel.SetActive(true);
    }


    public async Task HandleToolLoadComplete(Tool toolD)
    {
        var handle = await Addressables.InstantiateAsync(toolD.toolname).Task;

        // Wait for the operation to complete
         
        if (handle!=null)
        {
            Debug.Log("Loaded Tool: " + handle);
            
            ToolInteractionDetector toolPrefab = handle.AddComponent<ToolInteractionDetector>();
            toolPrefab.SetGameManager(gameManager);
            toolPrefab.SetRecipeIngridentLoader(RecipeIngrident);
            

            toolD.toolDetails.toolClone = toolPrefab; 
        }
        else
        {
            Debug.Log("Error loading Tool: " + toolD.toolname);
        }
      
    }

}
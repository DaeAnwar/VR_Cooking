using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipeStepsViewer : MonoBehaviour
{
    [SerializeField] GameObject StepsPanel;
    [SerializeField] RectTransform StepsHolder;
    [SerializeField] StepController StepPrefab;


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

    private void HandleRecipeStepView(RecipeData data)
    {
        Debug.Log($"Handling recipe steps view for {data.name}");
        if (data != null)
        {
            Debug.Log("Handling recipe steps view");
            Debug.Log($"Number of steps: {data.steps.Count}");

            foreach (string step in data.steps)
            {
                var stepClone = Instantiate(StepPrefab, StepsHolder);
                

                if (stepClone != null)
                {
                    stepClone.SetStep(step);
                }
                else
                {
                    Debug.LogError("StepController component not found on the instantiated prefab.");
                }
            }

            StepsPanel.SetActive(true);
        }
        else
        {
            Debug.LogWarning("RecipeData is null.");
        }
    }
}

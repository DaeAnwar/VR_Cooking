using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AddressableAssets;
public class Exit : MonoBehaviour
{
    [SerializeField] GameObject DidUknowPanel;
    [SerializeField] GameObject DiduNut;
    [SerializeField] GameObject StepsPanel;
    [SerializeField] GameObject DetailsPanel;
    [SerializeField] GameObject DidUknowBtn;


    [SerializeField] RecipeIngridentLoader RecipeIngridentLoader;
    [SerializeField] RecipeStepsViewer RecipeStepsViewer ;
    [SerializeField] DidUknow InfoPanel;

    public void No()
    {
        gameObject.SetActive(false);
        DidUknowPanel.SetActive(true);
    }
    public void Yes()
    {
        RealeaseIngridents();
        RealeaseOutcomes();
        RealeaseTools();
        ReturnPanels();
        InfoPanel.ResetPanel();

    }
    
    private void ReturnPanels()
    {
        InfoPanel.gameObject.transform.position = new Vector3(9.03f, 1.39f, 2.8f);
        InfoPanel.gameObject.transform.localRotation = Quaternion.Euler(new Vector3(0f, 60f, 0f));
        
        gameObject.SetActive(false);
        DidUknowPanel.SetActive(false);
        DidUknowBtn.SetActive(false);
        
        DiduNut.SetActive(false);
        StepsPanel.SetActive(false);
        DetailsPanel.SetActive(false);




    }
    private void RealeaseIngridents()
    {
        foreach (InstantiatedIngridents ingredient in RecipeIngridentLoader.InstantiatedIngridents)
        {
            Addressables.ReleaseInstance(ingredient.InstantiatedIngridentclone);
        }

        foreach(StepController stepPrefab in RecipeIngridentLoader.InstantiatedSteps)
        {

            Destroy(stepPrefab.gameObject);
        }
        RecipeIngridentLoader.InstantiatedIngridents = new List<InstantiatedIngridents>();
        RecipeIngridentLoader.InstantiatedSteps = new List<StepController>();
    }
    private void RealeaseOutcomes()
    {
        foreach (InstantiatedIngridents ingredient in RecipeIngridentLoader.InstantiatedIngridents)
        {
            Addressables.ReleaseInstance(ingredient.InstantiatedIngridentclone);
        }
        GameManager.Instance.InstatiatedOutcomes = new List<InstatietedOutcome>();
        GameManager.Instance.Infos = new List<Infos>();
        GameManager.Instance.Number_of_steps = 0;
        GameManager.Instance.CheckedSteps = 0;


    }
    private void RealeaseTools()
    {
        foreach (ToolInteractionDetector tool in RecipeStepsViewer.InstantiatedTools)
        {
            Addressables.ReleaseInstance(tool.gameObject);

        }

        
        RecipeStepsViewer.InstantiatedTools = new List<ToolInteractionDetector>();

    }
}

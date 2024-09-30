using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AddressableAssets;
public class DidUknow : MonoBehaviour
{
    [SerializeField] Text InfoText;
    int index ;
    public List<Infos> Informations = new List<Infos>();
    [SerializeField] GameObject NextButn;
    [SerializeField] GameObject PrevBtn;
    [SerializeField] GameObject ExitPanel;
    [SerializeField] GameObject ExitBtn;
    [SerializeField] GameObject NutiPanel;
    [SerializeField] GameObject DidukonwButton;
    [SerializeField] GameObject DashboardBtn;
    [SerializeField] GameObject DidUKnowPanel;
    [SerializeField] RecipeIngridentLoader RecipeIngridentLoader;
    [SerializeField] RecipeStepsViewer RecipeStepsViewer;
    [SerializeField] GameObject StepsPanel;
    [SerializeField] GameObject DetailsPanel;
    [SerializeField] Transform Target;

    /* public void EnableUI()
     {

     }*/


    public void HandleRecipeInfosView(List<Infos> Infos)
    {
        DidUKnowPanel.SetActive(true);

        index = 0;
        Informations = Infos;
        Debug.Log(Informations);
        if (Informations != null) { 
        SetInfo(Informations[index].information);
        }
        //SetInfo("Wassup I work");
    }








    private void SetInfo(String inform)
    {
        Debug.Log($"Did you know text: {inform}");

        
        if(InfoText == null)
        {
            Debug.LogError("text is missing", gameObject);
            return;
        }
        else
        {
            InfoText.text = inform;
        }
    }
    public void Exit()
    {
        ExitPanel.SetActive(true);
        DidUKnowPanel.SetActive(false);
        NutiPanel.SetActive(false);
        DashboardBtn.SetActive(true);

    }
    public void NextBtn()
    {
        index++;
        SetInfo(Informations[index].information);
        PrevBtn.SetActive(true);
        if (index == Informations.Count -1)
        {
            NextButn.SetActive(false);
            ExitBtn.SetActive(true);
            DashboardBtn.SetActive(true);

        }

    }
    public void PreviousBtn()
    {
        if(index == Informations.Count - 1)
        {
            NextButn.SetActive(true);
            ExitBtn.SetActive(false);
            DashboardBtn.SetActive(false);
        }
        index--;
        SetInfo(Informations[index].information);
        if (index == 0)
        {                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                      
            PrevBtn.SetActive(false);
        }


    }
    public void DidUKnowBtn()
    {
        index = 0;
        DidUKnowPanel.SetActive(true);
        SetInfo(Informations[index].information);
        DashboardBtn.SetActive(false);
        ExitBtn.SetActive(false);
        PrevBtn.SetActive(false);
        NextButn.SetActive(true);
    }
    public void backtoNut()
    {
        DidukonwButton.SetActive(true);
        DidUKnowPanel.SetActive(false);
        NutiPanel.SetActive(true);
        
    }

    /*******************************************************/
    public void No()
    {
        ExitPanel.SetActive(false);
        DidUKnowPanel.SetActive(true);
    }
    public void Yes()
    {
        RealeaseIngridents();
        RealeaseOutcomes();
        RealeaseTools();
        ReturnPanels();
        ResetPanel();
        GameManager.Instance.SetYouPlayedIt();
        GameManager.Instance.MoveToTarget(Target, 13);

    }

    public void ResetPanel()
    {
        index = 0;
        Informations = null;
        DashboardBtn.SetActive(false);
        ExitBtn.SetActive(false);
        PrevBtn.SetActive(false);
        NextButn.SetActive(true);
    } 

    private void ReturnPanels()
    {
        gameObject.transform.position = new Vector3(9.756f, 1.39f, 2.8f);
        gameObject.transform.localRotation = Quaternion.Euler(new Vector3(0f, 60f, 0f));

        ExitPanel.SetActive(false);
        DidUKnowPanel.SetActive(false);
        DidukonwButton.SetActive(false);

        NutiPanel.SetActive(false);
        StepsPanel.SetActive(false);
        DetailsPanel.SetActive(false);




    }
    private void RealeaseIngridents()
    {
        foreach (InstantiatedIngridents ingredient in RecipeIngridentLoader.InstantiatedIngridents)
        {
            Addressables.ReleaseInstance(ingredient.InstantiatedIngridentclone);
        }

        foreach (StepController stepPrefab in RecipeIngridentLoader.InstantiatedSteps)
        {

            Destroy(stepPrefab.gameObject);
        }
        RecipeIngridentLoader.InstantiatedIngridents = new List<InstantiatedIngridents>();
        RecipeIngridentLoader.InstantiatedSteps = new List<StepController>();
    }
    private void RealeaseOutcomes()
    {
        foreach (InstatietedOutcome InstatietedOutcome in GameManager.Instance.InstatiatedOutcomes)
        {
            //Addressables.ReleaseInstance(InstatietedOutcome.OutcomeClone);
            Destroy(InstatietedOutcome.OutcomeClone);
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

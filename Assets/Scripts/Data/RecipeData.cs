using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

[System.Serializable]
public class Ingredient
{

    public string Name;
    



}
[System.Serializable]
public class Nutrients
{
    
    public string Name;
    public double Value;



}
[System.Serializable]
public class Infos
{

    public string information;



}

[System.Serializable]
public class RecipeData
{

    public string id;

    [TextArea(1, 2)]

    public string name;


    public List<string> tags;
    [TextArea(2, 10)]

    public string description;


    public double prepareTime;


    public double cookTime;


    public List<Ingredient> Ingredients;




    public List<Steps> steps;

     public List<Tool> tools;

    public List<Nutrients> nutrients;

    public List<Infos> Infos;
    public string image;
}

[System.Serializable]
public class Steps
{
    public string StepDescription;
    public StepAction action;

    public GameObject IngredientClone;
     [HideInInspector] public StepController StepPrefab;
    public int IngredientsCounting;
   
    public float timer;  

    public string Outcome; 
    
    
}
[System.Serializable]
public class Tool
{
    [SerializeField] public string toolname;
    public ToolDetails toolDetails;  
    
}
[System.Serializable]
public class ToolDetails
{

    [SerializeField] public ToolInteractionDetector toolClone;
    [SerializeField] public int ToolId;
    

}

[System.Serializable]
public class StepAction
{
    public string Ingredient;
    public int ToolIndex;
    public string IngredientOutcome; 
     

}

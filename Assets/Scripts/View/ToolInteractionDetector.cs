using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.XR.Interaction.Toolkit;

public class ToolInteractionDetector : MonoBehaviour,IToolAnimation
{
    public Arrow arrow;
    public delegate void CountdownCompletedHandler();
    // Define a member variable of the delegate type
    public CountdownCompletedHandler CountdownCompleted;

    public GameManager _gameManager;
    private RecipeIngridentLoader _recipeIngridentLoader;
    [Header("Step Settings")]
    public int currentStepIndex = 0;
    public List<Steps> listOfThisToolSteps;
    public Steps currentStep;
    private int CountingIngridentIndex = 0;
    private List<GameObject> usedIngredients = new List<GameObject>();
    private Vector3 ToolTransform;
    public Animator animator;
    private string OutcomeCurrent;
    public TimerHandler Timer;
    public VfxHandler VfxOutcome;
    private GrabingChecking GrabCheckingIngrident;
    

    private void Start()
    {
        if (_gameManager == null)
        {
            Debug.LogError("GameManager not set!");
        }
        ToolTransform = gameObject.transform.position;
        Timer = gameObject.GetComponentInChildren<TimerHandler>();
        VfxOutcome = gameObject.GetComponentInChildren<VfxHandler>();
        animator = gameObject.GetComponent<Animator>();
        arrow = gameObject.GetComponentInChildren<Arrow>();


    }
   
    private void OnTriggerEnter(Collider other)
    {   
        // Get reference to the XRGrabInteractable component
        GrabCheckingIngrident = other.GetComponent<GrabingChecking>();


        if (GrabCheckingIngrident.isGrabbed == false)
        {
            currentStep = listOfThisToolSteps[currentStepIndex];
            AssingIfThereIsNoClone();
            GameObject collidedIngredient = other.gameObject;
        
        
            // Debugging informations

            Debug.Log("CollidedIngredient name: " + collidedIngredient);
        Debug.Log("CurrentStep IngredientClone is: " + currentStep.IngredientClone);

        if (collidedIngredient == (currentStep.IngredientClone))
        {
            CountingIngridentIndex++;
            //Step Counting Checking
            if ((currentStep.IngredientsCounting != CountingIngridentIndex )&&((currentStep.IngredientsCounting - CountingIngridentIndex)> 0))
            {
                PositionIngridentAgain(collidedIngredient);
                _gameManager.TurnNotificationON("need " + (currentStep.IngredientsCounting - CountingIngridentIndex) + " more " + currentStep.action.Ingredient + "s", Color.yellow);
                currentStep.StepPrefab.DisIncrementCounting(currentStep.IngredientsCounting - CountingIngridentIndex);
                
            }
            else
            //Check if the Player took the outcome if there 

            {
                    collidedIngredient.GetComponent<XRGrabInteractable>().enabled = false;

                    if ((VfxOutcome != null && VfxOutcome.Vfx.activeSelf == true))
                {
                    _gameManager.TurnNotificationON("Please Take your OutCome First", Color.white);

                }
                 else
                {
                   

                    currentStep.StepPrefab.CountingLabel.SetActive(false);
                        currentStep.StepPrefab.CheckToggle();
                    
                    Debug.Log("Checking Toggle");
                        _gameManager.TurnNotificationON(currentStep.StepDescription + " is Checked ", Color.green);




                        HandleCountAndPosition(collidedIngredient);


                    // Checking Outcome and Instantiate it if there is 
                    if (!string.IsNullOrEmpty(currentStep.Outcome))
                    {
                        OutcomeCurrent = currentStep.Outcome; 
                        CheckAndHandleOutcome();

                     }
                        if (currentStepIndex == listOfThisToolSteps.Count - 1)
                           {
                            if (arrow.gameObject.activeSelf == true)
                            {
                                arrow.gameObject.SetActive(false);
                            }

                        }
                    // Increment step index if there are more steps
                  
                    IncrementStepIndex();
                    CountingIngridentIndex = 0;
                        
                    }
            }
        }
        else
        {
            Debug.Log("Wrong ingredient");
            _gameManager.TurnNotificationON("Please read the steps carefully!", Color.white);
        }
            
    }
    }

    
   



    private void CheckAndHandleOutcome()
    {

            PlayAnimation();
            Timer.SetTimerAndVFX(currentStep.timer);
       
        CountdownCompleted += OnCountdownCompleted;
           

        
    }
    async void OnCountdownCompleted()
    {
        // Call  methods on Timer CountDown  
        
        VfxOutcome.gameObject.SetActive(true);
        await _gameManager.InstantiateOutcomeAndHandleInteraction(OutcomeCurrent);
        VfxOutcome.SetCurrentOutcomeName(OutcomeCurrent);
        StopAnimation();
        GameManager.Instance.RecipeOutcomeAnimationHandler();
        DestroyUsedIngredients();
        CountdownCompleted -= OnCountdownCompleted;


    }
    private void IncrementStepIndex()
    {
        if (currentStepIndex < listOfThisToolSteps.Count - 1)
            currentStepIndex++;
        currentStep = listOfThisToolSteps[currentStepIndex];
        
    }
    private void DestroyUsedIngredients()
    {
        foreach (GameObject usedIngredient in usedIngredients)
        {
            // Addressables.Release(usedIngredient);
            usedIngredient.SetActive(false);
            Debug.Log("Destroying " + usedIngredient.name);
        }
    }
    private void AssingIfThereIsNoClone()
    {
        if (currentStep.action.Ingredient == "None" && currentStep.IngredientClone == null)
        {
            currentStep.IngredientClone = _gameManager.GetOutcome(currentStep.action.IngredientOutcome);
            //currentStep.action.Ingredient = currentStep.action.IngredientOutcome; 


        }
    }
    public void SetRecipeIngridentLoader(RecipeIngridentLoader loader)
    {
        _recipeIngridentLoader = loader;
    }
    public void SetGameManager(GameManager gameManager)
    {
        _gameManager = gameManager;
    }
    private void HandleCountAndPosition(GameObject collidedIngredient)
    {
        foreach (var ingredient in _recipeIngridentLoader.InstantiatedIngridents)
        {
            if (ingredient.InstantiatedIngridentclone == collidedIngredient)
            {
                if (ingredient.Counting > 1)
                {
                    //position default
                    collidedIngredient.transform.position = ingredient.DefaultPosition;
                    Debug.Log("Reset position of ingredient: " + collidedIngredient.name);

                    ingredient.Counting--;
                    Debug.Log("Decreased count of ingredient: " + ingredient.Name);
                }
               
            }
        }
        // Add the ingredient to the used ingredients
        usedIngredients.Add(collidedIngredient);
        Debug.Log("Added ingredient to used list: " + collidedIngredient.name);
    }
    private void PositionIngridentAgain(GameObject collidedIngredient)
    {

        foreach (var ingredient in _recipeIngridentLoader.InstantiatedIngridents)
        {
            if (ingredient.InstantiatedIngridentclone == collidedIngredient)
            {
                
                    //position default
                    collidedIngredient.transform.position = ingredient.DefaultPosition;
                    Debug.Log("Reset position of ingredient: " + collidedIngredient.name);
                



            }
        }
    }

    public void PlayAnimation()
    {
       
        if (animator != null)
        {
            // Trigger the  animation
            animator.SetTrigger("Start");
        }
        else
        {
            Debug.LogWarning("Animator component is not set for the spoon GameObject.");
        }

    }

    public void StopAnimation()
    {
        if (animator != null)
        {
            // Trigger the  animation
            animator.SetTrigger("Stop");
        }
        else
        {
            Debug.LogWarning("Animator component is not set for the spoon GameObject.");
        }
    }
}

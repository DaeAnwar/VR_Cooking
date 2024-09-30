using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using System.Collections;
using System;

using System.Threading.Tasks;
using Random = System.Random;

[System.Serializable]
public class AssetReferenceRecipeContainer : AssetReferenceT<RecipeContainer>
{
    public AssetReferenceRecipeContainer(string guid) : base(guid)
    {
    }
}
[System.Serializable]
public class InstatietedOutcome
{

    public string OutcomeName;
    public GameObject OutcomeClone;




}

public class GameManager : MonoBehaviour, IGameManager
{
    [SerializeField] Transform Target;
   
    public ControllerChef Chef;

    RecipeController CurrentRecipe;
    [Space]
    public int Number_of_steps=0;
    public int CheckedSteps = 0;
    public List<Infos> Infos;
    [SerializeField] DidUknow UIinfo;
    [Space]
    [Space]
    [SerializeField] AssetReferenceRecipeContainer recipeContainer;
    private RecipeContainer _recipeContainer; 
    public RectTransform recipeHolder;
    public RecipeController recipePrefab;
    [Space]
    [Space]
  
    private GameObject currentOutcomeClone;
    private string currentOutcomeName;
    [SerializeField] float PopUpDuration = 4;
    [SerializeField] PopUpTool NotificationPrefab ;
    private Transform playerCamera;

    public List<InstatietedOutcome> InstatiatedOutcomes;
    private int InstatiatedOutcomesIndex=-1;
    public static GameManager Instance { get; private set; }
    void Awake()
    {
        playerCamera = Camera.main.transform;
        if (Instance == null)
        {
            Instance = this;
            // Ensure the GameManager persists between scenes
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            // If an instance already exists, destroy this one
            Destroy(gameObject);
        }
    }
   public void SetCurrentRecipe(RecipeController currentRecipePrefab)
    {
        CurrentRecipe = currentRecipePrefab;
    }
    public void SetYouPlayedIt()
    {
        CurrentRecipe.PlayedIt.SetActive(true);

    }
    public void SetRecipeData()
    {
        recipeContainer.LoadAssetAsync().Completed += OnAddressablRecipeLoaded;


    }
        void OnAddressablRecipeLoaded(AsyncOperationHandle<RecipeContainer> handle)
        {
            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                RecipeContainer loadedRecipeContainer = handle.Result;
                SetDownloadedRecipeContainer(loadedRecipeContainer);
                Debug.Log("RecipeContainer loaded successfully!");

            }
            else
            {
                // Handle the download error
                Debug.Log("Error downloading RecipeContainer");
            }

        }
        public void SetDownloadedRecipeContainer(RecipeContainer downloadedRecipeContainer)
    {
            // Assign the downloaded RecipeContainer to the GameManager's recipeContainer field
            _recipeContainer = downloadedRecipeContainer;
       
        InstantiateRecipes();
        // Instantiate recipes after downloading and setting the RecipeContainer

    }

    void InstantiateRecipes()
    {
        if (recipeContainer == null || recipeHolder == null || recipePrefab == null)
        {
            Debug.LogError("RecipeContainer, recipeHolder, or recipePrefab is not assigned in the GameManager.");
            return;
        }

        foreach (RecipeData recipeData in _recipeContainer.recipes)
        {
            var recipeClone = Instantiate(recipePrefab, recipeHolder);


            if (recipeClone != null)
            {
                recipeClone.SetData(recipeData);
            }
            else
            {
                Debug.LogError("RecipeController component not found on the recipe prefab.");
            }
        }
    }

    /*---------------------------------------------------------------------------------------------*/
   

    // Retrieves the current outcome clone
    public GameObject GetOutcome(string IngridentName)
    {
        foreach(InstatietedOutcome InstatietedOutcome in InstatiatedOutcomes)
        {
            Debug.Log("FelGameManager el IngridentName = "+IngridentName);
            Debug.Log("FelGameManager el InstatietedOutcome = " + InstatietedOutcome.OutcomeName);


            if (IngridentName == InstatietedOutcome.OutcomeName)
            {
                Debug.Log("GameManagerReturned IngridentName " + IngridentName);
                Debug.Log("GameManagerReturned InstatietedOutcome.OutcomeName" + InstatietedOutcome.OutcomeName);
                Debug.Log("GameManagerReturned InstatietedOutcome.OutcomeClone" + InstatietedOutcome.OutcomeClone);

                return InstatietedOutcome.OutcomeClone;


            }
        }
        Debug.Log("GameManagerReturned Null");

        return null;

    }

    // Instantiates the outcome prefab and handles interaction with the ingredient
    public async Task InstantiateOutcomeAndHandleInteraction(string outcomePrefabName)
    {
        Debug.Log("outcomePrefabName: " + outcomePrefabName);
        if (string.IsNullOrEmpty(outcomePrefabName))
        {
            Debug.Log("outcomePrefabName is empty");
            return;
        }

        var handle = await Addressables.LoadAssetAsync<GameObject>(outcomePrefabName).Task;

        if (handle != null)
        {
            GameObject outcomeClone = Instantiate(handle);

            // Increment the index
            InstatiatedOutcomesIndex++;

            // Make sure the list is large enough to accommodate the new outcome
            while (InstatiatedOutcomes.Count <= InstatiatedOutcomesIndex)
            {
                // Add new InstatietedOutcome objects to the list if needed
                InstatiatedOutcomes.Add(new InstatietedOutcome());
            }

            // Store the outcome clone in the InstatiatedOutcomes list
            InstatiatedOutcomes[InstatiatedOutcomesIndex].OutcomeClone = outcomeClone;
            InstatiatedOutcomes[InstatiatedOutcomesIndex].OutcomeName = outcomePrefabName;
        }
        else
        {
            Debug.LogError("Failed to load outcome prefab: " + outcomePrefabName);
        }
    }
    /*---------------------------------------------------------------------------------------------*/

    public void TurnNotificationON(string notifcationtext,Color notificationColor)
    {
        NotificationPrefab.SetPosition(playerCamera.position + playerCamera.forward * 1f, playerCamera.rotation);
        NotificationPrefab.SetColor(notificationColor);
        NotificationPrefab.SetText(notifcationtext);
        StartCoroutine(ShowPanelCoroutine(PopUpDuration));
    }

    IEnumerator ShowPanelCoroutine(float duration)
    {

        NotificationPrefab.NotificationPanel.SetActive(true);

        // Wait for the specified duration
        yield return new WaitForSeconds(duration);

        // Deactivate the panel
        NotificationPrefab.NotificationPanel.SetActive(false);
    }
    /**************************************************************************************************************************/
    public void RecipeOutcomeAnimationHandler()
    {
        Random random = new Random();
        int randomIndex = random.Next(8, 11); // Generates a random number between 4 and 8 (inclusive)
        PlayAnimationAndAudio(randomIndex);

    }
    public void CheckStepsCompleted()
    {
        CheckedSteps++;
        if (CheckedSteps < Number_of_steps)
        {
            Random random = new Random();
            int randomIndex = random.Next(4,7); // Generates a random number between 4 and 8 (inclusive)
            PlayAnimationAndAudio(randomIndex);
        }
        if (Number_of_steps == CheckedSteps)
        {
            //DidUKnowPanel.gameObject.SetActive(true);
            MoveToTarget(Target, 12);
            UIinfo.HandleRecipeInfosView(Infos);
            //UIinfo.EnableUI();
            //EventManager.OnStepsChecked?.Invoke(Infos);
        }


    }
    /*************************************************************************************************/
    private HashSet<int> onceOnlyIndices = new HashSet<int> { 2 };
    private List<int> executedIndices = new List<int>();

    public void PlayAnimationAndAudio(int index)
    {
        if (onceOnlyIndices.Contains(index))
        {
            if (!executedIndices.Contains(index))
            {
                executedIndices.Add(index);
                Chef.PlayAnimationAndAudio(index);
            }
        }
        else
        {
            Chef.PlayAnimationAndAudio(index);
        }
        
        
    }
    public void MoveToTarget(Transform Target, int index)
    {
        Chef.MoveToTarget(Target, index);
    }

}
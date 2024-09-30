using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;
using UnityEngine.AddressableAssets;

public static class EventManager 
{
    public static  UnityAction<RecipeData> OnRequestDetails;
    public static  UnityAction<RecipeData> OnRequestSteps;
    public static UnityAction<AssetReferenceGameObject> OnRequestKitchen;
    public static UnityAction<RecipeData> OnRequestIngridents;
    public static UnityAction<List<Infos>> OnStepsChecked;




}

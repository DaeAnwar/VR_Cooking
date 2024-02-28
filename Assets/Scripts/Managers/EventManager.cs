using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;

public static class EventManager 
{
    public static  UnityAction<RecipeData> OnRequestDetails;
    public static  UnityAction<RecipeData> OnRequestSteps;

}

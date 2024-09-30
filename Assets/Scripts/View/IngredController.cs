using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IngredController : MonoBehaviour
{
    [SerializeField] Text StepText;

    public void SetIngredient(Ingredient Ingredient)
    {
        StepText.text = Ingredient.Name;


    }
}
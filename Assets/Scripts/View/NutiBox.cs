using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NutiBox : MonoBehaviour
{
    [SerializeField] Text NutName;
    [SerializeField] Text NutValue;


    public void SetNutBox(Nutrients Nutrient)
    {
        NutName.text = Nutrient.Name;
        NutValue.text = $"{Nutrient.Value}";



    }
}

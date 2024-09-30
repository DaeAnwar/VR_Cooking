using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.Events;


public class StepController : MonoBehaviour
{

    [SerializeField] Toggle StepToggle;
    
    [SerializeField] Text StepText;
    [SerializeField] public GameObject CountingLabel;
    [SerializeField] public Text IngridentCounting;
    public void CheckToggle() {

        StepToggle.isOn = true;
        GameManager.Instance.CheckStepsCompleted();
    } 




    public void SetStep(string step , int Counting)
    {
        StepText.text = step;
        if (Counting == 1)
        {
            CountingLabel.SetActive(false);
        }
        else { 
        IngridentCounting.text = Counting.ToString();
        }

    }
    public void DisIncrementCounting(int Counting)
    {
        IngridentCounting.text = Counting.ToString(); 
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StepController : MonoBehaviour
{
    [SerializeField] Text StepText;

    public void SetStep(string step)
    {
        StepText.text = step;


    }
}

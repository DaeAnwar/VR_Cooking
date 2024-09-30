using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VfxHandler : MonoBehaviour
{
    private string OutcomeName;
    

    
    public GameObject Vfx;
    public VFXTrigger Vfxtrigger;



    public void SetCurrentOutcomeName(string outcome)
    {
        OutcomeName = outcome;
        Debug.Log("fel VFX SetCurrentOutcomeName(currentStep.Outcome) = " + OutcomeName);
        Vfxtrigger.setOutcome(OutcomeName);
    }
   
}

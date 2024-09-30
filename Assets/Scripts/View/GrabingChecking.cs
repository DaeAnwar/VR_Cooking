using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(XRGrabInteractable))]

public class GrabingChecking : MonoBehaviour
{
    public bool isGrabbed;
    private XRGrabInteractable grabInteractable;

    private void Awake()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();
    }
    private void OnEnable()
    {
        grabInteractable.selectEntered.AddListener(HandleIngredientSelectEnter);
        grabInteractable.selectExited.AddListener(HandleIngredientSelectExit);

    }

    

    private void OnDisable()
    {
        grabInteractable.selectEntered.RemoveListener(HandleIngredientSelectEnter);
        grabInteractable.selectExited.RemoveListener(HandleIngredientSelectExit);

    }

    private void HandleIngredientSelectEnter(SelectEnterEventArgs arg0)
    {
        isGrabbed = true;
        Debug.Log("Ingredient Grabbed");
    }

    private void HandleIngredientSelectExit(SelectExitEventArgs arg0)
    {
        isGrabbed = false;
        Debug.Log("Ingredient Dropped");
    }

}

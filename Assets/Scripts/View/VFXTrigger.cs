using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXTrigger : MonoBehaviour
{
    public string OutcomeName;
    public GameObject Outcome;


    public void setOutcome(string outcomename)
    {
        OutcomeName = outcomename;
    }
    void OnCollisionExit(Collision other)
    {
        if (GameManager.Instance.GetOutcome(OutcomeName) != null)
        {
            Outcome = GameManager.Instance.GetOutcome(OutcomeName);
        }
        Debug.Log("OutcomeJe lel VFXTrigger " + Outcome);

        GameObject collidedIngredient = other.gameObject;

        if (collidedIngredient == Outcome)
        {
            Debug.Log("Tssaker el vfx " + Outcome);

            gameObject.SetActive(false);

        }
    }
}

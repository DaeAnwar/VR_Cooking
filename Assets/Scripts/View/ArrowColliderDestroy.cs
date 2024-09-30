using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowColliderDestroy : MonoBehaviour
{
    [SerializeField] GameObject MainArrow;
    public MonoBehaviour KitchenViewr;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) { 

            Destroy(MainArrow);
            if (KitchenViewr != null)
            {
                KitchenViewr.enabled = true;
            }

        }
    }
}

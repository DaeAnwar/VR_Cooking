using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class KitchenContoller : MonoBehaviour
{
    [SerializeField] Transform Target;
    [SerializeField] GameObject Home; 
    [SerializeField] AssetReferenceGameObject Kitchen;
    public void SetAdressableKitchen()
    {
        EventManager.OnRequestKitchen?.Invoke(Kitchen);
        GameManager.Instance.MoveToTarget(Target,1);
        Destroy(Home);
       // Chef.MoveToAndExplain(new Vector3(8, 0.000101f, 2.797f));


    }

}

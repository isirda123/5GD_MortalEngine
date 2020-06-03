using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
public class ResourceInStock : MonoBehaviour
{
    public static event Action<GameManager.ResourceType> ResourceEmpty;
    public static event Action<ResourceInStock> ChangeStock;

    public ResourcesInfos resourcesInfos;
    [SerializeField] private float amontInStock;

    public float NumberInStock
    {
        get
        {
            return amontInStock;
        }
        set
        {
            if (value <= 0)
                ResourceEmpty?.Invoke(resourcesInfos.resourceType);
            if (resourcesInfos.resourceType == GameManager.ResourceType.Mouflu)
            {
                amontInStock = Mathf.Clamp(value, 0, amontInStockMax + DecretManager.Instance.totalDecreeInfos.maxMouffluFlat);
            }
            else if (resourcesInfos.resourceType == GameManager.ResourceType.Rock)
            {
                amontInStock = Mathf.Clamp(value, 0, amontInStockMax + DecretManager.Instance.totalDecreeInfos.maxRockFlat);
            }
            else if(resourcesInfos.resourceType == GameManager.ResourceType.Wood)
            {
                amontInStock = Mathf.Clamp(value, 0, amontInStockMax + DecretManager.Instance.totalDecreeInfos.maxWoodFlat);
            }
            else if (resourcesInfos.resourceType == GameManager.ResourceType.Berry)
            {
                amontInStock = Mathf.Clamp(value, 0, amontInStockMax + DecretManager.Instance.totalDecreeInfos.maxBerryFlat);
            }
            ChangeStock?.Invoke(this);
        }
    }

    [SerializeField] int amontInStockMax;
}

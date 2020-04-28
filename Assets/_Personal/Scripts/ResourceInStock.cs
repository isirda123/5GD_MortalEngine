using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
public class ResourceInStock : MonoBehaviour
{
    public GameManager.ResourceType resourceType;
    public ResourcesInfos resourcesInfos;
    private float numberInStock;
    public static event Action<GameManager.ResourceType> ResourceEmpty;
    public Sprite resourceImage;
    public float NumberInStock {
        get { return numberInStock; }
        set {
            if (value <= 0)
                ResourceEmpty?.Invoke(resourceType);
            numberInStock = Mathf.Clamp(value, 0, maxStock);
        }
    }
    [SerializeField] int maxStock;

    public GameManager.ResourceUsage ResourceUsage
    {
        get
        {
            GameManager.ResourceUsage resourceUsages = new GameManager.ResourceUsage();
            resourceUsages.resourceInStock = this;
            for (int i = 0; i < GameManager.Instance.needs.Length; i++)
            {
                Need need = GameManager.Instance.needs[i];
                //calculate energy used 
                if (resourcesInfos.resourceType == need.resourceUsed.resourcesInfos.resourceType)
                {
                    resourceUsages.resourceUsedPerMinute += need.resourceUsed.resourcesInfos.ReturnEnergyUseFor(need.needType);
                    //calculate time it is ok 
                    resourceUsages.lifeTime = GameManager.Instance.GetResourceInStock(resourcesInfos.resourceType).NumberInStock / resourceUsages.resourceUsedPerMinute;
                }
            }
            return resourceUsages;
        }
    }
}

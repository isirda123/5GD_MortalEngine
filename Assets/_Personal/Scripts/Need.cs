using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Need : MonoBehaviour
{
    public NeedViewer needViewer;
    public enum NeedType
    {
        Build,
        Food,
        Energy
    }
    public NeedType needType;
    [SerializeField] public ResourceInStock resourceUsed;
    [SerializeField] public GameManager.ResourceType[] resourcesUsable;
    [SerializeField] public ResourcesInfos[] resourceForValidation;
    [HideInInspector] public float multiplicateur;

    public float LifeTime
    {
        get
        {
            float lifeTime = 0;
            List<ResourceInStock> resourcesInStock = GetUsableResourcesInStock(this);
            for (int j = 0; j < resourcesInStock.Count; j++)
            {
                lifeTime += resourcesInStock[j].ResourceUsage.lifeTime;
            }
            return lifeTime;
        }
    }

    public bool CanUseResource(GameManager.ResourceType resourceType)
    { //check if the need can use a resource type
        bool canIUseIt = false;
        for (int i = 0; i < resourcesUsable.Length; i++)
        {
            if (resourcesUsable[i] == resourceType)
                canIUseIt = true;
        }
        return canIUseIt;
    }

    private List<ResourceInStock> GetUsableResourcesInStock(Need need)
    {
        List<ResourceInStock> usableResourcesInStock = new List<ResourceInStock>();
        for (int i = 0; i < need.resourcesUsable.Length; i++)
        {
            ResourceInStock resource = GameManager.Instance.GetResourceInStock(need.resourcesUsable[i]);
            usableResourcesInStock.Add(resource);
        }
        return usableResourcesInStock;
    }

    public int TilesNeeded
    {
        get
        {
            float wastOfEnergyUsed = 0;
            switch (needType)
            {
                case NeedType.Energy:
                    wastOfEnergyUsed = resourceUsed.resourcesInfos.wastForEnergyPerRound;
                    break;
                case NeedType.Food:
                    wastOfEnergyUsed = resourceUsed.resourcesInfos.wastForFoodPerRound;
                    break;
                case NeedType.Build:
                    wastOfEnergyUsed = resourceUsed.resourcesInfos.wastForBuildPerRound;
                    break;
            }
            //print("Need " + Mathf.CeilToInt(wastOfEnergyUsed / resourceUsed.resourcesInfos.ResourcesPerMinute)+ "of " + resourceUsed.resourceType);
            return Mathf.CeilToInt( wastOfEnergyUsed / resourceUsed.resourcesInfos.WonPerRound);
        }
    }

    public void UseResources()
    {
        GameManager.Instance.GetResourceInStock(resourceUsed.resourceType).NumberInStock
                    -= resourceUsed.resourcesInfos.ReturnEnergyUseFor(needType) * multiplicateur;
    }

    private void ChangeUsingRessource(GameManager.ResourceType currentRessource)
    {
        if (currentRessource == resourceUsed.resourceType)
        {
            for (int i = 0; i < resourcesUsable.Length; i++)
            {
                if (resourcesUsable[i] != currentRessource)
                {
                    ResourceInStock otherResourceUsable = GameManager.Instance.GetResourceInStock(resourcesUsable[i]);
                    if (otherResourceUsable.NumberInStock > 0)
                    {
                        resourceUsed = otherResourceUsable;
                        SetViewerTextAndImage(resourceUsed.resourcesInfos);
                    }
                    else
                    {
                        if(multiplicateur > 0)
                            GameManager.Instance.EndLevel(false);
                    }
                }
            }
        }
    }

    public void SetViewerTextAndImage(ResourcesInfos resourceInfos)
    {
        needViewer.SetResourceUsedText((int)Mathf.Round(GameManager.Instance.GetResourceInStock(resourceUsed.resourceType).NumberInStock));
        needViewer.SetResourceUsedPerRoundText((int)GameManager.Instance.GetResourceInStock(resourceUsed.resourceType).resourcesInfos.ReturnEnergyUseFor(needType));
        needViewer.SetImageResourceUsed(resourceUsed.resourcesInfos.sprite);
    }

    private void Start()
    {
        SetViewerTextAndImage(resourceUsed.resourcesInfos);
        ResourceInStock.ResourceEmpty += ChangeUsingRessource;
        ResourceViewer.ChangeResourceUsed += SetViewerTextAndImage;
    }
    private void OnDestroy()
    {
        ResourceInStock.ResourceEmpty -= ChangeUsingRessource;
        ResourceViewer.ChangeResourceUsed -= SetViewerTextAndImage;
    }

}

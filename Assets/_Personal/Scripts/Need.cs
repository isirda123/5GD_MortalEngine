using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Need : MonoBehaviour
{
    public enum NeedType
    {
        Build,
        Food,
        Energy
    }
    public NeedType needType;
    [HideInInspector] private ResourceInStock resourceUsed;
    public static event Action<Need> ResourceUsedChange;
    public ResourceInStock ResourceUsed
    {
        get { return resourceUsed; }
        set
        {
            resourceUsed = value;
            ResourceUsedChange?.Invoke(this);
        }
    }
    [SerializeField] public GameManager.ResourceType[] resourcesUsable;
    [SerializeField] public ResourcesInfos[] resourceForValidation;

    [HideInInspector] private float multiplicator;
    public float Multiplicator { get { return multiplicator - DecretManager.Instance.GetNeedConsumption(needType);} set { multiplicator = value; } }

    public bool CanUseResource(GameManager.ResourceType resourceType)
    {   
        //check if the need can use a resource type
        bool canIUseIt = false;
        for (int i = 0; i < resourcesUsable.Length; i++)
        {
            if (resourcesUsable[i] == resourceType)
                canIUseIt = true;
        }
        return canIUseIt;
    }

    public int TilesNeeded
    {
        get
        {
            float wastOfEnergyUsed = 0;
            switch (needType)
            {
                case NeedType.Energy:
                    wastOfEnergyUsed = ResourceUsed.resourcesInfos.wastForEnergyPerRound;
                    break;
                case NeedType.Food:
                    wastOfEnergyUsed = ResourceUsed.resourcesInfos.wastForFoodPerRound;
                    break;
                case NeedType.Build:
                    wastOfEnergyUsed = ResourceUsed.resourcesInfos.wastForBuildPerRound;
                    break;
            }
            return Mathf.CeilToInt( wastOfEnergyUsed / ResourceUsed.resourcesInfos.WonPerRound);
        }
    }

    public void UseResources()
    {
        ResourceUsed.NumberInStock -= (ResourceUsed.resourcesInfos.GetAmontUseFor(needType) * Multiplicator);
    }
}

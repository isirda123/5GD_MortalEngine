using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "NewResourceData", menuName = "ScriptableObjects/ResourceData")]
public class ResourcesInfos : ScriptableObject
{
    public GameManager.ResourceType resourceType;
    public float resourcesTimeToMine;
    public float resourcesTimeToRespawn;
    public float resourcesAmount;
    public float ResourcesPerMinute
    {
        get { float wonPerMinute = resourcesAmount / (resourcesTimeToMine + resourcesTimeToRespawn) * 60;
            return wonPerMinute;}
    }
    public float wastForEnergyPerMinute;
    public float wastForBuildPerMinute;
    public float wastForFoodPerMinute;

    public float ReturnEnergyUseFor(Need.NeedType needType)
    {
        float amount = -1;
        switch (needType)
        {
            case Need.NeedType.Energy:
                amount = wastForEnergyPerMinute;
                break;
            case Need.NeedType.Food:
                amount = wastForFoodPerMinute;
                break;
            case Need.NeedType.Build:
                amount = wastForBuildPerMinute;
                break;
        }
        return amount;
    }
}

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

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "NewResourceData", menuName = "ScriptableObjects/ResourceData")]
public class ResourcesInfos : ScriptableObject
{
    public GameManager.ResourceType resourceType;
    public float resourcesRoundsToRespawn;
    public float resourcesAmount;
    public float wastForEnergyPerRound;
    public float wastForBuildPerRound;
    public float wastForFoodPerRound;
    public Sprite sprite;
    public int nbrOfTurnsToRegrow;

    public float WonPerRound
    {
        get { float wonPerRound; wonPerRound = resourcesAmount / resourcesRoundsToRespawn; return wonPerRound; }
    }
    public float GetAmontUseFor(Need.NeedType needType)
    {
        float amount = -1;
        switch (needType)
        {
            case Need.NeedType.Energy:
                amount = wastForEnergyPerRound;
                break;
            case Need.NeedType.Food:
                amount = wastForFoodPerRound;
                break;
            case Need.NeedType.Build:
                amount = wastForBuildPerRound;
                break;
        }
        return amount;
    }
}

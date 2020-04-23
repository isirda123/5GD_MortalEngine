using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


[CreateAssetMenu(fileName = "NewDecreeScriptable", menuName = "ScriptableObjects/DecreeScriptable")]
public class DecreeScriptable : ScriptableObject
{
    [Flags]
    public enum TargetFields
    {
        typeOfDecree = 1<<0,
        title = 1 << 1,
        flavorText = 1 <<2,
        maxFoodPercent = 1 << 3,
        maxEnergyPercent = 1 << 4,
        maxConstructionPercent = 1 << 5,
        consumptionFoodPercent = 1 << 6,
        consumptionEnergyPercent = 1 << 7,
        consumptionBuildPercent = 1 << 8,
        speedPercent = 1 << 9,
        collectSpeedPercent = 1 << 10,
        collectRangeMax = 1 << 11,
        giveMouflu = 1 << 12,
        giveRock = 1 << 13,
        giveWood = 1 << 14,
        giveBerry = 1 << 15,
        collectQuantityMouflu = 1 << 16,
        collectQuantityRock = 1 << 17,
        collectQuantityWood = 1 << 18, 
        collectQuantityBerry = 1 << 19,
        speedRespawnMoufluPercent = 1 << 20,
        speedRespawnRockPercent = 1 << 21,
        speedRespawnWoodPercent = 1 << 22,
        speedRespawnBerryPercent = 1 << 23
    }

    public DecretsInfos decretsInfos = new DecretsInfos();


    public TargetFields targetField = (TargetFields)((int)TargetFields.typeOfDecree | (int)TargetFields.title | (int)TargetFields.flavorText);

    public bool CheckField(TargetFields fieldToCheck)
    {
        return ((int)targetField & (int)fieldToCheck) == (int)fieldToCheck;
    }

}

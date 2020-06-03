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
        typeOfDecree = 1 << 0,
        title = 1 << 1,
        flavorText = 1 << 2,
        maxMouffluFlat = 1 << 3,
        maxRockFlat = 1 << 4,
        maxWoodFlat = 1 << 5,
        maxBerryFlat = 1 << 6,
        consumptionFoodFlat = 1 << 7,
        consumptionEnergyFlat = 1 << 8,
        consumptionBuildFlat = 1 << 9,
        collectRangeMax = 1 << 10,
        giveMouflu = 1 << 11,
        giveRock = 1 << 12,
        giveWood = 1 << 13,
        giveBerry = 1 << 14,
        collectQuantityMouflu = 1 << 15,
        collectQuantityRock = 1 << 16,
        collectQuantityWood = 1 << 17,
        collectQuantityBerry = 1 << 18,
        numberOfMove = 1 << 19,
        fly = 1 << 20,
        roundBetweenDecree = 1 << 21,
    }

    public DecretsInfos decretsInfos = new DecretsInfos();


    public TargetFields targetField = (TargetFields)((int)TargetFields.typeOfDecree | (int)TargetFields.title | (int)TargetFields.flavorText);

    public bool CheckField(TargetFields fieldToCheck)
    {
        return ((int)targetField & (int)fieldToCheck) == (int)fieldToCheck;
    }

}

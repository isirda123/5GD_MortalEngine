using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DecretsInfos
{
    public int reference;
    public typeOfDecree myTypeOfDecree;
    public string title;
    public string flavorText;
    public int maxFoodPercent;
    public int maxEnergyPercent;
    public int maxConstructionPercent;
    public int consumptionFoodPercent;
    public int consumptionEnergyPercent;
    public int consumptionBuildPercent;
    public int speedPercent;
    public int collectSpeedPercent;
    public int collectRangeMax;
    public int giveMouflu;
    public int giveRock;
    public int giveWood;
    public int giveBerry;
    public int collectQuantityMouflu;
    public int collectQuantityRock;
    public int collectQuantityWood;
    public int collectQuantityBerry;
    public int speedRespawnMoufluPercent;
    public int speedRespawnRockPercent;
    public int speedRespawnWoodPercent;
    public int speedRespawnBerryPercent;


    public enum typeOfDecree
    {
        decree,
        randomEvent,
        choice,
        yesNo
    }
}

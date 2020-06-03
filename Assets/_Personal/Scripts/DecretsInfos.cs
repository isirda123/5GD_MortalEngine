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
    public int collectRangeMax;
    public int giveMouflu;
    public int giveRock;
    public int giveWood;
    public int giveBerry;
    public int collectQuantityMouflu;
    public int collectQuantityRock;
    public int collectQuantityWood;
    public int collectQuantityBerry;


    public enum typeOfDecree
    {
        decree,
        randomEvent,
        choice,
        yesNo
    }
}

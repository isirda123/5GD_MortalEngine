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
    public int maxMouffluFlat;
    public int maxRockFlat;
    public int maxWoodFlat;
    public int maxBerryFlat;
    public int consumptionFoodFlat;
    public int consumptionEnergyFlat;
    public int consumptionBuildFlat;
    public int collectRangeMax;
    public int giveMouflu;
    public int giveRock;
    public int giveWood;
    public int giveBerry;
    public int collectQuantityMouflu;
    public int collectQuantityRock;
    public int collectQuantityWood;
    public int collectQuantityBerry;
    public int numberOfMove;
    public int fly;
    public int roundBetweenDecree;


    public enum typeOfDecree
    {
        decree,
        randomEvent,
        choice,
        yesNo
    }
}

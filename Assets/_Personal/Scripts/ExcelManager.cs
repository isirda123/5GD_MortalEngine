using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExcelManager : MonoBehaviour
{
    public string[] data;
    string[] row;

    public List<DecretsInfos> allDecree = new List<DecretsInfos>();

    // Start is called before the first frame update
    /*void Start()
    {
        TextAsset decreeData = Resources.Load<TextAsset>("DecreeData");

        data = decreeData.text.Split(new char[] { '\n' });

        for (int i =1; i <data.Length -1; i++)
        {
            row = data[i].Split(new char[] { ';' });

            if (row[1] != "")
            {
                DecretsInfos di = new DecretsInfos();

                di.reference = int.Parse(row[0]);
                //di.typeOfDecree = row[1];
                di.title = row[2];
                di.flavorText = row[3];
                di.maxFoodPercent = int.Parse(row[4]);
                di.maxEnergyPercent = int.Parse(row[5]);
                di.maxConstructionPercent = int.Parse(row[6]);
                di.consumptionFoodPercent = int.Parse(row[7]);
                di.consumptionEnergyPercent = int.Parse(row[8]);
                di.consumptionBuildPercent = int.Parse(row[9]);
                di.speedPercent = int.Parse(row[10]);
                di.collectSpeedPercent = int.Parse(row[11]);
                di.collectRangeMax = int.Parse(row[12]);
                di.giveMouflu = int.Parse(row[13]);
                di.giveRock = int.Parse(row[14]);
                di.giveWood = int.Parse(row[15]);
                di.giveBerry = int.Parse(row[16]);
                di.collectQuantityMouflu = int.Parse(row[17]);
                di.collectQuantityRock = int.Parse(row[18]);
                di.collectQuantityWood = int.Parse(row[19]);
                di.collectQuantityBerry = int.Parse(row[20]);
                di.speedRespawnMoufluPercent = int.Parse(row[21]);
                di.speedRespawnRockPercent = int.Parse(row[22]);
                di.speedRespawnWoodPercent = int.Parse(row[23]);
                di.speedRespawnBerryPercent = int.Parse(row[24]);





                allDecree.Add(di);
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }*/
}

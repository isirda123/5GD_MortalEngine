using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DecretsValidate : MonoBehaviour
{
    public DecreeScriptable personalDecree;
    public Transform decreeToModify;
    string effect;

    // Start is called before the first frame update
    void Awake()
    {
        decreeToModify = transform.parent.parent.GetChild(1);
    }

    // Update is called once per frame
    void OnEnable()
    {
        
    }

    public void SetText()
    {
        transform.GetChild(0).GetComponent<Text>().text = personalDecree.decretsInfos.title;
    }

    public void ShowCurrentDecree()
    {
        decreeToModify.GetChild(0).GetComponent<Text>().text = personalDecree.decretsInfos.title;
        decreeToModify.GetChild(1).GetComponent<Text>().text = personalDecree.decretsInfos.flavorText;
        showInfoDecret(personalDecree);
        decreeToModify.GetChild(2).GetComponent<Text>().text = effect;
    }



    void EnterKey()
    {
        effect += "\n";
    }



    public void showInfoDecret(DecreeScriptable dS)
    {
        effect = "";
        EnterKey();
        if (dS.decretsInfos.maxFoodPercent != 0)
        {
            effect += "Nourriture max: " + dS.decretsInfos.maxFoodPercent + "%";
            EnterKey();
        }
        if (dS.decretsInfos.maxEnergyPercent != 0)
        {
            effect += "Energie max : " + dS.decretsInfos.maxEnergyPercent + "%";
            EnterKey();
        }
        if (dS.decretsInfos.maxConstructionPercent != 0)
        {
            effect += "Construction max : " + dS.decretsInfos.maxConstructionPercent + "%";
            EnterKey();
        }
        if (dS.decretsInfos.consumptionFoodPercent != 0)
        {
            effect += "Consommation nourriture : " + dS.decretsInfos.consumptionFoodPercent + "%";
            EnterKey();
        }
        if (dS.decretsInfos.consumptionEnergyPercent != 0)
        {
            effect += "Consommation energie : " + dS.decretsInfos.consumptionEnergyPercent + "%";
            EnterKey();
        }
        if (dS.decretsInfos.consumptionBuildPercent != 0)
        {
            effect += "Consommation construction : " + dS.decretsInfos.consumptionBuildPercent + "%";
            EnterKey();
        }
        if (dS.decretsInfos.speedPercent != 0)
        {
            effect += "Vitesse de déplacement : " + dS.decretsInfos.speedPercent + "%";
            EnterKey();
        }
        if (dS.decretsInfos.collectSpeedPercent != 0)
        {
            effect += "Vitesse de collecte : " + dS.decretsInfos.collectSpeedPercent + "%";
            EnterKey();
        }

        if (dS.decretsInfos.collectRangeMax != 0)
        {
            effect += "Distance de collect : " + dS.decretsInfos.collectRangeMax;
            EnterKey();
        }

        if (dS.decretsInfos.giveMouflu != 0)
        {
            effect += "Mouflu : " + dS.decretsInfos.giveMouflu;
            EnterKey();
        }

        if (dS.decretsInfos.giveRock != 0)
        {
            effect += "Pierre : " + dS.decretsInfos.giveRock;
            EnterKey();
        }

        if (dS.decretsInfos.giveWood != 0)
        {
            effect += "Bois : " + dS.decretsInfos.giveWood;
            EnterKey();
        }

        if (dS.decretsInfos.giveBerry != 0)
        {
            effect += "Baie : " + dS.decretsInfos.giveBerry;
            EnterKey();
        }

        if (dS.decretsInfos.collectQuantityMouflu != 0)
        {
            effect += "Collecte Mouflu : " + dS.decretsInfos.collectQuantityMouflu;
            EnterKey();
        }

        if (dS.decretsInfos.collectQuantityRock != 0)
        {
            effect += "Collecte rock : " + dS.decretsInfos.collectQuantityRock;
            EnterKey();
        }

        if (dS.decretsInfos.collectQuantityWood != 0)
        {
            effect += "Collecte bois : " + dS.decretsInfos.collectQuantityWood;
            EnterKey();
        }

        if (dS.decretsInfos.collectQuantityBerry != 0)
        {
            effect += "Collecte baie : " + dS.decretsInfos.collectQuantityBerry;
            EnterKey();
        }

        if (dS.decretsInfos.speedRespawnMoufluPercent != 0)
        {
            effect += "Temps respawn Bouflu : " + dS.decretsInfos.collectQuantityBerry + "%";
            EnterKey();
        }

        if (dS.decretsInfos.speedRespawnRockPercent != 0)
        {
            effect += "Temps respawn pierre : " + dS.decretsInfos.speedRespawnRockPercent + "%";
            EnterKey();
        }

        if (dS.decretsInfos.speedRespawnWoodPercent != 0)
        {
            effect += "Temps respawn bois : " + dS.decretsInfos.speedRespawnWoodPercent + "%";
            EnterKey();
        }

        if (dS.decretsInfos.speedRespawnBerryPercent != 0)
        {
            effect += "Temps respawn baie : " + dS.decretsInfos.speedRespawnBerryPercent + "%";
            EnterKey();
        }
    }
}

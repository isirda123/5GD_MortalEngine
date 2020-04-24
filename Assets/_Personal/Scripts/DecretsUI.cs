using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DecretsUI : MonoBehaviour
{
    [SerializeField] public Text nameOfDecree;
    [SerializeField] public Text description;
    [SerializeField] public Text effect;

    DecreeScriptable personalDecree;


    // Start is called before the first frame update
    void Start()
    {
        personalDecree = new DecreeScriptable();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void showInfoDecret (DecreeScriptable dS)
    {
        nameOfDecree.text = dS.decretsInfos.title;
        description.text = dS.decretsInfos.flavorText;
        effect.text = "";
        EnterKey();
        if (dS.decretsInfos.maxFoodPercent != 0)
        {
            effect.text += "Nourriture max: " + dS.decretsInfos.maxFoodPercent + "%";
            EnterKey();
        }
        if (dS.decretsInfos.maxEnergyPercent != 0)
        {
            effect.text += "Energie max : " + dS.decretsInfos.maxEnergyPercent + "%";
            EnterKey();
        }
        if (dS.decretsInfos.maxConstructionPercent != 0)
        {
            effect.text += "Construction max : " + dS.decretsInfos.maxConstructionPercent + "%";
            EnterKey();
        }
        if (dS.decretsInfos.consumptionFoodPercent != 0)
        {
            effect.text += "Consommation nourriture : " + dS.decretsInfos.consumptionFoodPercent + "%";
            EnterKey();
        }
        if (dS.decretsInfos.consumptionEnergyPercent != 0)
        {
            effect.text += "Consommation energie : " + dS.decretsInfos.consumptionEnergyPercent + "%";
            EnterKey();
        }
        if (dS.decretsInfos.consumptionBuildPercent != 0)
        {
            effect.text += "Consommation construction : " + dS.decretsInfos.consumptionBuildPercent + "%";
            EnterKey();
        }
        if (dS.decretsInfos.speedPercent != 0)
        {
            effect.text += "Vitesse de déplacement : " + dS.decretsInfos.speedPercent + "%";
            EnterKey();
        }
        if (dS.decretsInfos.collectSpeedPercent != 0)
        {
            effect.text += "Vitesse de collecte : " + dS.decretsInfos.collectSpeedPercent + "%";
            EnterKey();
        }

        if (dS.decretsInfos.collectRangeMax != 0)
        {
            effect.text += "Distance de collect : " + dS.decretsInfos.collectRangeMax;
            EnterKey();
        }

        if (dS.decretsInfos.giveMouflu != 0)
        {
            effect.text += "Mouflu : " + dS.decretsInfos.giveMouflu;
            EnterKey();
        }

        if (dS.decretsInfos.giveRock != 0)
        {
            effect.text += "Pierre : " + dS.decretsInfos.giveRock;
            EnterKey();
        }

        if (dS.decretsInfos.giveWood != 0)
        {
            effect.text += "Bois : " + dS.decretsInfos.giveWood;
            EnterKey();
        }

        if (dS.decretsInfos.giveBerry != 0)
        {
            effect.text += "Baie : " + dS.decretsInfos.giveBerry;
            EnterKey();
        }

        if (dS.decretsInfos.collectQuantityMouflu != 0)
        {
            effect.text += "Collecte Mouflu : " + dS.decretsInfos.collectQuantityMouflu;
            EnterKey();
        }

        if (dS.decretsInfos.collectQuantityRock != 0)
        {
            effect.text += "Collecte rock : " + dS.decretsInfos.collectQuantityRock;
            EnterKey();
        }

        if (dS.decretsInfos.collectQuantityWood != 0)
        {
            effect.text += "Collecte bois : " + dS.decretsInfos.collectQuantityWood;
            EnterKey();
        }

        if (dS.decretsInfos.collectQuantityBerry != 0)
        {
            effect.text += "Collecte baie : " + dS.decretsInfos.collectQuantityBerry;
            EnterKey();
        }

        if (dS.decretsInfos.speedRespawnMoufluPercent != 0)
        {
            effect.text += "Temps respawn Bouflu : " + dS.decretsInfos.collectQuantityBerry + "%";
            EnterKey();
        }

        if (dS.decretsInfos.speedRespawnRockPercent != 0)
        {
            effect.text += "Temps respawn pierre : " + dS.decretsInfos.speedRespawnRockPercent + "%";
            EnterKey();
        }

        if (dS.decretsInfos.speedRespawnWoodPercent != 0)
        {
            effect.text += "Temps respawn bois : " + dS.decretsInfos.speedRespawnWoodPercent + "%";
            EnterKey();
        }

        if (dS.decretsInfos.speedRespawnBerryPercent != 0)
        {
            effect.text += "Temps respawn baie : " + dS.decretsInfos.speedRespawnBerryPercent + "%";
            EnterKey();
        }
    }


    void EnterKey()
    {
        effect.text += "\n";
    }
}

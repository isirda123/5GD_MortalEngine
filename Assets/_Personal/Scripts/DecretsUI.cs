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
        if (dS.decretsInfos.maxMouffluFlat != 0)
        {
            effect.text += "Nourriture max: " + dS.decretsInfos.maxMouffluFlat;
            EnterKey();
        }
        if (dS.decretsInfos.maxRockFlat != 0)
        {
            effect.text += "Energie max : " + dS.decretsInfos.maxRockFlat;
            EnterKey();
        }
        if (dS.decretsInfos.maxWoodFlat != 0)
        {
            effect.text += "Construction max : " + dS.decretsInfos.maxWoodFlat;
            EnterKey();
        }
        if (dS.decretsInfos.consumptionFoodFlat != 0)
        {
            effect.text += "Consommation nourriture : " + dS.decretsInfos.consumptionFoodFlat;
            EnterKey();
        }
        if (dS.decretsInfos.consumptionEnergyFlat != 0)
        {
            effect.text += "Consommation energie : " + dS.decretsInfos.consumptionEnergyFlat;
            EnterKey();
        }
        if (dS.decretsInfos.consumptionBuildFlat != 0)
        {
            effect.text += "Consommation construction : " + dS.decretsInfos.consumptionBuildFlat;
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
        if (dS.decretsInfos.numberOfMove != 0)
        {
            effect.text += "Number of Move : " + dS.decretsInfos.numberOfMove;
            EnterKey();
        }
        if (dS.decretsInfos.fly != 0)
        {
            effect.text += "You can fly now";
            EnterKey();
        }
        if (dS.decretsInfos.roundBetweenDecree != 0)
        {
            effect.text += "Round between Decree : " + dS.decretsInfos.roundBetweenDecree;
            EnterKey();
        }
    }


    void EnterKey()
    {
        effect.text += "\n";
    }
}

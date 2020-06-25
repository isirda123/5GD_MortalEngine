using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DecretsUI : MonoBehaviour
{
    [SerializeField] public Text nameOfDecree;
    [SerializeField] public Text description;
    [SerializeField] public Text effect;
    [SerializeField] public Image visual;

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
            effect.text += "Max food: " + dS.decretsInfos.maxMouffluFlat;
            EnterKey();
        }
        if (dS.decretsInfos.maxRockFlat != 0)
        {
            effect.text += "Max energy : " + dS.decretsInfos.maxRockFlat;
            EnterKey();
        }
        if (dS.decretsInfos.maxWoodFlat != 0)
        {
            effect.text += "Max build : " + dS.decretsInfos.maxWoodFlat;
            EnterKey();
        }
        if (dS.decretsInfos.consumptionFoodModificator != 0)
        {
            effect.text += "Food consumption : " + dS.decretsInfos.consumptionFoodModificator;
            EnterKey();
        }
        if (dS.decretsInfos.consumptionEnergyModificator != 0)
        {
            effect.text += "Energy consumption : " + dS.decretsInfos.consumptionEnergyModificator;
            EnterKey();
        }
        if (dS.decretsInfos.consumptionBuildModificator != 0)
        {
            effect.text += "Build consumption : " + dS.decretsInfos.consumptionBuildModificator;
            EnterKey();
        }
        if (dS.decretsInfos.collectRangeMax != 0)
        {
            effect.text += "Collect distance : " + dS.decretsInfos.collectRangeMax;
            EnterKey();
        }

        if (dS.decretsInfos.giveMouflu != 0)
        {
            effect.text += "Mouflu : " + dS.decretsInfos.giveMouflu;
            EnterKey();
        }

        if (dS.decretsInfos.giveRock != 0)
        {
            effect.text += "Rock : " + dS.decretsInfos.giveRock;
            EnterKey();
        }

        if (dS.decretsInfos.giveWood != 0)
        {
            effect.text += "Wood : " + dS.decretsInfos.giveWood;
            EnterKey();
        }

        if (dS.decretsInfos.giveBerry != 0)
        {
            effect.text += "Berry : " + dS.decretsInfos.giveBerry;
            EnterKey();
        }

        if (dS.decretsInfos.collectQuantityMouflu != 0)
        {
            effect.text += "Mouflu collect : " + dS.decretsInfos.collectQuantityMouflu;
            EnterKey();
        }

        if (dS.decretsInfos.collectQuantityRock != 0)
        {
            effect.text += "Rock collect : " + dS.decretsInfos.collectQuantityRock;
            EnterKey();
        }

        if (dS.decretsInfos.collectQuantityWood != 0)
        {
            effect.text += "Wood collect : " + dS.decretsInfos.collectQuantityWood;
            EnterKey();
        }

        if (dS.decretsInfos.collectQuantityBerry != 0)
        {
            effect.text += "Berry collect : " + dS.decretsInfos.collectQuantityBerry;
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

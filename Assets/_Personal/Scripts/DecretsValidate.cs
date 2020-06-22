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
        if (dS.decretsInfos.maxMouffluFlat != 0)
        {
            effect += "Nourriture max: " + dS.decretsInfos.maxMouffluFlat + "%";
            EnterKey();
        }
        if (dS.decretsInfos.maxRockFlat != 0)
        {
            effect += "Energie max : " + dS.decretsInfos.maxRockFlat + "%";
            EnterKey();
        }
        if (dS.decretsInfos.maxWoodFlat != 0)
        {
            effect += "Construction max : " + dS.decretsInfos.maxWoodFlat + "%";
            EnterKey();
        }
        if (dS.decretsInfos.consumptionFoodModificator != 0)
        {
            effect += "Consommation nourriture : " + dS.decretsInfos.consumptionFoodModificator + "%";
            EnterKey();
        }
        if (dS.decretsInfos.consumptionEnergyModificator != 0)
        {
            effect += "Consommation energie : " + dS.decretsInfos.consumptionEnergyModificator + "%";
            EnterKey();
        }
        if (dS.decretsInfos.consumptionBuildModificator != 0)
        {
            effect += "Consommation construction : " + dS.decretsInfos.consumptionBuildModificator + "%";
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
    }
}

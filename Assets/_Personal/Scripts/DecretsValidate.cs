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
            effect += "Max food: " + dS.decretsInfos.maxMouffluFlat + "%";
            EnterKey();
        }
        if (dS.decretsInfos.maxRockFlat != 0)
        {
            effect += "Max energy : " + dS.decretsInfos.maxRockFlat + "%";
            EnterKey();
        }
        if (dS.decretsInfos.maxWoodFlat != 0)
        {
            effect += "Max build : " + dS.decretsInfos.maxWoodFlat + "%";
            EnterKey();
        }
        if (dS.decretsInfos.consumptionFoodModificator != 0)
        {
            effect += "Food consumption : " + dS.decretsInfos.consumptionFoodModificator + "%";
            EnterKey();
        }
        if (dS.decretsInfos.consumptionEnergyModificator != 0)
        {
            effect += "Energy consumption : " + dS.decretsInfos.consumptionEnergyModificator + "%";
            EnterKey();
        }
        if (dS.decretsInfos.consumptionBuildModificator != 0)
        {
            effect += "Build consumption : " + dS.decretsInfos.consumptionBuildModificator + "%";
            EnterKey();
        }

        if (dS.decretsInfos.collectRangeMax != 0)
        {
            effect += "Collect distance : " + dS.decretsInfos.collectRangeMax;
            EnterKey();
        }

        if (dS.decretsInfos.giveMouflu != 0)
        {
            effect += "Mouflu : " + dS.decretsInfos.giveMouflu;
            EnterKey();
        }

        if (dS.decretsInfos.giveRock != 0)
        {
            effect += "Rock : " + dS.decretsInfos.giveRock;
            EnterKey();
        }

        if (dS.decretsInfos.giveWood != 0)
        {
            effect += "Wood : " + dS.decretsInfos.giveWood;
            EnterKey();
        }

        if (dS.decretsInfos.giveBerry != 0)
        {
            effect += "Berry : " + dS.decretsInfos.giveBerry;
            EnterKey();
        }

        if (dS.decretsInfos.collectQuantityMouflu != 0)
        {
            effect += "Mouflu collect : " + dS.decretsInfos.collectQuantityMouflu;
            EnterKey();
        }

        if (dS.decretsInfos.collectQuantityRock != 0)
        {
            effect += "Rock collect : " + dS.decretsInfos.collectQuantityRock;
            EnterKey();
        }

        if (dS.decretsInfos.collectQuantityWood != 0)
        {
            effect += "Wood collect : " + dS.decretsInfos.collectQuantityWood;
            EnterKey();
        }

        if (dS.decretsInfos.collectQuantityBerry != 0)
        {
            effect += "Berry collect : " + dS.decretsInfos.collectQuantityBerry;
            EnterKey();
        }
    }
}

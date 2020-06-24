using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System;
public class DecretManager : Singleton<DecretManager>
{
    [SerializeField] int numberOfRoundBetweenDecree;
    int nextDecreeRound;
    int numberOfDecreeAvailable = 0;
    [SerializeField] int timeBetweenTwoDecree;
    [SerializeField] GameObject canvasDecree;
    [SerializeField] GameObject availableDecree;
    [SerializeField] GameObject textNoDecreeAvailable;
    [SerializeField] GameObject numberOfDecreeGO;
    [SerializeField] GameObject validateButton;
    [SerializeField] GameObject decretValidateManager;
    [SerializeField] GameObject decretValidatePrefab;

    [SerializeField] Sprite baseDecreeColor;
    [SerializeField] Sprite selectedDecree;
    [SerializeField] Sprite NotValideDecree;

    [SerializeField] List<DecreeScriptable> allDecree = new List<DecreeScriptable>();
    [SerializeField] List<DecreeScriptable> decreeChoosen = new List<DecreeScriptable>();
    [SerializeField] List<DecreeScriptable> decreeValidate = new List<DecreeScriptable>();

    [HideInInspector] public DecretsInfos totalDecreeInfos;
    [HideInInspector] CharaAvatar avatar;

    public static Action DecretSelected;

    bool decreeCanvasOpen = false;
    bool decreeChoiceCanvasOpen = false;
    bool decreeAlreadySeen = false;

    int choosenDecree = 0;

    // Start is called before the first frame update
    void Start()
    {
        numberOfDecreeGO = GameObject.Find("Nb Decret");
        allDecree = Resources.LoadAll<DecreeScriptable>("Decree").ToList();
        numberOfDecreeGO.SetActive(false);
        avatar = (CharaAvatar)GameObject.FindObjectOfType(typeof(CharaAvatar));
        SetRoundWhenGetAnotherDecree();
    }

    void OnEnable()
    {
        ActionsButtons.Vote += ChangeDecreeInterface;
        RoundManager.RoundEnd += addRound;
    }

    void OnDisable()
    {
        ActionsButtons.Vote -= ChangeDecreeInterface;
        RoundManager.RoundEnd -= addRound;
    }

    // Update is called once per frame
    void Update()
    {
        if (numberOfDecreeAvailable > 0)
        {
            numberOfDecreeGO.SetActive(true);
            numberOfDecreeGO.transform.GetChild(0).GetComponent<Text>().text = numberOfDecreeAvailable.ToString();
        }
        else
        {
            numberOfDecreeGO.SetActive(false);
        }
    }

    void SetRoundWhenGetAnotherDecree()
    {
        nextDecreeRound = RoundManager.Instance.numberOfRound + numberOfRoundBetweenDecree;
    }

    void addRound()
    {
        if (RoundManager.Instance.numberOfRound != 0)
        {
            if (RoundManager.Instance.numberOfRound == nextDecreeRound -1)
            {
                NewDecree();
            }
        }
    }

    void NewDecree()
    {
        numberOfDecreeAvailable += 1;
        print("New Decree Available");
    }

    public void ChangeDecreeInterface()
    {
        HideItAll();
        if (decreeCanvasOpen == false)
        {
            decreeCanvasOpen = true;
            canvasDecree.SetActive(true);
            if (numberOfDecreeAvailable == 0)
                EmptyDecreeCanvas();
            else if (numberOfDecreeAvailable > 0)
            {
                ChoiceDecreeCanvas();
            }
        }
        else
        {
            canvasDecree.SetActive(true);
            decretValidateManager.SetActive(true);
            decreeCanvasOpen = false;
        }
    }

    void ShowAllChoosenDecree()
    {
        decretValidateManager.SetActive(true);
    }

    public void ReturntoGame()
    {
        HideItAll();
        ShowNormalUI();
    }

    public void ChoiceADecree(int WhichDecree)
    {
        UnSelectAllDecree();
        availableDecree.transform.GetChild(WhichDecree - 1).transform.GetChild(4).gameObject.SetActive(true);
        choosenDecree = WhichDecree - 1;
        print(choosenDecree);
    }

    void UnSelectAllDecree()
    {
        for (int i = 0; i < availableDecree.transform.childCount; i++)
        {
            availableDecree.transform.GetChild(i).transform.GetChild(4).gameObject.SetActive(false);
        }
    }

    void ShowNormalUI()
    {
        decreeCanvasOpen = false;
        UnSelectAllDecree();
        print("Show Game");
    }
    void HideItAll()
    {
        canvasDecree.SetActive(false);
        textNoDecreeAvailable.SetActive(false);
        availableDecree.SetActive(false);
        decretValidateManager.SetActive(false);
        print("HideAll");
    }

    void EmptyDecreeCanvas()
    {
        textNoDecreeAvailable.SetActive(true);
    }

    void ChoiceDecreeCanvas()
    {
        if (decreeAlreadySeen == false)
        {
            GetDecreesAvailable();
        }
        else
        {
            GetDecreesAlreadyGet();
        }
    }

    void GetDecreesAvailable()
    {
        decreeAlreadySeen = true;
        availableDecree.SetActive(true);
        GetDecreeFromPool();
    }

    void GetDecreeFromPool()
    {
        for (int i = 0; i < 3; i++)
        {
            print(i);
            int randomDecreeIndex = UnityEngine.Random.Range(0, allDecree.Count);
            decreeChoosen.Add(allDecree[randomDecreeIndex]);
            availableDecree.transform.GetChild(i).GetComponent<DecretsUI>().showInfoDecret(allDecree[randomDecreeIndex]);
            allDecree.Remove(decreeChoosen[i]);
        }
    }

    void GetDecreesAlreadyGet()
    {
        availableDecree.SetActive(true);
    }


    public void ValidateDecree()
    {
        numberOfDecreeAvailable -= 1;
        SetRoundWhenGetAnotherDecree();
        SetAllDecreeInfos(decreeChoosen[choosenDecree]);
        decreeValidate.Add(decreeChoosen[choosenDecree]);
        GameObject dVP = Instantiate(decretValidatePrefab, decretValidateManager.transform.GetChild(0));
        dVP.GetComponent<DecretsValidate>().personalDecree = decreeChoosen[choosenDecree];
        dVP.GetComponent<DecretsValidate>().SetText();
        print(decreeChoosen[choosenDecree]);
        decreeChoosen.RemoveAt(choosenDecree);
        for (int i = 0; i < 2; i++)
        {
            allDecree.Add(decreeChoosen[0]);
            decreeChoosen.RemoveAt(0);
        }
        decreeAlreadySeen = false;
        UnSelectAllDecree();
        decreeCanvasOpen = false;

        if (numberOfDecreeAvailable > 0)
        {
            HideItAll();
            ChangeDecreeInterface();
        }
        else
        {
            HideItAll();
            ShowNormalUI();
        }
        DecretSelected?.Invoke();
        SoundManager.Instance.DecreeSound();
        RoundManager.Instance.LaunchEndRound();
    }


    void SetAllDecreeInfos(DecreeScriptable dS)
    {
        totalDecreeInfos.maxMouffluFlat += dS.decretsInfos.maxMouffluFlat;
        totalDecreeInfos.maxRockFlat += dS.decretsInfos.maxRockFlat;
        totalDecreeInfos.maxWoodFlat += dS.decretsInfos.maxWoodFlat;
        totalDecreeInfos.consumptionFoodModificator += dS.decretsInfos.consumptionFoodModificator;
        totalDecreeInfos.consumptionEnergyModificator += dS.decretsInfos.consumptionEnergyModificator;
        totalDecreeInfos.consumptionBuildModificator += dS.decretsInfos.consumptionBuildModificator;
        totalDecreeInfos.collectRangeMax += dS.decretsInfos.collectRangeMax;

        totalDecreeInfos.giveMouflu += dS.decretsInfos.giveMouflu;
        avatar.SetResourceInStock(GameManager.ResourceType.Mouflu, dS.decretsInfos.giveMouflu);

        totalDecreeInfos.giveRock += dS.decretsInfos.giveRock;
        avatar.SetResourceInStock(GameManager.ResourceType.Rock, dS.decretsInfos.giveRock);

        totalDecreeInfos.giveWood += dS.decretsInfos.giveWood;
        avatar.SetResourceInStock(GameManager.ResourceType.Wood, dS.decretsInfos.giveWood);

        totalDecreeInfos.giveBerry += dS.decretsInfos.giveBerry;
        avatar.SetResourceInStock(GameManager.ResourceType.Berry, dS.decretsInfos.giveBerry);

        totalDecreeInfos.collectQuantityMouflu += dS.decretsInfos.collectQuantityMouflu;
        totalDecreeInfos.collectQuantityRock += dS.decretsInfos.collectQuantityRock;
        totalDecreeInfos.collectQuantityWood += dS.decretsInfos.collectQuantityWood;
        totalDecreeInfos.collectQuantityBerry += dS.decretsInfos.collectQuantityBerry;
        totalDecreeInfos.numberOfMove += dS.decretsInfos.numberOfMove;
        totalDecreeInfos.fly += dS.decretsInfos.fly;
        totalDecreeInfos.roundBetweenDecree += dS.decretsInfos.roundBetweenDecree;
    }

    public float GetNeedConsumption(Need.NeedType needType)
    {
        float needConsumption = 0;
        switch (needType)
        {
            case Need.NeedType.Build:
                needConsumption = totalDecreeInfos.consumptionBuildModificator;
                break;
            case Need.NeedType.Energy:
                needConsumption = totalDecreeInfos.consumptionEnergyModificator;
                break;
            case Need.NeedType.Food:
                needConsumption = totalDecreeInfos.consumptionFoodModificator;
                break;
        }
        return needConsumption;
    }

}

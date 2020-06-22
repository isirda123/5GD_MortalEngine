using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    [HideInInspector] public NeedViewer needViewerSelected;
    [SerializeField] public StockViewer stockViewer;
    [SerializeField] private GameObject winPopUp;
    [SerializeField] private GameObject loosePopUp;
    [SerializeField] ActionsButtons[] actionsButtons;
    [SerializeField] public NeedViewer[] needViewers;
    [SerializeField] PopUpResourceStock popUpResourceStock;
    [SerializeField] PopUpResourceHarvest popUpResourceHarvest;
    [SerializeField] public ActionsButtons returnMenu;
    [SerializeField] public ActionsButtons passDuringMove;
    [SerializeField] public ActionsButtons returnMenuHarvest;
    [SerializeField] public ActionsButtons Harvest;

    [SerializeField] GameObject resourcesNeededFolder;
    [SerializeField] GameObject resourcesNeeded;

    private void SetNeedViewers()
    {
        for (int i = 0; i < needViewers.Length; i++)
        {
            for (int j = 0; j < PlayerInput.Instance.cityPlayer.needs.Length; j++)
            {
                if (needViewers[i].needType == PlayerInput.Instance.cityPlayer.needs[j].needType)
                    needViewers[i].need = PlayerInput.Instance.cityPlayer.needs[j];
            }
        }
    }

    public void DrawInformationResourcesNeededAround(Need need)
    {
        for (int i =0; i < resourcesNeededFolder.transform.childCount; i++)
        {
            print("destroy");
            Destroy(resourcesNeededFolder.transform.GetChild(i).gameObject);
        }

        for (int j =0; j < PlayerInput.Instance.cityPlayer.needs.Length; j++)
        {
            if (PlayerInput.Instance.cityPlayer.needs[j].ResourceUsed == null)
            {
                return;
            }
        }
        CharaAvatar avatar = (CharaAvatar)GameObject.FindObjectOfType(typeof(CharaAvatar));

        List<CharaAvatar.ResourceConsume> rC = avatar.GetResourcesUsedPerRound();

        for (int i =0; i < rC.Count; i++)
        {
            if (rC[i].resourceType == GameManager.ResourceType.Berry)
            {
                ResourcesInfos rI = (Resources.Load("ResourcesInfos/Berry", typeof(ResourcesInfos)) as ResourcesInfos);
                print(rC[i].resourceType + "     " + rC[i].amountPerRound + "    " + rI.WonPerRound);
                int numberOfTileNeeded = (int)Mathf.Ceil(rC[i].amountPerRound / (rI.WonPerRound +DecretManager.Instance.totalDecreeInfos.collectQuantityBerry));
                if (numberOfTileNeeded > 0)
                {
                    GameObject uIResource = Instantiate(resourcesNeeded, resourcesNeededFolder.transform.position, resourcesNeededFolder.transform.rotation, resourcesNeededFolder.transform);
                    uIResource.GetComponent<Image>().sprite = (Resources.Load<Sprite>("UINeededResources/Berry"));
                    uIResource.transform.GetChild(0).GetComponent<Text>().text = numberOfTileNeeded.ToString();
                }
            }

            if (rC[i].resourceType == GameManager.ResourceType.Wood)
            {
                ResourcesInfos rI = (Resources.Load("ResourcesInfos/Wood", typeof(ResourcesInfos)) as ResourcesInfos);
                print(rC[i].resourceType + "     " + rC[i].amountPerRound + "    " + rI.WonPerRound);
                int numberOfTileNeeded = (int)Mathf.Ceil(rC[i].amountPerRound / (rI.WonPerRound + DecretManager.Instance.totalDecreeInfos.collectQuantityWood));
                if (numberOfTileNeeded > 0)
                {
                    GameObject uIResource = Instantiate(resourcesNeeded, resourcesNeededFolder.transform.position, resourcesNeededFolder.transform.rotation, resourcesNeededFolder.transform);
                    uIResource.GetComponent<Image>().sprite = (Resources.Load<Sprite>("UINeededResources/Wood"));
                    uIResource.transform.GetChild(0).GetComponent<Text>().text = numberOfTileNeeded.ToString();
                }
            }

            if (rC[i].resourceType == GameManager.ResourceType.Rock)
            {

                GameObject uIResource = Instantiate(resourcesNeeded, resourcesNeededFolder.transform.position, resourcesNeededFolder.transform.rotation, resourcesNeededFolder.transform);
                uIResource.GetComponent<Image>().sprite = (Resources.Load<Sprite>("UINeededResources/Rock"));
                uIResource.transform.GetChild(0).GetComponent<Text>().text = "X";
                uIResource.transform.GetChild(0).GetComponent<Text>().color = Color.red;

            }

            if (rC[i].resourceType == GameManager.ResourceType.Mouflu)
            {
                GameObject uIResource = Instantiate(resourcesNeeded, resourcesNeededFolder.transform.position, resourcesNeededFolder.transform.rotation, resourcesNeededFolder.transform);
                uIResource.GetComponent<Image>().sprite = (Resources.Load<Sprite>("UINeededResources/Rock"));
                uIResource.transform.GetChild(0).GetComponent<Text>().text = "X";
                uIResource.transform.GetChild(0).GetComponent<Text>().color = Color.red;
            }
        }
    }

    private void DrawWinPopUp()
    {
        winPopUp.SetActive(true);
    }
    private void DrawLoosePopUp()
    {
        loosePopUp.SetActive(true);
    }
    private void DrawEndLevelPopUp(bool win)
    {
        if (win)
        {
            DrawWinPopUp();
        }
        else
        {
            DrawLoosePopUp();
            SoundManager.Instance.GameOverSound();
        }
    }

    private void HideButtons()
    {
        for (int i = 0; i < actionsButtons.Length; i++)
        {
            actionsButtons[i].gameObject.SetActive(false);
        }
    }
    private void HideButtonsMoving()
    {
        for (int i = 0; i < actionsButtons.Length; i++)
        {
            actionsButtons[i].gameObject.SetActive(false);
        }
        returnMenu.gameObject.SetActive(true);
    }
    private void HideButtonsHarvest()
    {
        for (int i = 0; i < actionsButtons.Length; i++)
        {
            actionsButtons[i].gameObject.SetActive(false);
        }
        returnMenuHarvest.gameObject.SetActive(true);
        Harvest.gameObject.SetActive(true);

    }
    private void DrawButtons()
    {
        for (int i = 0; i < actionsButtons.Length; i++)
        {
            actionsButtons[i].gameObject.SetActive(true);
        }
        returnMenu.gameObject.SetActive(false);
        passDuringMove.gameObject.SetActive(false);
    }

    private void DrawPopUpResourceStockUsed()
    {
        for (int i = 0; i < needViewers.Length; i++)
        {
            Vector3 posInstant = needViewers[i].transform.position - needViewers[i].transform.up;
            GameObject popUpGo = Instantiate(popUpResourceStock.gameObject, posInstant, Quaternion.identity , transform) as GameObject;
            PopUpResourceStock popUp = popUpGo.GetComponent<PopUpResourceStock>();
            popUp.SetImage(needViewers[i].need.ResourceUsed.resourcesInfos);
            popUp.SetText(-(int)(needViewers[i].need.ResourceUsed.resourcesInfos.GetAmontUseFor(needViewers[i].need.needType) * needViewers[i].need.Multiplicator));
        }
    }

    private void DrawPopUpResourceHarvest()
    {
        for (int i = 0; i < needViewers.Length; i++)
        {
            Vector3 posInstant = needViewers[i].transform.position - needViewers[i].transform.up;
            GameObject popUpGo = Instantiate(popUpResourceStock.gameObject, posInstant, Quaternion.identity, transform) as GameObject;
            PopUpResourceStock popUp = popUpGo.GetComponent<PopUpResourceStock>();
            popUp.SetImage(needViewers[i].need.ResourceUsed.resourcesInfos);
            popUp.SetText(+(int)needViewers[i].need.ResourceUsed.resourcesInfos.GetAmontUseFor(needViewers[i].need.needType));
        }
    }

    private void AssignEvents()
    {
        RoundManager.LevelEnd += DrawEndLevelPopUp;
        RoundManager.RoundStart += DrawButtons;
        
        RoundManager.RoundEnd += DrawPopUpResourceStockUsed;
        ActionsButtons.Move += HideButtonsMoving;
        ActionsButtons.Pass += HideButtons;
        Need.ResourceUsedChange += DrawInformationResourcesNeededAround;
        ActionsButtons.PassDurigMove += HideButtons;
        ActionsButtons.Harvest += HideButtonsHarvest;
        ActionsButtons.Vote += HideButtons;
        ActionsButtons.ReturnMenu += DrawButtons;
    }

    private void UnassignEvents()
    {
        RoundManager.LevelEnd -= DrawEndLevelPopUp;
        RoundManager.RoundStart -= DrawButtons;
        RoundManager.RoundEnd -= DrawPopUpResourceStockUsed;
        ActionsButtons.Move -= HideButtonsMoving;
        Need.ResourceUsedChange -= DrawInformationResourcesNeededAround;
        ActionsButtons.Pass -= HideButtons;
        ActionsButtons.PassDurigMove -= HideButtons;
        ActionsButtons.Harvest -= HideButtonsHarvest;
        ActionsButtons.Vote -= HideButtons;
        ActionsButtons.ReturnMenu -= DrawButtons;

    }

    private void OnEnable()
    {
        AssignEvents();
        SetNeedViewers();
    }

    private void OnDisable()
    {
        UnassignEvents();
    }

    private void Awake()
    {
        SetCamera();
    }

    private void SetCamera() => GetComponent<Canvas>().worldCamera = Camera.main;
    

}

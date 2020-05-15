using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class UIManager : Singleton<UIManager>
{
    [HideInInspector] public NeedViewer needViewerSelected;
    public StockViewer stockViewer;
    [SerializeField] private GameObject winPopUp;
    [SerializeField] private GameObject loosePopUp;
    [SerializeField] ActionsButtons[] actionsButtons;
    [SerializeField] public NeedViewer[] needViewers;
    [SerializeField] PopUpResourceStock popUpResourceStock;
    [SerializeField] PopUpResourceHarvest popUpResourceHarvest;
    [SerializeField] ActionsButtons passDuringMove;

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
            DrawWinPopUp();
        else
            DrawLoosePopUp();
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
        passDuringMove.gameObject.SetActive(true);
    }
    private void DrawButtons()
    {
        for (int i = 0; i < actionsButtons.Length; i++)
        {
            actionsButtons[i].gameObject.SetActive(true);
        }
        passDuringMove.gameObject.SetActive(false);
    }

    private void SetImageResourceUsed(ResourcesInfos resourcesInfos)
    {
        needViewerSelected.SetImageResourceUsed(resourcesInfos.sprite);
    }

    private void SetTextResourceUsed(ResourcesInfos resourcesInfos)
    {
        needViewerSelected.SetImageResourceUsed(resourcesInfos.sprite);
    }

    private void DrawPopUpResourceStockUsed()
    {
        for (int i = 0; i < needViewers.Length; i++)
        {
            Vector3 posInstant = needViewers[i].transform.position - needViewers[i].transform.up;
            GameObject popUpGo = Instantiate(popUpResourceStock.gameObject, posInstant, Quaternion.identity , transform) as GameObject;
            PopUpResourceStock popUp = popUpGo.GetComponent<PopUpResourceStock>();
            popUp.SetImage(needViewers[i].need.resourceUsed.resourcesInfos);
            popUp.SetText(-(int)needViewers[i].need.resourceUsed.resourcesInfos.ReturnEnergyUseFor(needViewers[i].need.needType));
        }
    }

    private void DrawPopUpResourceHarvest()
    {
        for (int i = 0; i < needViewers.Length; i++)
        {
            Vector3 posInstant = needViewers[i].transform.position - needViewers[i].transform.up;
            GameObject popUpGo = Instantiate(popUpResourceStock.gameObject, posInstant, Quaternion.identity, transform) as GameObject;
            PopUpResourceStock popUp = popUpGo.GetComponent<PopUpResourceStock>();
            popUp.SetImage(needViewers[i].need.resourceUsed.resourcesInfos);
            popUp.SetText(+(int)needViewers[i].need.resourceUsed.resourcesInfos.ReturnEnergyUseFor(needViewers[i].need.needType));
        }
    }

    private void AssignEvents()
    {
        GameManager.LevelEnd += DrawEndLevelPopUp;
        GameManager.RoundStart += DrawButtons;
        GameManager.RoundEnd += DrawPopUpResourceStockUsed;

        ActionsButtons.Move += HideButtonsMoving;
        ActionsButtons.Pass += HideButtons;
        ActionsButtons.Harvest += HideButtons;
        ActionsButtons.Vote += HideButtons;

        ResourceViewer.ChangeResourceUsed += SetImageResourceUsed;
    }

    private void UnassignEvents()
    {
        GameManager.LevelEnd -= DrawEndLevelPopUp;
        GameManager.RoundStart -= DrawButtons;
        GameManager.RoundEnd -= DrawPopUpResourceStockUsed;

        ActionsButtons.Move -= HideButtonsMoving;
        ActionsButtons.Pass -= HideButtons;
        ActionsButtons.Harvest -= HideButtons;
        ActionsButtons.Vote -= HideButtons;

        ResourceViewer.ChangeResourceUsed -= SetImageResourceUsed;
    }

    private void OnEnable()
    {
        AssignEvents();
    }

    private void OnDestroy()
    {
        UnassignEvents();
    }
}

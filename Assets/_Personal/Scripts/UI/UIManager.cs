using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

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
    [SerializeField] ActionsButtons passDuringMove;

    private void SetNeedViewers()
    {

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

    private void DrawPopUpResourceStockUsed()
    {
        for (int i = 0; i < needViewers.Length; i++)
        {
            Vector3 posInstant = needViewers[i].transform.position - needViewers[i].transform.up;
            GameObject popUpGo = Instantiate(popUpResourceStock.gameObject, posInstant, Quaternion.identity , transform) as GameObject;
            PopUpResourceStock popUp = popUpGo.GetComponent<PopUpResourceStock>();
            popUp.SetImage(needViewers[i].need.ResourceUsed.resourcesInfos);
            popUp.SetText(-(int)(needViewers[i].need.ResourceUsed.resourcesInfos.GetAmontUseFor(needViewers[i].need.needType)* needViewers[i].need.multiplicator));
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
        ActionsButtons.PassDurigMove += HideButtons;
        ActionsButtons.Harvest += HideButtons;
        ActionsButtons.Vote += HideButtons;
    }

    private void UnassignEvents()
    {
        RoundManager.LevelEnd -= DrawEndLevelPopUp;
        RoundManager.RoundStart -= DrawButtons;
        RoundManager.RoundEnd -= DrawPopUpResourceStockUsed;

        ActionsButtons.Move -= HideButtonsMoving;
        ActionsButtons.Pass -= HideButtons;
        ActionsButtons.PassDurigMove -= HideButtons;
        ActionsButtons.Harvest -= HideButtons;
        ActionsButtons.Vote -= HideButtons;
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

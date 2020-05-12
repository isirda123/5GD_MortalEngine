using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    [HideInInspector] public NeedViewer needViewerSelected;
    public StockViewer stockViewer;
    [SerializeField] private GameObject winPopUp;
    [SerializeField] private GameObject loosePopUp;
    [SerializeField] ActionsButtons[] actionsButtons;

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
    private void DrawButtons()
    {
        for (int i = 0; i < actionsButtons.Length; i++)
        {
            actionsButtons[i].gameObject.SetActive(true);
        }
    }
    private void Start()
    {
        GameManager.LevelEnd += DrawEndLevelPopUp;
        ActionsButtons.Move += HideButtons;
        ActionsButtons.Pass += HideButtons;
        ActionsButtons.Harvest += HideButtons;
        ActionsButtons.Vote += HideButtons;
        CharaAvatar.MoveEnd += DrawButtons;
    }

    private void OnDestroy()
    {
        GameManager.LevelEnd -= DrawEndLevelPopUp;
        ActionsButtons.Move -= HideButtons;
        ActionsButtons.Pass -= HideButtons;
        ActionsButtons.Harvest -= HideButtons;
        ActionsButtons.Vote -= HideButtons;
        CharaAvatar.MoveEnd -= DrawButtons;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    [HideInInspector] public NeedViewer needViewerSelected;
    public StockViewer stockViewer;
    [SerializeField] private GameObject winPopUp;
    [SerializeField] private GameObject loosePopUp;

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
    private void Start()
    {
        GameManager.LevelEnd += DrawEndLevelPopUp;
    }
    private void OnDestroy()
    {
        GameManager.LevelEnd -= DrawEndLevelPopUp;
    }
}

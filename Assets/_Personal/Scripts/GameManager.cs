using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] public GameAssets gameAssets;
    [HideInInspector] public int levelId;
    public enum ResourceType
    {
        None,
        Wood,
        Mouflu,
        Berry,
        Rock
    }
    #region States
    public enum GameState
    {
        Playing,
        Score
    }
    private GameState gameState;
    public GameState State
    {
        get { return gameState; }
        set { gameState = value; }
    }
    #endregion

    private void CountDownLoadLevel(bool win)
    {
        Sequence sequence = DOTween.Sequence();
        sequence.AppendInterval(2f);
        sequence.OnComplete(() => LoadLevel(win, levelId));
    }

    private void LoadLevel(bool win, int levelId)
    {
        if (win)
        {
            levelId++;
            SceneManager.LoadScene(levelId);
        }
        else
        {
            SceneManager.LoadScene(levelId);
        }
    }
    private void Start()
    {
        RoundManager.LevelEnd += CountDownLoadLevel;
    }

    private void OnDisable()
    {
        RoundManager.LevelEnd -= CountDownLoadLevel;
    }
}

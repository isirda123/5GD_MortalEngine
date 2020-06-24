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


    void GoToNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    void ReloadSameLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    
    private void OnEnable()
    {
        ActionsButtons.ReloadTheSameLevel += ReloadSameLevel;
        ActionsButtons.GoToNextLevel += GoToNextLevel;
    }

    private void OnDisable()
    {
        ActionsButtons.ReloadTheSameLevel -= ReloadSameLevel;
        ActionsButtons.GoToNextLevel -= GoToNextLevel;

    }
}

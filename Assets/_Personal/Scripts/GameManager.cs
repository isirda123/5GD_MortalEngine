using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DG.Tweening;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] public GameAssets gameAssets;

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

}

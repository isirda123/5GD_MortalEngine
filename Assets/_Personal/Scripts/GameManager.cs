using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : Singleton<GameManager>
{
    public enum ResourceType
    {
        None,
        Wood,
        Mouflu,
        Berry,
        Rock
    }
    public RessourcesStartDatas ressourcesStartDatas;
    public ResourceInStock[] stock;
    public Need needSelected;
    public Need[] needs;
    public static event Action<bool> LevelEnd;
    public enum GameState
    {
        Playing,
        Score
    }
    private GameState gameState;
    public ResourceInStock ReturnResourceInStock(ResourceType resourceType)
    {
        ResourceInStock resourceInStockNeeded = null;
        for (int i = 0; i < stock.Length; i++)
        {
            if(resourceType == stock[i].resourceType)
            {
                resourceInStockNeeded = stock[i];
            }
        }
        return resourceInStockNeeded;
    }
    public void EndLevel(bool win)
    {
        if(gameState == GameState.Playing)
            LevelEnd?.Invoke(win);
    }
    private void SetStartingStock()
    {
        for (int i = 0; i < stock.Length; i++)
        {
            switch (stock[i].resourceType)
            {
                case GameManager.ResourceType.Mouflu:
                    stock[i].NumberInStock = ressourcesStartDatas.mouflu;
                    break;
                case GameManager.ResourceType.Berry:
                    stock[i].NumberInStock = ressourcesStartDatas.berry;
                    break;
                case GameManager.ResourceType.Wood:
                    stock[i].NumberInStock = ressourcesStartDatas.wood;
                    break;
                case GameManager.ResourceType.Rock:
                    stock[i].NumberInStock = ressourcesStartDatas.rock;
                    break;
            }
        }
    }

    private void Start()
    {
        SetStartingStock();
    }

}

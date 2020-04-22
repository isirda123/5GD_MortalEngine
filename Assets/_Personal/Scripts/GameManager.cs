using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public enum ResourceType
    {
        None,
        Wood,
        Chicken,
        Corn,
        Rock
    }
    public RessourcesStartDatas ressourcesStartDatas;
    public ResourceInStock[] stock;
    public Need needSelected;
    public Need[] needs;

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

    private void SetRessourcesAtStart()
    {
        for (int i = 0; i < stock.Length; i++)
        {
            switch (stock[i].resourceType)
            {
                case GameManager.ResourceType.Chicken:
                    stock[i].NumberInStock = ressourcesStartDatas.chicken;
                    break;
                case GameManager.ResourceType.Corn:
                    stock[i].NumberInStock = ressourcesStartDatas.corn;
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
        SetRessourcesAtStart();
    }



    private void Update()
    {
        
    }
}

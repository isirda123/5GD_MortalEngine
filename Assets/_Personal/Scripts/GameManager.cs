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
}

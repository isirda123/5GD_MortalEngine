using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StockViewer : MonoBehaviour
{
    [SerializeField] public ResourceViewer[] resourcesViewers;
    [SerializeField] private Stock stock;

    private void Update()
    {
        SetViwersText();
    }

    private void SetViwersText()
    {
        //set the number of each resources in the UI
        for (int i = 0; i < resourcesViewers.Length; i++)
        {
            for (int j = 0; j < stock.resourcesInStock.Length; j++)
            {
                if(stock.resourcesInStock[j].resourceType == resourcesViewers[i].resourceType)
                {
                    int nbr = (int)stock.resourcesInStock[j].numberInStock;
                    resourcesViewers[i].tmp.text = nbr.ToString();
                }
            }
        }
    }

    public void DesableStockViewer() => gameObject.SetActive(false);
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StockViewer : MonoBehaviour
{
    [SerializeField] public ResourceViewer[] resourcesViewers;

    private void Update()
    {
        SetViwersText();
    }

    private void SetViwersText()
    {
        ResourceInStock[] stock = GameManager.Instance.stock;
        //set the number of each resources in the UI
        for (int i = 0; i < resourcesViewers.Length; i++)
        {
            for (int j = 0; j < stock.Length; j++)
            {
                if(stock[j].resourceType == resourcesViewers[i].resourcesInfos.resourceType)
                {
                    int nbr = (int)Mathf.Round(stock[j].NumberInStock);
                    resourcesViewers[i].tmp.text = nbr.ToString();
                }
            }
        }
    }
    
    public void DesableStockViewer() => gameObject.SetActive(false);
}

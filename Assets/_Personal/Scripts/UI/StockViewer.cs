using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StockViewer : MonoBehaviour
{
    [SerializeField] public ResourceViewer[] resourcesViewers;
    [SerializeField] NeedViewer NeedViewer;
    public void DisableStockViewer()
    {
        NeedViewer.DrawStockViewer();
    }
}

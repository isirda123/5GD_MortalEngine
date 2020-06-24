using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class StockViewerEscape : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] NeedViewer NeedViewer;
    public void OnPointerDown(PointerEventData eventData)
    {
        print("Down");
        NeedViewer.DrawStockViewer();
    }
}

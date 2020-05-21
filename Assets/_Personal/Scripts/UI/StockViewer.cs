using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StockViewer : MonoBehaviour
{
    [SerializeField] public ResourceViewer[] resourcesViewers;

    public void DesableStockViewer() => gameObject.SetActive(false);
    private void Start()
    {
        gameObject.SetActive(false);
    }
}

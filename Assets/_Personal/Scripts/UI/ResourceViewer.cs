using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class ResourceViewer : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{
    public GameManager.ResourceType resourceType;
    public TextMeshProUGUI tmp;
    public Image background;
    [SerializeField] private Image resourceImage;

    public void OnPointerDown (PointerEventData eventData)
    {
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (CheckResourceType())
        {
            SetImageResourceUsed();
            UIManager.Instance.stockViewer.DesableStockViewer();
            SetResourceUsed();
        }
    }

    private void SetImageResourceUsed()
    {
        UIManager.Instance.needViewerSelected.SetImageResourceUsed(resourceImage.sprite);
    }
    private void SetResourceUsed()
    {
        GameManager.Instance.needSelected.resourceUsed = GameManager.Instance.ReturnResourceInStock(resourceType);
    }

    private bool CheckResourceType()
    {
        bool canIUseThisResource = false;
        for (int i = 0; i < UIManager.Instance.needViewerSelected.resourcesUsable.Length; i++)
        {
            if (resourceType == UIManager.Instance.needViewerSelected.resourcesUsable[i])
                canIUseThisResource = true;
        }
        return canIUseThisResource;
    }


}

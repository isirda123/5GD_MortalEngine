using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class ResourceViewer : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{
    public TextMeshProUGUI tmp;
    public Image background;
    public static event Action<ResourcesInfos> ChangeResourceUsed;
    public ResourcesInfos resourcesInfos;

    public void OnPointerDown (PointerEventData eventData)
    {
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (CheckResourceType())
        {
            ChangeResourceUsed?.Invoke(resourcesInfos);
            UIManager.Instance.stockViewer.DesableStockViewer();
        }
    }

    private bool CheckResourceType()
    {
        bool canIUseThisResource = false;
        for (int i = 0; i < UIManager.Instance.needViewerSelected.need.resourcesUsable.Length; i++)
        {
            if (resourcesInfos.resourceType == UIManager.Instance.needViewerSelected.need.resourcesUsable[i])
                canIUseThisResource = true;
        }
        return canIUseThisResource;
    }
}

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
    public ResourcesInfos resourcesInfos;

    public void OnPointerDown (PointerEventData eventData)
    {
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (CheckResourceType())
        {
            PlayerInput.Instance.ChangeNeedSelectedResourceUse(resourcesInfos);
            transform.parent.GetComponent<StockViewer>().DisableStockViewer();
        }
    }

    private bool CheckResourceType()
    {
        bool canIUseThisResource = false;
        for (int i = 0; i < UIManager.Instance.needViewerSelected.need.resourcesUsable.Length; i++)
        {
            if (resourcesInfos.resourceType == UIManager.Instance.needViewerSelected.need.resourcesUsable[i]
                && PlayerInput.Instance.cityPlayer.GetResourceInStock(resourcesInfos.resourceType).NumberInStock > 0)
                canIUseThisResource = true;
        }
        return canIUseThisResource;
    }

    private void SetViewerText(ResourceInStock resourcesInStock)
    {
        if (resourcesInfos.resourceType == resourcesInStock.resourcesInfos.resourceType)
        {
            int nbr = (int)Mathf.Round(resourcesInStock.NumberInStock);
            if (resourcesInStock.NumberInStock > 0)
                tmp.text = nbr.ToString();
            else
                tmp.text = "0";
        }
    }

    private void OnEnable()
    {
        ResourceInStock.ChangeStock += SetViewerText;
    }
    private void OnDestroy()
    {
        ResourceInStock.ChangeStock -= SetViewerText;
    }

}

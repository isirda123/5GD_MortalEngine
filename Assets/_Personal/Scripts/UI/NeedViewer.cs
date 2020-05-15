using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using System;

public class NeedViewer : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private StockViewer stockViewer;
    [SerializeField] private Image ressourceUsedImage;
    [SerializeField] private TextMeshProUGUI resourceUsedSotck;
    [SerializeField] private TextMeshProUGUI resourceUsedPerRound;

    public Need.NeedType needType;
    public Need need;

    public void SetResourceUsedText(int nbr)
    {
        resourceUsedSotck.text = nbr.ToString();
    }

    public void SetResourceUsedPerRoundText(int nbr)
    {
        resourceUsedPerRound.text = "- " + nbr.ToString();
    }

    public void SetImageResourceUsed(Sprite ressourceUsedImage)
    {
        this.ressourceUsedImage.sprite = ressourceUsedImage;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        DrawStockViewer();
        SetResourcesUsableViewer();
        SetNeedViewerSelected();
        SetNeedSelected();
    }

    private void DrawStockViewer() => stockViewer.gameObject.SetActive(true);

    private void SetResourcesUsableViewer()
    {
        for (int j = 0; j < stockViewer.resourcesViewers.Length; j++)
        {
            stockViewer.resourcesViewers[j].background.color = Color.red;
        }
        for (int i = 0; i < stockViewer.resourcesViewers.Length; i++)
        {
            for (int j = 0; j < need.resourcesUsable.Length; j++)
            {
                if (stockViewer.resourcesViewers[i].resourcesInfos.resourceType == need.resourcesUsable[j])
                {
                    stockViewer.resourcesViewers[i].background.color = Color.green;
                }
            }
        }
    }

    private void SetNeedViewerSelected() => UIManager.Instance.needViewerSelected = this;
    private void SetNeedSelected() => GameManager.Instance.needSelected = need;

}

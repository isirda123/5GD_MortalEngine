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
    [SerializeField] private Need.NeedType needType;
    [HideInInspector] public Need need;

    public void SetResourceUsedAmontText(Need need)
    {
        if (need == this.need)
        {
            resourceUsedSotck.text = ((int)(need.ResourceUsed.NumberInStock)).ToString();
        }
    }

    public void SetResourceUsedPerRoundText(Need need)
    {
        if (need == this.need)
        {
            resourceUsedPerRound.text = "- " + ((int)need.ResourceUsed.resourcesInfos.GetAmontUseFor(needType) * need.multiplicator).ToString();
        }
    }

    public void SetImageResourceUsed(Need need)
    {
        if (need == this.need)
        {
            ressourceUsedImage.sprite = need.ResourceUsed.resourcesInfos.sprite;
        }
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
                if (stockViewer.resourcesViewers[i].resourcesInfos.resourceType == need.resourcesUsable[j] 
                    && PlayerInput.Instance.cityPlayer.GetResourceInStock(stockViewer.resourcesViewers[i].resourcesInfos.resourceType).NumberInStock > 0)
                {
                    stockViewer.resourcesViewers[i].background.color = Color.green;
                }
            }
        }
    }

    private void SetNeedViewerSelected() => UIManager.Instance.needViewerSelected = this;
    private void SetNeedSelected() => PlayerInput.Instance.needSelected = need;

    private void SetNewResourceUsed(Need need)
    {
        SetImageResourceUsed(need);
        SetResourceUsedAmontText(need);
        SetResourceUsedPerRoundText(need);
    }

    private void OnEnable()
    {
        Need.ResourceUsedChange += SetNewResourceUsed;
        CharaAvatar.ResourceUsed += SetResourceUsedAmontText;
    }

    private void OnDisable()
    {
        Need.ResourceUsedChange -= SetNewResourceUsed;
        CharaAvatar.ResourceUsed -= SetResourceUsedAmontText;
    }
}

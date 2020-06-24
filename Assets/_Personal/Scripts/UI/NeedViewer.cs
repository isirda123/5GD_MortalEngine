using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using System;
using DG.Tweening;

public class NeedViewer : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private StockViewer stockViewer;
    [SerializeField] private Image ressourceUsedImage;
    [SerializeField] private TextMeshProUGUI resourceUsedStockText;
    [SerializeField] private TextMeshProUGUI resourceUsedPerRound;
    [SerializeField] public Need.NeedType needType;
    [HideInInspector] public Need need;

    bool showStockViewer = false;


    public void SetResourceUsedAmontText(Need need)
    {
        if (need == this.need)
        {
            //resourceUsedSotck.text = ((int)(need.ResourceUsed.NumberInStock)).ToString();
            float start = int.Parse(resourceUsedStockText.text);
            float end = (int)(need.ResourceUsed.NumberInStock);
            float pacing = start - end;

            float value = start;

            if (RoundManager.Instance.State == RoundManager.RoundState.ResolvingRound && need.resourceJustChanged == false)
            {
                print(start + "   " + end);
                DOTween.To(() => value, x => value = x, end, 2).OnUpdate(() => VisualDrawResources(value,pacing)).OnComplete(() => EndVisualDrawResources());
                print(value);
            }
            else
            {
                resourceUsedStockText.text = ((int)(need.ResourceUsed.NumberInStock)).ToString();
                need.resourceJustChanged = false;
            }
            //StartCoroutine(DrawVisualResourceAmount(start, end));
        }
    }

    private void VisualDrawResources(float value, float pacing)
    {
        resourceUsedStockText.text = ((int)value).ToString();
        if (pacing > 0)
        {
            resourceUsedStockText.fontSize = (float) ((-Mathf.PingPong(Time.time * pacing * 0.5f, 5)) + 25);
            Color color = Color.red;
            resourceUsedStockText.color = color;
        }
        else
        {
            resourceUsedStockText.fontSize = (float)((Mathf.PingPong(Time.time * pacing * 0.5f, 5)) + 25);
            Color color = Color.green;
            resourceUsedStockText.color = color;
        }
    }

    private void EndVisualDrawResources()
    {
        resourceUsedStockText.color = Color.black;
        resourceUsedStockText.fontSize = 25;
    }

    public void SetResourceUsedPerRoundText(Need need)
    {
        if (need == this.need)
        {
            resourceUsedPerRound.text = "- " + ((int)need.ResourceUsed.resourcesInfos.GetAmontUseFor(needType) * need.Multiplicator).ToString();
        }
    }

    public void SetResourceUsedPerRoundText()
    {
        resourceUsedPerRound.text = "- " + ((int)need.ResourceUsed.resourcesInfos.GetAmontUseFor(needType) * need.Multiplicator).ToString();
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

    
    public void DrawStockViewer()
    {
        print(gameObject.name);
        showStockViewer = !showStockViewer;
        StartCoroutine(SetViewerPos());
    }

    IEnumerator SetViewerPos()
    {
        stockViewer.gameObject.SetActive(true);
        if (showStockViewer == true)
        {
            while (stockViewer.transform.localPosition.y > 70)
            {
                if (showStockViewer == false)
                {
                    break;
                }
                stockViewer.transform.localPosition = new Vector3(stockViewer.transform.localPosition.x, stockViewer.transform.localPosition.y - 4f, stockViewer.transform.localPosition.z);
                yield return new WaitForSeconds(0.01f);
            }
        }
        else
        {
            while (stockViewer.transform.localPosition.y < 315)
            {
                if (showStockViewer == true)
                {
                    break;
                }
                stockViewer.transform.localPosition = new Vector3(stockViewer.transform.localPosition.x, stockViewer.transform.localPosition.y + 4f, stockViewer.transform.localPosition.z);
                yield return new WaitForSeconds(0.01f);
            }
        }
    }

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
        DecretManager.DecretSelected += SetResourceUsedPerRoundText;
    }

    private void OnDisable()
    {
        Need.ResourceUsedChange -= SetNewResourceUsed;
        CharaAvatar.ResourceUsed -= SetResourceUsedAmontText;
        DecretManager.DecretSelected -= SetResourceUsedPerRoundText;
    }
}

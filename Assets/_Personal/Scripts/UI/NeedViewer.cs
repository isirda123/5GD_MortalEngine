using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class NeedViewer : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] public GameManager.ResourceType[] resourcesUsable;
    [SerializeField] public GameManager.ResourceType resourceUsed;
    [SerializeField] private StockViewer stockViewer;
    [SerializeField] private Image ressourceUsedImage;
    [SerializeField] private TextMeshProUGUI resourceUsed_tmp;

    private void SetResourceUsedText()
    {
        for (int i = 0; i < GameManager.Instance.stock.Length; i++)
        {
            if(resourceUsed == GameManager.Instance.stock[i].resourceType)
            {
                int nbr = (int)GameManager.Instance.stock[i].numberInStock;
                resourceUsed_tmp.text = nbr.ToString();
            }
        }
    }

    public void SetResourceUsed(GameManager.ResourceType resourceUsed, Sprite ressourceUsedImage)
    {
        this.resourceUsed = resourceUsed;
        this.ressourceUsedImage.sprite = ressourceUsedImage;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        DrawStockViewer();
        ResetResourcesUSableViewer();
        SetResourcesUsableViewer();
        SetNeedSelected();
    }

    private void DrawStockViewer() => stockViewer.gameObject.SetActive(true);

    private void SetResourcesUsableViewer()
    {
        for (int i = 0; i < stockViewer.resourcesViewers.Length; i++)
        {
            for (int j = 0; j < resourcesUsable.Length; j++)
            {
                if (stockViewer.resourcesViewers[i].resourceType == resourcesUsable[j])
                {
                    stockViewer.resourcesViewers[i].background.color = Color.green;
                }
            }
        }
    }

    private void ResetResourcesUSableViewer()
    {
        for (int j = 0; j < stockViewer.resourcesViewers.Length; j++)
        {
            stockViewer.resourcesViewers[j].background.color = Color.red;
        }
    }

    private void SetNeedSelected() => UIManager.Instance.needSelected = this;



    private void Update()
    {
        SetResourceUsedText();
    }
}

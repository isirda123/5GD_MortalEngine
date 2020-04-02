using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ResourceViewer : MonoBehaviour, IPointerDownHandler
{
    public GameManager.ResourceType resourceType;
    public TextMeshProUGUI tmp;
    public Image background;
    [SerializeField] private Image resourceImage;


    public void OnPointerDown(PointerEventData eventData)
    {
        if (CheckResourceType())
        {
            SetResourceUsed();
            UIManager.Instance.stockViewer.DesableStockViewer();
        }
    }

    private void SetResourceUsed() => UIManager.Instance.needSelected.SetResourceUsed(resourceType,resourceImage.sprite);

    private bool CheckResourceType()
    {
        bool canIUseThisResource = false;
        for (int i = 0; i < UIManager.Instance.needSelected.resourcesUsable.Length; i++)
        {
            if (resourceType == UIManager.Instance.needSelected.resourcesUsable[i])
                canIUseThisResource = true;
        }
        return canIUseThisResource;
    }


}

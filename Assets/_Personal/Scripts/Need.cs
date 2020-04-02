using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Need : MonoBehaviour
{
    public NeedViewer needViewer;
    public NeedsDatas needDatas;
    public enum NeedType
    {
        Houses,
        Food,
        Energy
    }
    public NeedType needType;
    [SerializeField] public ResourceInStock resourceUsed;

    private void UseResources()
    {
        switch (resourceUsed.resourceType)
        {
            case GameManager.ResourceType.Chicken:
                GameManager.Instance.ReturnResourceInStock(resourceUsed.resourceType).numberInStock -= needDatas.chickenPerSecond * Time.deltaTime;
                break;
            case GameManager.ResourceType.Wood:
                GameManager.Instance.ReturnResourceInStock(resourceUsed.resourceType).numberInStock -= needDatas.woodPerSecond * Time.deltaTime;
                break;
            case GameManager.ResourceType.Rock:
                GameManager.Instance.ReturnResourceInStock(resourceUsed.resourceType).numberInStock -= needDatas.rockPerSecond * Time.deltaTime;
                break;
            case GameManager.ResourceType.Corn:
                GameManager.Instance.ReturnResourceInStock(resourceUsed.resourceType).numberInStock -= needDatas.cornPerSecond * Time.deltaTime;
                break;
        }
        needViewer.SetResourceUsedText((int)GameManager.Instance.ReturnResourceInStock(resourceUsed.resourceType).numberInStock);
    }

    private void SetRessourceSelected()
    {

    }

    private void Update()
    {
        UseResources();
    }
}

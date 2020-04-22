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
                GameManager.Instance.ReturnResourceInStock(resourceUsed.resourceType).NumberInStock -= needDatas.chickenPerMinute * Time.deltaTime / 60f;
                break;
            case GameManager.ResourceType.Wood:
                GameManager.Instance.ReturnResourceInStock(resourceUsed.resourceType).NumberInStock -= needDatas.woodPerMinute * Time.deltaTime / 60f;
                break;
            case GameManager.ResourceType.Rock:
                GameManager.Instance.ReturnResourceInStock(resourceUsed.resourceType).NumberInStock -= needDatas.rockPerMinute * Time.deltaTime / 60f;
                break;
            case GameManager.ResourceType.Corn:
                GameManager.Instance.ReturnResourceInStock(resourceUsed.resourceType).NumberInStock -= needDatas.cornPerMinute * Time.deltaTime / 60f;
                break;
        }
        needViewer.SetResourceUsedText((int)Mathf.Round(GameManager.Instance.ReturnResourceInStock(resourceUsed.resourceType).NumberInStock));
    }

    private void SetRessourceSelected()
    {

    }

    private void Update()
    {
        UseResources();
    }
}

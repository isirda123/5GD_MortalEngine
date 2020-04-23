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
    [SerializeField] public GameManager.ResourceType[] resourcesUsable;

    private void UseResources()
    {
        switch (resourceUsed.resourceType)
        {
            case GameManager.ResourceType.Mouflu:
                GameManager.Instance.ReturnResourceInStock(resourceUsed.resourceType).NumberInStock -= needDatas.chickenPerMinute * Time.deltaTime / 60f;
                break;
            case GameManager.ResourceType.Wood:
                GameManager.Instance.ReturnResourceInStock(resourceUsed.resourceType).NumberInStock -= needDatas.woodPerMinute * Time.deltaTime / 60f;
                break;
            case GameManager.ResourceType.Rock:
                GameManager.Instance.ReturnResourceInStock(resourceUsed.resourceType).NumberInStock -= needDatas.rockPerMinute * Time.deltaTime / 60f;
                break;
            case GameManager.ResourceType.Berry:
                GameManager.Instance.ReturnResourceInStock(resourceUsed.resourceType).NumberInStock -= needDatas.berryPerMinute * Time.deltaTime / 60f;
                break;
        }
        print(Mathf.Round(GameManager.Instance.ReturnResourceInStock(resourceUsed.resourceType).NumberInStock));
        needViewer.SetResourceUsedText((int)Mathf.Round(GameManager.Instance.ReturnResourceInStock(resourceUsed.resourceType).NumberInStock));
    }

    private void Update()
    {
        UseResources();
    }

    private void ChangeUsingRessource(GameManager.ResourceType currentRessource)
    {
        if (currentRessource == resourceUsed.resourceType)
        {
            for (int i = 0; i < resourcesUsable.Length; i++)
            {
                if (resourcesUsable[i] != currentRessource)
                {
                    ResourceInStock otherResourceUsable = GameManager.Instance.ReturnResourceInStock(resourcesUsable[i]);
                    if (otherResourceUsable.NumberInStock > 0)
                    {
                        print("Resource Changed");
                        resourceUsed = otherResourceUsable;
                        needViewer.SetImageResourceUsed(resourceUsed.resourceImage);
                    }
                    else
                    {
                        //GameManager.LevelEnd?.Invoke(false);
                        print("LOOOOOOOOOOOOOOOOOSE");
                    }
                }
            }
        }
    }

    private void Start()
    {
        ResourceInStock.ResourceEmpty += ChangeUsingRessource;
    }
    private void OnDestroy()
    {
        ResourceInStock.ResourceEmpty -= ChangeUsingRessource;
    }
}

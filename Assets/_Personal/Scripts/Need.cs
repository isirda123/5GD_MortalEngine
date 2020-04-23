using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Need : MonoBehaviour
{
    public NeedViewer needViewer;
    public enum NeedType
    {
        Build,
        Food,
        Energy
    }
    public NeedType needType;
    [SerializeField] public ResourceInStock resourceUsed;
    [SerializeField] public GameManager.ResourceType[] resourcesUsable;
    [SerializeField] public ResourcesInfos[] resourceForValidation;
    public int TilesNeeded
    {
        get
        {
            float wastOfEnergyUsed = 0;
            switch (needType)
            {
                case NeedType.Energy:
                    wastOfEnergyUsed = resourceUsed.resourcesInfos.wastForEnergyPerMinute;
                    break;
                case NeedType.Food:
                    wastOfEnergyUsed = resourceUsed.resourcesInfos.wastForFoodPerMinute;
                    break;
                case NeedType.Build:
                    wastOfEnergyUsed = resourceUsed.resourcesInfos.wastForBuildPerMinute;
                    break;
            }
            return Mathf.CeilToInt(resourceUsed.resourcesInfos.ResourcesPerMinute / wastOfEnergyUsed);
        }
    }

    private void UseResources()
    {
        switch (needType)
        {
            case NeedType.Energy:
                GameManager.Instance.ReturnResourceInStock(resourceUsed.resourceType).NumberInStock 
                    -= resourceUsed.resourcesInfos.wastForEnergyPerMinute * Time.deltaTime / 60f;
                break;
            case NeedType.Food:
                GameManager.Instance.ReturnResourceInStock(resourceUsed.resourceType).NumberInStock 
                    -= resourceUsed.resourcesInfos.wastForFoodPerMinute * Time.deltaTime / 60f;
                break;
            case NeedType.Build:
                GameManager.Instance.ReturnResourceInStock(resourceUsed.resourceType).NumberInStock 
                    -= resourceUsed.resourcesInfos.wastForBuildPerMinute * Time.deltaTime / 60f;
                break;
        }
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
                        GameManager.Instance.EndLevel(false);
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

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
    [HideInInspector] public float multiplicateur;

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
        GameManager.Instance.GetResourceInStock(resourceUsed.resourceType).NumberInStock
                    -= resourceUsed.resourcesInfos.ReturnEnergyUseFor(needType) * Time.deltaTime / 60f * multiplicateur;
        needViewer.SetResourceUsedText((int)Mathf.Round(GameManager.Instance.GetResourceInStock(resourceUsed.resourceType).NumberInStock));
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
                    ResourceInStock otherResourceUsable = GameManager.Instance.GetResourceInStock(resourcesUsable[i]);
                    if (otherResourceUsable.NumberInStock > 0)
                    {
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

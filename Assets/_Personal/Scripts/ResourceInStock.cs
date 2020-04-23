using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
public class ResourceInStock : MonoBehaviour
{
    public GameManager.ResourceType resourceType;
    private float numberInStock;
    public static event Action<GameManager.ResourceType> ResourceEmpty;
    public Sprite resourceImage;
    public float NumberInStock {
        get { return numberInStock; }
        set {
            if (value <= 0)
                ResourceEmpty?.Invoke(resourceType);
            numberInStock = Mathf.Clamp(value, 0, maxStock);
        }
    }
    [SerializeField] int maxStock;
}

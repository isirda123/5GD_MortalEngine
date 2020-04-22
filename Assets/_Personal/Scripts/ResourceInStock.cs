using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceInStock : MonoBehaviour
{
    public GameManager.ResourceType resourceType;
    private float numberInStock;
    public float NumberInStock { get { return numberInStock; } set { numberInStock = Mathf.Clamp(value, 0, maxStock); } }
    [SerializeField] int maxStock;
}

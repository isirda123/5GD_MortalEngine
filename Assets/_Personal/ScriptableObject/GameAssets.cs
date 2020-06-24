using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewGameDatas", menuName = "ScriptableObjects/NewGameDatas")]
public class GameAssets : ScriptableObject
{
    public PopUpResourceHarvest popUpResourceHarvest;
    public PopUpResourceStock popUpResourceStock;
    public GameObject arrow;

    public Material border;
    public Material center;

    [SerializeField] public GameObject[] visualForNoneTile;
    [SerializeField] public GameObject[] visualForWaterTile;
}

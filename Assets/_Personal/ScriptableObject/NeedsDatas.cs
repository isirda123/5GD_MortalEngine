using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewNeedData", menuName = "ScriptableObjects/NeedDatas")]
public class NeedsDatas : ScriptableObject
{
    public float woodPerSecond;
    public float rockPerSecond;
    public float chickenPerSecond;
    public float cornPerSecond;
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewNeedData", menuName = "ScriptableObjects/NeedDatas")]
public class NeedsDatas : ScriptableObject
{
    public float woodPerMinute;
    public float chickenPerMinute;
    public float berryPerMinute;
    public float rockPerMinute;
}

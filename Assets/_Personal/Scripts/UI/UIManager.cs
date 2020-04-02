using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    [HideInInspector] public NeedViewer needSelected;
    public StockViewer stockViewer;
}

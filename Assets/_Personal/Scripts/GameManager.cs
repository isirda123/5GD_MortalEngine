using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public enum ResourceType
    {
        None,
        Wood,
        Chicken,
        Corn,
        Rock
    }

    public Stock stock;
    public NeedViewer[] needs;
}

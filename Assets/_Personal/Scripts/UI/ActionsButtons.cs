using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class ActionsButtons : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{
    [SerializeField] PlayerAction playerAction;
    public enum PlayerAction
    {
        Move,
        Harvest,
        Pass,
        Vote,
        PassDurigMove
    }
    public static event Action Move;
    public static event Action Pass;
    public static event Action Harvest;
    public static event Action Vote;
    public static event Action PassDurigMove;

    public void OnPointerDown(PointerEventData eventData)
    {

    }

    public void OnPointerUp(PointerEventData eventData)
    {
        switch (playerAction)
        {
            case PlayerAction.Move:
                Move?.Invoke();
                break;
            case PlayerAction.Harvest:
                Harvest?.Invoke();
                break;
            case PlayerAction.Pass:
                Pass?.Invoke();
                break;
            case PlayerAction.Vote:
                Vote?.Invoke();
                break;
            case PlayerAction.PassDurigMove:
                PassDurigMove?.Invoke();
                break;
        }
    }

}

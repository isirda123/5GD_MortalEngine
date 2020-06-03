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
        PassDurigMove,
        ReturnMenu
    }
    public static event Action Move;
    public static event Action Pass;
    public static event Action Harvest;
    public static event Action Vote;
    public static event Action PassDurigMove;
    public static event Action ReturnMenu;

    public void OnPointerDown(PointerEventData eventData)
    {

    }

    public void OnPointerUp(PointerEventData eventData)
    {
        switch (playerAction)
        {
            case PlayerAction.Move:
                if (PlayerInput.Instance.cityPlayer.State == CharaAvatar.CharacterState.WaitForAction)
                    Move?.Invoke();
                break;
            case PlayerAction.Harvest:
                if (PlayerInput.Instance.cityPlayer.State == CharaAvatar.CharacterState.WaitForAction)
                    Harvest?.Invoke();
                break;
            case PlayerAction.Pass:
                if (PlayerInput.Instance.cityPlayer.State == CharaAvatar.CharacterState.WaitForAction)
                    Pass?.Invoke();
                break;
            case PlayerAction.Vote:
                if (PlayerInput.Instance.cityPlayer.State == CharaAvatar.CharacterState.WaitForAction)
                    Vote?.Invoke();
                break;
            case PlayerAction.PassDurigMove:
                if (PlayerInput.Instance.cityPlayer.State == CharaAvatar.CharacterState.WaitForMoving)
                    PassDurigMove?.Invoke();
                break;
            case PlayerAction.ReturnMenu:
                ReturnMenu?.Invoke();
                break;
        }
    }
}

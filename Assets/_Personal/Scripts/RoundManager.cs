using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DG.Tweening;
public class RoundManager : Singleton<RoundManager>
{
    [HideInInspector] public int numberOfRound;

    #region EVENTS
    public static event Action<bool> LevelEnd;
    public static event Action RoundEnd;
    public static event Action RoundStart;

    private void AssignEvents()
    {
        ActionsButtons.Move += SetMakingAction;
        ActionsButtons.Pass += LaunchEndRound;
        ActionsButtons.Harvest += SetMakingAction;
        RoundEnd += SetRoundStateResolving;
        RoundEnd += AddRound;
    }

    private void UnassignEvents()
    {
        ActionsButtons.Move -= SetMakingAction;
        ActionsButtons.Pass -= LaunchEndRound;
        ActionsButtons.Harvest -= SetMakingAction;
        RoundEnd -= SetRoundStateResolving;
        RoundEnd -= AddRound;
    }

    private void OnEnable()
    {
        AssignEvents();
    }

    private void OnDestroy()
    {
        UnassignEvents();
    }

    #endregion

    #region STATES
    public enum RoundState
    {
        ChoosingAction,
        MakingAction,
        ResolvingRound
    }

    private RoundState roundState;

    private void SwitchRoundState(RoundState roundStateFocused)
    {
        switch (roundStateFocused)
        {
            case RoundState.ChoosingAction:
                break;
            case RoundState.MakingAction:
                break;
            case RoundState.ResolvingRound:
                break;
        }
    }
    #endregion

    #region METHODS
    private void SetMakingAction()
    {
        SwitchRoundState(RoundState.MakingAction);
    }

    private void SetRoundStateResolving()
    {
        SwitchRoundState(RoundState.ResolvingRound);
    }
    private void SetChoosingForAction()
    {
        SwitchRoundState(RoundState.ChoosingAction);
    }

    public void EndLevel(bool win)
    {
        if (GameManager.Instance.State == GameManager.GameState.Playing)
            LevelEnd?.Invoke(win);
    }

    public void LaunchEndRound()
    {
        RoundEnd?.Invoke();
        Sequence sequence = DOTween.Sequence();
        sequence.AppendInterval(2);
        sequence.OnComplete(() => RoundStart?.Invoke());
    }

    public void LunchStartRound()
    {
        RoundStart?.Invoke();
    }

    private void AddRound()
    {
        numberOfRound += 1;
    }
    #endregion
}

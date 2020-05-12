using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : Singleton<GameManager>
{
    public enum ResourceType
    {
        None,
        Wood,
        Mouflu,
        Berry,
        Rock
    }
    public ResourcesNeedsStartDatas ressourcesStartDatas;
    public ResourceInStock[] stock;
    [HideInInspector] public Need needSelected;
    public Need[] needs;
    public static event Action<bool> LevelEnd;
    public struct ResourceUsage
    {
        public ResourceInStock resourceInStock;
        public float lifeTime;
        public float resourceUsedPerMinute;
    }

    public enum GameState
    {
        Playing,
        Score
    }

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

    private GameState gameState;
    public ResourceInStock GetResourceInStock(ResourceType resourceType)
    {
        ResourceInStock resourceInStockNeeded = null;
        for (int i = 0; i < stock.Length; i++)
        {
            if(resourceType == stock[i].resourceType)
            {
                resourceInStockNeeded = stock[i];
            }
        }
        return resourceInStockNeeded;
    }
    public void EndLevel(bool win)
    {
        if(gameState == GameState.Playing)
            LevelEnd?.Invoke(win);
    }
    private void SetStartingStock()
    {
        for (int i = 0; i < stock.Length; i++)
        {
            switch (stock[i].resourceType)
            {
                case ResourceType.Mouflu:
                    stock[i].NumberInStock = ressourcesStartDatas.mouflu;
                    break;
                case ResourceType.Berry:
                    stock[i].NumberInStock = ressourcesStartDatas.berry;
                    break;
                case ResourceType.Wood:
                    stock[i].NumberInStock = ressourcesStartDatas.wood;
                    break;
                case ResourceType.Rock:
                    stock[i].NumberInStock = ressourcesStartDatas.rock;
                    break;
            }
        }
    }

    private void SetNeedsMultiplicateur()
    {
        for (int i = 0; i < needs.Length; i++)
        {
            switch (needs[i].needType)
            {
                case Need.NeedType.Build:
                    needs[i].multiplicateur = ressourcesStartDatas.needBuildsStart;
                    break;
                case Need.NeedType.Energy:
                    needs[i].multiplicateur = ressourcesStartDatas.needEnergyStart;
                    break;
                case Need.NeedType.Food:
                    needs[i].multiplicateur = ressourcesStartDatas.needFoodStart;
                    break;
            }
            //desable viwer if no need
            if (needs[i].multiplicateur == 0)
                needs[i].needViewer.gameObject.SetActive(false);
        }
    }

    public IEnumerator RespawnOfRessources(float timeToRespawn, GameObject objectToRespawn)
    {
        yield return new WaitForSeconds(timeToRespawn);
        objectToRespawn.SetActive(true);
    }

    private void SetMakingAction()
    {
        SwitchRoundState(RoundState.MakingAction);
    }
    private void SetResolveRound()
    {
        SwitchRoundState(RoundState.ResolvingRound);
    }
    private void SetChoosingForAction()
    {
        SwitchRoundState(RoundState.ChoosingAction);
    }

    private void Start()
    {
        SetStartingStock();
        SetNeedsMultiplicateur();
        ActionsButtons.Move += SetMakingAction;
        CharaAvatar.MoveEnd += SetResolveRound;
    }

    private void OnDestroy()
    {
        ActionsButtons.Move -= SetMakingAction;
        CharaAvatar.MoveEnd -= SetResolveRound;
    }
}

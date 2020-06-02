using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Linq;
using DG.Tweening;
using System;

public class CharaAvatar : MonoBehaviour
{
    #region MOUVEMENT
    private Sequence mouvementSequence;
    [SerializeField] private float mouvementAnimationSpeed;
    [SerializeField] private int mouvementRange;
    private Tile tileSelectedForMove = null;
    private int mouvementRemain;
    #endregion

    #region STATE MACHINE
    //CHARACTER STATE MACHINE
    public enum CharacterState
    {
        Moving,
        Mining,
        WaitForMoving,
        WaitForAction
    }
    private CharacterState actualState;
    public CharacterState State
    { get { return actualState; } set { SwitchState(value); } }
    private void SwitchState(CharacterState focusState)
    {
        switch (focusState)
        {
            case CharacterState.WaitForAction:
                actualState = CharacterState.WaitForAction;
                break;
            case CharacterState.WaitForMoving:
                TilesManager.Instance.DrawMoveRange(mouvementRemain);
                actualState = CharacterState.WaitForMoving;
                break;
            case CharacterState.Moving:
                actualState = CharacterState.Moving;
                break;
            case CharacterState.Mining:
                break;
        }
    }
    #endregion

    #region EVENTS
    public static event Action<Need> ResourceUsed;
    private void AssignEvents()
    {
        Tile.TileTouched += Move;
        ActionsButtons.Move += SetWaitForMoving;
        ActionsButtons.Harvest += HarvestTilesAround;
        ActionsButtons.PassDurigMove += UseAllMovement;
        RoundManager.RoundEnd += UseResourcesInStock;
        RoundManager.RoundEnd += TilesManager.Instance.SpawnResourcesEndOfTurn;
        ResourceInStock.ResourceEmpty += ChangeUsingRessource;
    }

    private void UnassignEvents()
    {
        Tile.TileTouched -= Move;
        ActionsButtons.Move -= SetWaitForMoving;
        ActionsButtons.Harvest -= HarvestTilesAround;
        ActionsButtons.PassDurigMove -= UseAllMovement;
        RoundManager.RoundEnd -= UseResourcesInStock;
        RoundManager.RoundEnd -= TilesManager.Instance.SpawnResourcesEndOfTurn;
        ResourceInStock.ResourceEmpty -= ChangeUsingRessource;
    }

    private void OnEnable()
    {
        AssignEvents();
    }
    private void OnDesable()
    {
        UnassignEvents();
    }

    #endregion

    #region START SETTINGS
    void Start()
    {
        SetStartingValues();
        SetMaxMouvementRemain();
        GetTileUnder().avatarOnMe = true;
    }

    private void SetStartingValues()
    {
        SetStartingStock();
        SetStartNeedsMultiplicateur();
        SetNeeds();
    }
    private void SetStartingStock()
    {
        for (int i = 0; i < stock.Length; i++)
        {
            switch (stock[i].resourcesInfos.resourceType)
            {
                case GameManager.ResourceType.Mouflu:
                    stock[i].NumberInStock = resourcesNeedsStartDatas.mouflu;
                    break;
                case GameManager.ResourceType.Berry:
                    stock[i].NumberInStock = resourcesNeedsStartDatas.berry;
                    break;
                case GameManager.ResourceType.Wood:
                    stock[i].NumberInStock = resourcesNeedsStartDatas.wood;
                    break;
                case GameManager.ResourceType.Rock:
                    stock[i].NumberInStock = resourcesNeedsStartDatas.rock;
                    break;
            }
        }
    }
    private void SetStartNeedsMultiplicateur()
    {
        for (int i = 0; i < needs.Length; i++)
        {
            switch (needs[i].needType)
            {
                case Need.NeedType.Build:
                    needs[i].multiplicator = resourcesNeedsStartDatas.needBuildsStart;
                    break;
                case Need.NeedType.Energy:
                    needs[i].multiplicator = resourcesNeedsStartDatas.needEnergyStart;
                    break;
                case Need.NeedType.Food:
                    needs[i].multiplicator = resourcesNeedsStartDatas.needFoodStart;
                    break;
            }
        }
    }
    private void SetNeeds()
    {
        for (int i = 0; i < needs.Length; i++)
        {
            ResourceInStock firstResourceUsable = null;
            for (int j = 0; j < needs[i].resourcesUsable.Length; j++)
            {
                ResourceInStock resourceInStock = GetResourceInStock(needs[i].resourcesUsable[j]);
                if (resourceInStock.NumberInStock > 0)
                    firstResourceUsable = resourceInStock;
            }
            needs[i].ResourceUsed = firstResourceUsable;
        }
    }
    #endregion

    [SerializeField] private GameObject workZone;
    [SerializeField] private Need[] needs;
    [SerializeField] public ResourceInStock[] stock;
    [SerializeField] ResourcesNeedsStartDatas resourcesNeedsStartDatas;

    private Tile resourceFocused;


    private void SetResourceUsed(ResourcesInfos resourceToUseInfos,Need need)
    {
        ResourceInStock resourceInStock = GetResourceInStock(resourceToUseInfos.resourceType);
        PlayerInput.Instance.needSelected.ResourceUsed = resourceInStock;
    }

    private void SetWaitForMoving()
    {
        State = CharacterState.WaitForMoving;
    }

    private void SetMaxMouvementRemain() => mouvementRemain = mouvementRange;

    private void HarvestTilesAround()
    {
        List<Tile> tiles = GetResourcesAround(GetTileUnder().neighbours);
        for (int i = 0; i < tiles.Count; i++)
        {
            SetResourceInStock(tiles[i]);
            tiles[i].tileType = Tile.TypeOfTile.None;
            tiles[i].SetTypeOfTile();
        }
        Sequence sequence = DOTween.Sequence();
        sequence.AppendInterval(2);
        sequence.OnComplete(()=> RoundManager.Instance.LaunchEndRound());
    }

    public ResourceInStock GetResourceInStock(GameManager.ResourceType resourceType)
    {
        ResourceInStock resourceInStockNeeded = null;
        for (int i = 0; i < stock.Length; i++)
        {
            if (resourceType == stock[i].resourcesInfos.resourceType)
            {
                resourceInStockNeeded = stock[i];
            }
        }
        return resourceInStockNeeded;
    }

    private void ChangeUsingRessource(GameManager.ResourceType emptyResource)
    {
        for (int i = 0; i < needs.Length; i++)
        {
            if (needs[i].ResourceUsed == null)
            {
                ResourceInStock firstResourceUsable = GetResourceInStock(needs[i].resourcesUsable[0]);
                needs[i].ResourceUsed = firstResourceUsable;
            }
            else
            {
                if (emptyResource == needs[i].ResourceUsed.resourcesInfos.resourceType)
                {
                    for (int j = 0; j < needs[i].resourcesUsable.Length; j++)
                    {
                        if (needs[i].resourcesUsable[j] != emptyResource)
                        {
                            ResourceInStock otherResourceUsable = GetResourceInStock(needs[i].resourcesUsable[j]);
                            if (otherResourceUsable.NumberInStock > 0)
                            {
                                needs[i].ResourceUsed = otherResourceUsable;
                            }
                            else
                            {
                                if (needs[i].multiplicator > 0)
                                    RoundManager.Instance.EndLevel(false);
                            }
                        }
                    }
                }
            }
        }
    }

    private void UseResourcesInStock()
    {
        print("resource use");
        for (int i = 0; i < needs.Length; i++)
            needs[i].UseResources();
        for (int i = 0; i < needs.Length; i++)
            ResourceUsed?.Invoke(needs[i]);
    }

    private List<Tile> GetResourcesAround(List<Tile> neighbours)
    {
        List<Tile> resourcesInRange = new List<Tile>();
        for (int i = 0; i < neighbours.Count; i++)
        {
            if (neighbours[i].resourcesInfos != null)
            {
                resourcesInRange.Add(neighbours[i]);
            }
        }
        return resourcesInRange;
    }

    private void Move(Tile tileHit)
    {
        workZone.SetActive(false);
        if (State != CharacterState.WaitForMoving)
        {
            return;
        }
        if (tileHit == tileSelectedForMove)
        {
            tileSelectedForMove = null;
            TilesManager.Instance.SetNormalColorOfTiles();
            if (tileHit.tileType == Tile.TypeOfTile.Blocker)
            {
                return;
            }

            if (State == CharacterState.Moving)
            {
                mouvementSequence.Kill();
            }
            List<Tile> positionToGo = new List<Tile>();
            GetTileUnder().avatarOnMe = false;
            positionToGo = TilesManager.Instance.GeneratePathTo(GetTileUnder(), tileHit);
            mouvementSequence = DOTween.Sequence();

            State = CharacterState.Moving;
            workZone.SetActive(false);

            Vector3 start = transform.position;
            for (int i = 0; i < positionToGo.Count; i++)
            {
                Vector3 point = positionToGo[i].transform.position;
                float time = Vector3.Distance(start, point);
                mouvementSequence.AppendCallback(() => transform.LookAt(point));
                mouvementSequence.Append(transform.DOMove(point, time).SetEase(Ease.Linear));
                start = positionToGo[i].transform.position;
            }
            mouvementRemain -= positionToGo.Count - 1;
            mouvementSequence.timeScale = mouvementAnimationSpeed;
            mouvementSequence.onComplete += EndMove;
        }
        else
        {
            if (tileHit.tileType == Tile.TypeOfTile.Blocker)
            {
                return;
            }
            if (tileSelectedForMove != null)
            {
                List<Tile>resetTiles = TilesManager.Instance.GeneratePathTo(GetTileUnder(), tileSelectedForMove);
                foreach (Tile tile in resetTiles)
                {
                    tile.SetNormalColor();
                }
            }
            TilesManager.Instance.DrawMoveRange(mouvementRemain);
            tileSelectedForMove = tileHit;
            List<Tile> preview = TilesManager.Instance.GeneratePathTo(GetTileUnder(), tileHit);
            for (int i = 0; i < preview.Count; i++)
            {
                Color colorLerped = Color.Lerp(Color.yellow, Color.green, ((float)(i + 1) / (float)preview.Count));
                preview[i].GetComponent<MeshRenderer>().materials[1].color = colorLerped;
            }
        }
    }

    public Tile GetTileUnder()
    {
        Tile tileUnder = null;
        RaycastHit hitTile;
        LayerMask layerMask = 1 << 10;
        Debug.DrawRay(transform.position - new Vector3(0, -0.5f, 0), -Vector3.up, Color.red, 10);
        if (Physics.Raycast(transform.position - new Vector3(0, -0.5f, 0), -Vector3.up, out hitTile, 3, layerMask))
        {
            
            tileUnder = hitTile.transform.GetComponent<Tile>();
        }
        return tileUnder;
    }

    private void UseAllMovement()
    {
        mouvementRemain = 0;
        State = CharacterState.WaitForAction;
        EndMove();
    }

    private void EndMove()
    {
        GetTileUnder().avatarOnMe = true;
        if (mouvementRemain == 0)
        {
            workZone.SetActive(true);
            mouvementRemain = mouvementRange;
            TilesManager.Instance.SetNormalColorOfTiles();
            RoundManager.Instance.LaunchEndRound();
        }
        else
        {
            TilesManager.Instance.DrawMoveRange(mouvementRemain);
            State = CharacterState.WaitForMoving;
        }
    }

    private void SetResourceInStock(Tile resourceFocused)
    {
        resourceFocused.DrawResourceHarvest();
        GetResourceInStock(resourceFocused.resourcesInfos.resourceType).NumberInStock += resourceFocused.resourcesInfos.resourcesAmount;
        resourceFocused.State = Tile.StateOfResources.Reloading;
    }

    public void BeginMining()
    {
        if(State != CharacterState.Mining)
            State = CharacterState.Mining;
    }

    void OnTriggerEnter (Collider collider)
    {
        if (collider.transform.tag == "Hexagone")
        {
            Tile tile = collider.GetComponent<Tile>();
            if (tile != null && tile.tileType != Tile.TypeOfTile.None)
            {
                tile.tileType = Tile.TypeOfTile.None;
                tile.SetTypeOfTile();
            }
                
        }
    }
}

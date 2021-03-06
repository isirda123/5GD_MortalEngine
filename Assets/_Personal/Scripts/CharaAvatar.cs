﻿using UnityEngine;
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
    [SerializeField] Color lerpFrom, LerpTo;
    LineRenderer line;
    [SerializeField] GameObject pin;
    #endregion

    #region STATE MACHINE
    //CHARACTER STATE MACHINE
    public enum CharacterState
    {
        WaitForAction,
        Moving,
        Mining,
        WaitForMoving,
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
                print(TilesManager.Instance);
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
        ActionsButtons.ReturnMenu += SetWaitForAction;
        ActionsButtons.ReturnMenu += destroyLineRenderer;
        ActionsButtons.ReturnMenu += HideHarvestButton;
        ActionsButtons.Harvest += HarvestTilesAround;
        ActionsButtons.PassDurigMove += UseAllMovement;
        RoundManager.RoundEnd += UseResourcesInStock;
        RoundManager.RoundEnd += SetMaxMouvementRemain;
        ResourceInStock.ResourceEmpty += ChangeUsingRessource;
        RoundManager.RoundStart += CheckForNumberOfTurnWithoutDying;
    }

    private void UnassignEvents()
    {
        Tile.TileTouched -= Move;
        ActionsButtons.Move -= SetWaitForMoving;
        ActionsButtons.ReturnMenu -= SetWaitForAction;
        ActionsButtons.ReturnMenu -= destroyLineRenderer;
        ActionsButtons.ReturnMenu -= HideHarvestButton;
        ActionsButtons.Harvest -= HarvestTilesAround;
        ActionsButtons.PassDurigMove -= UseAllMovement;
        RoundManager.RoundEnd -= UseResourcesInStock;
        RoundManager.RoundEnd -= SetMaxMouvementRemain;
        ResourceInStock.ResourceEmpty -= ChangeUsingRessource;
        RoundManager.RoundStart -= CheckForNumberOfTurnWithoutDying;
    }

    private void OnEnable()
    {
        StartCoroutine(GameManager.Instance.FadeOut());
        pin = Instantiate(pin, Vector3.zero,pin.transform.rotation);
        pin.SetActive(false);
        AssignEvents();

        SetStartingValues();
        SetMaxMouvementRemain();
        GetTileUnder().avatarOnMe = true;
    }

    private void OnDisable()
    {
        UnassignEvents();
    }

    #endregion

    #region START SETTINGS

    //void Start()
    //{
    //    SetStartingValues();
    //    SetMaxMouvementRemain();
    //    GetTileUnder().avatarOnMe = true;
    //}

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
                    needs[i].Multiplicator = resourcesNeedsStartDatas.needBuildsStart;
                    break;
                case Need.NeedType.Energy:
                    needs[i].Multiplicator = resourcesNeedsStartDatas.needEnergyStart;
                    break;
                case Need.NeedType.Food:
                    needs[i].Multiplicator = resourcesNeedsStartDatas.needFoodStart;
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

    [SerializeField] public Need[] needs;
    [SerializeField] public ResourceInStock[] stock;
    [SerializeField] ResourcesNeedsStartDatas resourcesNeedsStartDatas;

   


    private Tile resourceFocused;

    private void SetResourceUsed(ResourcesInfos resourceToUseInfos,Need need)
    {
        ResourceInStock resourceInStock = GetResourceInStock(resourceToUseInfos.resourceType);
        print("set resource used");
        PlayerInput.Instance.needSelected.ResourceUsed = resourceInStock;
    }

    private void SetWaitForMoving()
    {
        State = CharacterState.WaitForMoving;
    }

    private void SetWaitForAction()
    {
        State = CharacterState.WaitForAction;
        tileSelectedForMove = null;
    }

    private void SetMaxMouvementRemain() => mouvementRemain = mouvementRange + DecretManager.Instance.totalDecreeInfos.numberOfMove;


    [HideInInspector] bool waitForHarvest = false;
    private void HarvestTilesAround()
    {
        if (waitForHarvest == true)
        {
            List<Tile> tiles = GetResourcesAround(GetTileUnder().neighbours);
            if (SomethingAroundToHarvest(tiles) == true)
            {
                for (int i = 0; i < tiles.Count; i++)
                {
                    if (tiles[i].State == Tile.StateOfResources.Available)
                    {
                        SetResourceInStock(tiles[i]);
                        tiles[i].State = Tile.StateOfResources.Reloading;
                        tiles[i].DrawResourceHarvest();
                    }
                }
                SoundManager.Instance.RecolteFeedBackSound();
                Sequence sequence = DOTween.Sequence();
                sequence.AppendInterval(2);
                sequence.OnComplete(() => RoundManager.Instance.LaunchEndRound());
                CheckForVictory();
            }
            else
            {

            }
            HideHarvestButton();
            waitForHarvest = false;
        }
        else
        {
            DrawResourcesValueAround();
            waitForHarvest = true;
        }
    }
    void HideHarvestButton()
    {
        UIManager.Instance.Harvest.gameObject.SetActive(false);
        UIManager.Instance.returnMenuHarvest.gameObject.SetActive(false);
    }

    void DrawResourcesValueAround()
    {

    }

    public bool SomethingAroundToHarvest(List<Tile>Neighbours)
    {
        return true;
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
                needs[i].resourceJustChanged = true;
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
                                needs[i].resourceJustChanged = true;
                                needs[i].ResourceUsed = otherResourceUsable;
                            }
                            else
                            {
                                if (needs[i].Multiplicator > 0)
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
        if (State != CharacterState.WaitForMoving)
        {
            return;
        }
        if (tileHit == tileSelectedForMove)
        {
            tileSelectedForMove = null;
            Destroy(line.gameObject);
            pin.SetActive(false);
            TilesManager.Instance.SetNormalColorOfTiles();
            TilesManager.Instance.DrawOffset(true);
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

            Vector3 start = transform.position;
            for (int i = 0; i < positionToGo.Count; i++)
            {
                Vector3 point = positionToGo[i].transform.position;
                float time = Vector3.Distance(start, point);
                mouvementSequence.Append(transform.DOMove(point, time).SetEase(Ease.Linear));
                start = positionToGo[i].transform.position;
            }
            mouvementRemain -= positionToGo.Count - 1;
            mouvementSequence.timeScale = mouvementAnimationSpeed;
            SoundManager.Instance.Deplacement();
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
                List<Tile> resetTiles = TilesManager.Instance.GeneratePathTo(GetTileUnder(), tileSelectedForMove);
                foreach (Tile tile in resetTiles)
                {
                    tile.SetNormalColor();
                }
            }
            List<Tile> preview = TilesManager.Instance.GeneratePathTo(GetTileUnder(), tileHit);
            TilesManager.Instance.DrawMoveRange(mouvementRemain);
            if (preview.Count > mouvementRemain + 1)
            {
                print("Too Far");
                tileSelectedForMove = null;
                return;
            }
            tileSelectedForMove = tileHit;
            List<Vector3> posForArrow = new List<Vector3>();
            for (int i = 0; i < preview.Count; i++)
            {
                //Color colorLerped = Color.Lerp(lerpFrom, LerpTo, ((float)(i + 1) / (float)preview.Count));
                //preview[i].GetComponent<MeshRenderer>().materials[1].color = colorLerped;
                posForArrow.Add(preview[i].transform.position);
                print("add");
            }
            if (posForArrow.Count > 0)
            {
                print("Print Arrow");
                SetVisualArrow(posForArrow);
            }
        }
    }

    void destroyLineRenderer()
    {
        if (line != null)
        {
            Destroy(line.gameObject);
            pin.SetActive(false);
            line = null;
        }
    }

    void SetVisualArrow(List<Vector3> posForArrow)
    {
        destroyLineRenderer();

        line = (Instantiate(GameManager.Instance.gameAssets.arrow, GetTileUnder().transform.position, Quaternion.identity)).GetComponent<LineRenderer>();
        line.positionCount = posForArrow.Count;
        for (int i =0; i < posForArrow.Count; i++)
        {
            line.SetPositions(posForArrow.ToArray());
        }
        pin.SetActive(true);
        pin.transform.position = posForArrow[posForArrow.Count - 1];
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
        if (line != null)
        {
            Destroy(line.gameObject);
            pin.SetActive(false);
        }
        EndMove();
    }

    private void EndMove()
    {
        GetTileUnder().avatarOnMe = true;
        TilesManager.Instance.SetReachableTileTo(false);
        TilesManager.Instance.DrawOffset(true);
        TilesManager.Instance.SetNormalColorOfTiles();
        SoundManager.Instance.StopInstantFeedBack();
        if (mouvementRemain == 0)
        {
            SetMaxMouvementRemain();
            TilesManager.Instance.SetNormalColorOfTiles();
            RoundManager.Instance.LaunchEndRound();
            State = CharacterState.WaitForAction;
        }
        else
        {
            TilesManager.Instance.DrawMoveRange(mouvementRemain);
            UIManager.Instance.returnMenu.gameObject.SetActive(false);
            UIManager.Instance.passDuringMove.gameObject.SetActive(true);
            State = CharacterState.WaitForMoving;
        }
    }

    private void SetResourceInStock(Tile resourceFocused)
    {
        if (resourceFocused.tileType == Tile.TypeOfTile.Mouflu)
        {
            GetResourceInStock(resourceFocused.resourcesInfos.resourceType).NumberInStock += resourceFocused.resourcesInfos.resourcesAmount + DecretManager.Instance.totalDecreeInfos.collectQuantityMouflu;
        }
        else if (resourceFocused.tileType == Tile.TypeOfTile.Rock)
        {
            GetResourceInStock(resourceFocused.resourcesInfos.resourceType).NumberInStock += resourceFocused.resourcesInfos.resourcesAmount + DecretManager.Instance.totalDecreeInfos.collectQuantityRock;
        }
        else if (resourceFocused.tileType == Tile.TypeOfTile.Wood)
        {
            GetResourceInStock(resourceFocused.resourcesInfos.resourceType).NumberInStock += resourceFocused.resourcesInfos.resourcesAmount + DecretManager.Instance.totalDecreeInfos.collectQuantityWood;
        }
        else if (resourceFocused.tileType == Tile.TypeOfTile.Berry)
        {
            GetResourceInStock(resourceFocused.resourcesInfos.resourceType).NumberInStock += resourceFocused.resourcesInfos.resourcesAmount + DecretManager.Instance.totalDecreeInfos.collectQuantityBerry;
        }
    }
    public void SetResourceInStock(GameManager.ResourceType typeOfResource, int amount)
    {
        GetResourceInStock(typeOfResource).NumberInStock += amount;
    }

    [System.Serializable]
    public struct ResourceConsume
    {
        public GameManager.ResourceType resourceType;
        public float amountPerRound;
    }

    [SerializeField]
    public ResourceConsume[] allResourceUsedPerRound;
    [SerializeField]
    public ResourceConsume[] allResourceGetPerRound;
    private void CheckForVictory()
    {
        bool victory = true;
        allResourceUsedPerRound = ListToOrganize(GetResourcesUsedPerRound());
        allResourceGetPerRound = ListToOrganize(GetResourcePerRound());

        for (int i =0; i < allResourceUsedPerRound.Length; i++)
        {
            if (allResourceGetPerRound[i].amountPerRound < allResourceUsedPerRound[i].amountPerRound)
            {
                //print("NoVictory : " + allResourceGetPerRound[i].resourceType + " :" + allResourceGetPerRound[i].amountPerRound + " - " + allResourceUsedPerRound[i].resourceType + " : " + allResourceUsedPerRound[i].amountPerRound);
                victory = false;
            }
        }

        if (victory)
        {
            RoundManager.Instance.EndLevel(true);
        }
    }

    private ResourceConsume[] ListToOrganize(List<ResourceConsume> listToOrganize)
    {
        ResourceConsume[] order = new ResourceConsume[5];
        List<ResourceConsume> buffer = new List<ResourceConsume>();

        for (int i = 0; i < listToOrganize.Count; i++)
        {
            order[(int)listToOrganize[i].resourceType] = listToOrganize[i];
        }

        return order;
    }

    private List<ResourceConsume> GetResourcePerRound()
    {
        List<ResourceConsume> allResourceGetPerRound = new List<ResourceConsume>();
        List<Tile> neighbour = GetTileUnder().neighbours;
        foreach (Tile tile in neighbour)
        {
            if (tile.CheckForSameTypeAround(tile.neighbours))
            {
                if (tile.resourcesInfos != null)
                {
                    if (tile.roundNbrOfDesable == RoundManager.Instance.numberOfRound)
                    {

                        bool resourceAlreadyUsed = false;
                        for (int i = 0; i < allResourceGetPerRound.Count; i++)
                        {
                            if (allResourceGetPerRound[i].resourceType == tile.resourcesInfos.resourceType)
                            {
                                resourceAlreadyUsed = true;
                                ResourceConsume rC = allResourceGetPerRound[i];
                                rC.amountPerRound += tile.resourcesInfos.WonPerRound;
                                allResourceGetPerRound[i] = rC;
                            }
                        }
                        if (!resourceAlreadyUsed)
                        {
                            ResourceConsume rC = new ResourceConsume();
                            rC.resourceType = tile.resourcesInfos.resourceType;
                            rC.amountPerRound = tile.resourcesInfos.WonPerRound;
                            allResourceGetPerRound.Add(rC);
                        }
                    }
                }
            }
        }
        return allResourceGetPerRound;
    }

    public List<ResourceConsume> GetResourcesUsedPerRound()
    {
        List<ResourceConsume> allResourceNeeded = new List<ResourceConsume>();
        foreach (Need need in needs)
        {
            bool resourceAlreadyUsed = false;
            //check if a resource is already used
            for (int i = 0; i < allResourceNeeded.Count; i++)
            {
                if(allResourceNeeded[i].resourceType == need.ResourceUsed.resourcesInfos.resourceType)
                {
                    resourceAlreadyUsed = true;
                    ResourceConsume rC = allResourceNeeded[i];
                    rC.amountPerRound += need.ResourceUsed.resourcesInfos.GetAmontUseFor(need.needType) * need.Multiplicator;
                    allResourceNeeded[i] = rC;
                }
            }
            if (!resourceAlreadyUsed)
            {
                ResourceConsume rC = new ResourceConsume();
                rC.resourceType = need.ResourceUsed.resourcesInfos.resourceType;
                rC.amountPerRound = need.ResourceUsed.resourcesInfos.GetAmontUseFor(need.needType) * need.Multiplicator;
                allResourceNeeded.Add(rC);
            }
        }

        return allResourceNeeded;
    }

    void CheckForNumberOfTurnWithoutDying()
    {
        bool youAreInDeepShitBro = false;
        List<ResourceConsume> resourceNeededPerRound = GetResourcesUsedPerRound();
        for (int i =0; i < resourceNeededPerRound.Count; i++)
        {
            if (resourceNeededPerRound[i].resourceType == GameManager.ResourceType.Wood)
            {
                if (resourceNeededPerRound[i].amountPerRound >= stock[0].NumberInStock)
                {
                    youAreInDeepShitBro = true;
                }
            }
            else if (resourceNeededPerRound[i].resourceType == GameManager.ResourceType.Rock)
            {
                if (resourceNeededPerRound[i].amountPerRound >= stock[1].NumberInStock)
                {
                    youAreInDeepShitBro = true;
                }
            }
            else if (resourceNeededPerRound[i].resourceType == GameManager.ResourceType.Berry)
            {
                if (resourceNeededPerRound[i].amountPerRound >= stock[2].NumberInStock)
                {
                    youAreInDeepShitBro = true;
                }
            }
            else if (resourceNeededPerRound[i].resourceType == GameManager.ResourceType.Mouflu)
            {
                if (resourceNeededPerRound[i].amountPerRound >= stock[3].NumberInStock)
                {
                    youAreInDeepShitBro = true;
                }
            }
        }
        if (youAreInDeepShitBro == true)
        {
            SoundManager.Instance.LowSupplySound();
        }

    }


    void OnTriggerEnter (Collider collider)
    {
        if (collider.transform.tag == "Hexagone")
        {
            Tile tile = collider.GetComponent<Tile>();
            if (tile != null && tile.tileType != Tile.TypeOfTile.None && tile.State != Tile.StateOfResources.Reloading)
            {
                tile.State = Tile.StateOfResources.Reloading;
            }
                
        }
    }
}

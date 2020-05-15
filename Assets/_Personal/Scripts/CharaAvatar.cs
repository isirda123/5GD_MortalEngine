using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Linq;
using DG.Tweening;
using System;

public class CharaAvatar : MonoBehaviour
{
    [SerializeField] private GameObject workZone;

    //MOUVEMENT
    private Sequence mouvementSequence;
    [SerializeField] private float mouvementAnimationSpeed;
    [SerializeField] private int mouvementRange;
    private Tile tileSelectedForMove = null;
    private int mouvementRemain;

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

    private Tile resourceFocused;

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
        }
        Sequence sequence = DOTween.Sequence();
        sequence.AppendInterval(2);
        sequence.OnComplete(()=> GameManager.Instance.LunchEndRound());
    }
    //EVENTS
    private void AssignActions()
    {
        Tile.TileTouched += Move;
        ActionsButtons.Move += SetWaitForMoving;
        ActionsButtons.Harvest += HarvestTilesAround;
        ActionsButtons.Pass += UseAllMovement;
    }

    private void UnassignActions()
    {
        Tile.TileTouched -= Move;
        ActionsButtons.Move -= SetWaitForMoving;
        ActionsButtons.Harvest -= HarvestTilesAround;
        ActionsButtons.Pass -= UseAllMovement;
    }
    private void OnEnable()
    {
        AssignActions();
    }
    private void OnDesable()
    {
        UnassignActions();
    }

    void Start()
    {
        SetMaxMouvementRemain();
        GetTileUnder().avatarOnMe = true;
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

    private void CheckLevelValidation(List<Tile> resourcesAround)
    {
        int woodTilesNeeded = 0;
        int berryTilesNeeded = 0;
        for (int i = 0; i < GameManager.Instance.needs.Length; i++)
        {
            if (GameManager.Instance.needs[i].resourceUsed.resourceType == GameManager.ResourceType.Wood)
                woodTilesNeeded += GameManager.Instance.needs[i].TilesNeeded;
            else
            if (GameManager.Instance.needs[i].resourceUsed.resourceType == GameManager.ResourceType.Berry)
                berryTilesNeeded += GameManager.Instance.needs[i].TilesNeeded;
            //protection pour que le joueur mette des resources renouvlables pour le check de son lvl
            else
                woodTilesNeeded += 10;
        }

        //count resources around
        int woodAround = 0;
        int berryAround = 0;
        for (int i = 0; i < resourcesAround.Count; i++)
        {
            if (resourcesAround[i].resourcesInfos.resourceType == GameManager.ResourceType.Wood)
                woodAround++;
            if (resourcesAround[i].resourcesInfos.resourceType == GameManager.ResourceType.Berry)
                berryAround++;
        }

        //compare both
        if (woodAround >= woodTilesNeeded && berryAround >= berryTilesNeeded)
            GameManager.Instance.EndLevel(true);
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
            if (tileHit.tileType == Tile.typeOfTile.Blocker)
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
            if (tileHit.tileType == Tile.typeOfTile.Blocker)
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
        print("TileUnder : " + tileUnder);
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
            GameManager.Instance.LunchEndRound();
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
        GameManager.Instance.GetResourceInStock(resourceFocused.resourcesInfos.resourceType).NumberInStock += resourceFocused.resourcesInfos.resourcesAmount;
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
            if (tile != null)
                tile.State = Tile.StateOfResources.Reloading;
        }
    }
}

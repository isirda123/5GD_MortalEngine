﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Linq;
using DG.Tweening;
using System;

public class CharaAvatar : MonoBehaviour
{
    [Header("Work")]
    [SerializeField] public GameObject workZone;
    [SerializeField] float rangeWorkZone;
    float timeToMineAll;
    [HideInInspector] public bool stopped;
    [SerializeField] public bool doItOneTime = false;
    bool findThePos = false;
    [Tooltip("In Seconde")]
    [SerializeField] float timeBeforeVictory;
    public float speedOfMove;

    [Header("UI")]
    [SerializeField] GameObject mineCanvas;
    [SerializeField] Text woodText;
    [SerializeField] Text chickenText;
    [SerializeField] Text cornText;
    [SerializeField] Text rockText;
    [SerializeField] Text timingTime;
    [SerializeField] public Text buttonText;

    [SerializeField] Collider[] hitColliders;
    [SerializeField] TilesManager pathManager;
    DG.Tweening.Sequence sequence;
    public static event Action MoveEnd;

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

    private float startMiningTime;

    private Tile resourceFocused;

    private void SwitchState(CharacterState focusState)
    {
        switch (focusState)
        {
            case CharacterState.WaitForAction:
                actualState = CharacterState.WaitForAction;
                break;
            case CharacterState.WaitForMoving:
                print("DRAW MOVE RANGE");
                actualState = CharacterState.WaitForMoving;
                //draw deplacement range
                break;
            case CharacterState.Moving:
                buttonText.text = "Begin";
                actualState = CharacterState.Moving;
                break;
            case CharacterState.Mining:
                // feedbacks (light target resource etc)
                //set variables for mine
                startMiningTime = Time.time;

                //Collider[] hitColliders = Physics.OverlapSphere(transform.position, rangeWorkZone / 2, 1 << 8);
                //get resources around & set mine text & check level validation

                List<Tile> neighbours = new List<Tile>();
                List<Tile> neighboursTwo = GetTileUnder().neighbours;
                foreach (Tile tI in neighboursTwo)
                {
                    if (tI.stateResources == Tile.stateOfResources.Available && tI.tileType != Tile.typeOfTile.Blocker && tI.tileType != Tile.typeOfTile.None)
                    {
                        neighbours.Add(tI);
                    }
                }
                print(neighbours.Count);
                List<Tile> resourcesInRange = GetResourcesAround(neighbours);
                if (neighbours.Count > 0)
                {
                    CheckLevelValidation(resourcesInRange);
                    resourceFocused = GetOptimalResource(resourcesInRange);
                    actualState = CharacterState.Mining;
                }
                else
                {
                    print("Nothing there");
                    actualState = CharacterState.Moving;
                }
                break;
        }
    }
    
    private void SetWaitForMoving()
    {
        State = CharacterState.WaitForMoving;
    }

    private void HarvestTilesAround()
    {
         GetResourcesAround(GetTileUnder().neighbours);
         
    }

    void Start()
    {
        Tile.TileTouched += Move;
        ActionsButtons.Move += SetWaitForMoving;
       // ActionsButtons.Harvest += Mine;
    }

    private void OnDestroy()
    {
        Tile.TileTouched -= Move;
        ActionsButtons.Move -= SetWaitForMoving;
    }

    private void Update()
    {
        //CheckState(State);
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
        //use to check if needs < resourcesAround
        //count tiles needed
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

    private Tile GetOptimalResource(List<Tile> resourcesInRange)
    {
        Need[] needs = new Need[GameManager.Instance.needs.Length];
        GameManager.Instance.needs.CopyTo(needs,0);
        System.Array.Sort(needs, (x, y) => x.LifeTime.CompareTo(y.LifeTime));
        Tile firstOptimalResourceInGame = null;
        for (int n = 0; n < needs.Length; n++)
        {
            Need need = needs[n];
            GameManager.ResourceType resourceTypeToTake = need.resourceUsed.resourcesInfos.resourceType;
            //check if there is a optimal resource in range
            for (int i = 0; i < resourcesInRange.Count; i++)
            {
                if (resourcesInRange[i].resourcesInfos.resourceType == resourceTypeToTake)
                    firstOptimalResourceInGame = resourcesInRange[i];
            }
            //if there is none, take the first resource that can be use for this need
            if (firstOptimalResourceInGame == null)
            {
                for (int i = 0; i < resourcesInRange.Count; i++)
                {
                    for (int j = 0; j < need.resourcesUsable.Length; j++)
                    {
                        if (resourcesInRange[i].resourcesInfos.resourceType == need.resourcesUsable[j])
                            firstOptimalResourceInGame = resourcesInRange[i];
                    }
                }
            }
            if (firstOptimalResourceInGame != null)
                n = 1000;
        }
        return firstOptimalResourceInGame;
    }

    private void Move(Tile tileHit)
    {
        if (tileHit.tileType == Tile.typeOfTile.Blocker || State != CharacterState.WaitForMoving)
        {
            return;
        }

        if (State == CharacterState.Moving)
        {
            sequence.Kill();
        }
        List<Vector3> positionToGo = new List<Vector3>();
        positionToGo = pathManager.GeneratePathTo(GetTileUnder(), tileHit);
        sequence = DOTween.Sequence();

        State = CharacterState.Moving;
        stopped = false;
        workZone.SetActive(false);

        Vector3 start = transform.position;
        for (int i = 0; i < positionToGo.Count; i++)
        {
            Vector3 point = positionToGo[i];
            float time = Vector3.Distance(start, point);
            sequence.AppendCallback(() => transform.LookAt(point));
            sequence.Append(transform.DOMove(point, time).SetEase(Ease.Linear));
            start = positionToGo[i];
        }

        sequence.timeScale = speedOfMove;
        sequence.onComplete += EndMove;
        //sequence.onComplete += CheckTileUnder;
    }

    private Tile GetTileUnder()
    {
        Tile tileUnder = null;
        RaycastHit hitTile;
        LayerMask layerMask = 1 << 10;
        if (Physics.Raycast(transform.position - new Vector3(0, -0.5f, 0), -Vector3.up, out hitTile, 3, layerMask))
        {
            tileUnder = hitTile.transform.GetComponent<Tile>();
        }
        return tileUnder;
    }

    private void EndMove()
    {
        workZone.SetActive(true);
        stopped = true;
        MoveEnd?.Invoke();
    }

    private void SetResourceInStock(Tile resourceFocused)
    {
        GameManager.Instance.GetResourceInStock(resourceFocused.resourcesInfos.resourceType).NumberInStock += resourceFocused.resourcesInfos.resourcesAmount;
        resourceFocused.stateResources = Tile.stateOfResources.Reloading;
    }

    public void BeginMining()
    {
        if(State != CharacterState.Mining)
            State = CharacterState.Mining;
    }

    void OnTriggerEnter (Collider collider)
    {
        if (collider.transform.tag == "Resources")
        {
            collider.GetComponent<Tile>().stateResources = Tile.stateOfResources.Reloading;
        }
    }
}

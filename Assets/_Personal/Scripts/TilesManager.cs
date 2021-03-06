﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

using Random = UnityEngine.Random;
[ExecuteInEditMode]
public class TilesManager : Singleton<TilesManager>
{

    [SerializeField] List<Tile> tileReachable = new List<Tile>();

    public List<Tile> tiles = new List<Tile>();

    [SerializeField]List<Tile> currentPath = null;

    [Range(0,10)]
    [SerializeField] public float offSetTile;



    [ContextMenu("Set All Tiles")]
    public void SetAllTiles()
    {
        GetTiles();
        SetTilesTypes();
        SetTilesNeighbors();
    }

    [ContextMenu("Set Tiles Types")]
    void SetTilesTypes()
    {
        for (int i = 0; i < tiles.Count; i++)
        {
            if (tiles[i].tag == "Hexagone")
            {
                tiles[i].GetComponent<Tile>().DrawVisualTile();
                tiles[i].GetComponent<Tile>().DrawRandomVisualOnNoneTile();
                tiles[i].GetComponent<Tile>().DrawRandomVisualOnWaterTile();
            }
        }
    }

    [ContextMenu("Set Tiles Neighbors")]
    void SetTilesNeighbors()
    {
        for (int i = 0; i < tiles.Count; i++)
        {
            if (tiles[i].tag == "Hexagone")
            {
                tiles[i].GetComponent<Tile>().GetTileAround();
            }
        }
    }

    public void SetReachableTileTo (bool b)
    {
        foreach (Tile tile in tiles)
        {
            tile.reachable = b;
        }
    }
    public void DrawOffset(bool enable)
    {
        foreach(Tile tile in tiles)
        {
            tile.drawOffsetTile(enable);
        }

    }

    public void SetNormalColorOfTiles()
    {
        foreach (Tile tile in tiles)
        {
            tile.SetNormalColor();
        }
    }

    [ContextMenu("Get tiles")]
    void GetTiles()
    {
        tiles.Clear();
        for (int i = 0; i < transform.childCount; i++)
        {
            tiles.Add(transform.GetChild(i).GetComponent<Tile>());
        }
    }

#if UNITY_EDITOR
    [ExecuteInEditMode]
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            SetAllTiles();
            print("coucou");
        }
    }
#endif

    public List<Tile> GeneratePathTo(Tile start, Tile target)
    {
        Dictionary<Tile, float> distance = new Dictionary<Tile, float>();
        Dictionary<Tile, Tile> previous = new Dictionary<Tile, Tile>();

        //Setup the GameObject Not Visited Yet
        List<Tile> unvisitedTiles = new List<Tile>();

        distance[start] = 0;
        previous[start] = null;

        //Initialize everything to have inifity distance
        foreach (Tile go in tiles)
        {
            if (go != start)
            {
                distance[go] = Mathf.Infinity;
                previous[go] = null;
            }

            unvisitedTiles.Add(go);
        }
        while (unvisitedTiles.Count > 0)
        {
            //UnvisitedGO will be the Gameobject as close as the start as possible
            Tile unvisitedGO = null;

            foreach (Tile possibleUnvisitedGO in unvisitedTiles)
            {
                
                if (unvisitedGO == null || distance[possibleUnvisitedGO] < distance[unvisitedGO])
                {
                    unvisitedGO = possibleUnvisitedGO;
                }
            }
            if (unvisitedGO == target)
            {
                //Exit if we find the target
                break;
            }
            unvisitedTiles.Remove(unvisitedGO);

            foreach (Tile go in unvisitedGO.GetComponent<Tile>().neighbours)
            {
                if (go.tileType != Tile.TypeOfTile.Blocker)
                {
                    float alt = distance[unvisitedGO] + Vector3.Distance(unvisitedGO.transform.position, go.transform.position);
                    if (alt < distance[go])
                    {
                        distance[go] = alt;
                        previous[go] = unvisitedGO;
                    }
                }
            }
        }
        foreach (Tile go in target.neighbours)
        {
            if (previous[go]!= null){
            }
        }
        if (previous[target] == null)
        {
            //The target is not reachable so he didn't have neighbours connected to him, or there is a gab between the start and the end
            return null;
        }

        currentPath = new List<Tile>();
        Tile currentGO = target;
        //Step through the previous chain of GameObject to create the path
        while(currentGO != null)
        {
            currentPath.Add(currentGO);
            currentGO = previous[currentGO];
        }

        //The path is invert. Going from Target to Start. Change it.
        currentPath.Reverse();
        List<Tile> positionToGo = new List<Tile>();
        for (int i = 0; i < currentPath.Count; i++)
        {
            positionToGo.Add(currentPath[i]);
        }
        return positionToGo;
    }

    Tile CheckWhereAvatarIs()
    {
        foreach(Tile tile in tiles)
        {
            if (tile.avatarOnMe == true)
            {
                return tile;
            }
        }

        return null;
    }

    public void DrawMoveRange(int mouvementRemain)
    {
        tileReachable.Clear();
        Tile start = CheckWhereAvatarIs();
        tileReachable.Add(start);
        foreach (Tile go in start.neighbours)
        {
            if (go.tileType != Tile.TypeOfTile.Blocker)
            {
                tileReachable.Add(go);
            }
        }

        for (int i =0; i < mouvementRemain -1; i++)
        {
            int lenghtOfArrayNow = tileReachable.Count;
            for (int j =0; j< lenghtOfArrayNow; j++)
            {
                foreach(Tile go in tileReachable[j].neighbours)
                {
                    if (go.tileType != Tile.TypeOfTile.Blocker)
                    {
                        bool noEntry = true;
                        for (int k =0; k <tileReachable.Count; k++)
                        {
                            if (tileReachable[k] == go)
                            {
                                noEntry = false;
                                break;
                            }
                        }
                        if (noEntry == true)
                        {
                            tileReachable.Add(go);
                        }
                    }
                }
            }
        }

        foreach(Tile tile in tileReachable)
        {
            tile.reachable = true;
        }

        foreach (Tile tile in tiles)
        {
            if (tile.reachable == false)
            {
                tile.DrawDisableTile();
                tile.drawOffsetTile(true);
                tile.drawOffsetTile(false);
            }
        }


        Dictionary<Tile, float> distance = new Dictionary<Tile, float>();
        Dictionary<Tile, Tile> previous = new Dictionary<Tile, Tile>();

        //Setup the GameObject Not Visited Yet
        List<Tile> unvisitedTiles = new List<Tile>();

        distance[start] = 0;
        previous[start] = null;

        //Initialize everything to have inifity distance
        foreach (Tile go in tiles)
        {
            if (go != start)
            {
                distance[go] = Mathf.Infinity;
                previous[go] = null;
            }

            unvisitedTiles.Add(go);
        }
        while (unvisitedTiles.Count > 0)
        {
            //UnvisitedGO will be the Gameobject as close as the start as possible
            Tile unvisitedGO = null;

            foreach (Tile possibleUnvisitedGO in unvisitedTiles)
            {

                if (unvisitedGO == null || distance[possibleUnvisitedGO] < distance[unvisitedGO])
                {
                    unvisitedGO = possibleUnvisitedGO;
                }
            }
            unvisitedTiles.Remove(unvisitedGO);

            foreach (Tile go in unvisitedGO.GetComponent<Tile>().neighbours)
            {
                if (go.GetComponent<Tile>().tileType != Tile.TypeOfTile.Blocker)
                {
                    float alt = distance[unvisitedGO] + Vector3.Distance(unvisitedGO.transform.position, go.transform.position);
                    if (alt < distance[go])
                    {
                        distance[go] = alt;
                        previous[go] = unvisitedGO;
                    }
                }
            }
        }
    }

   

   

    Tile.TypeOfTile[] spawnResourceTypes = { Tile.TypeOfTile.Wood, Tile.TypeOfTile.Berry, Tile.TypeOfTile.Mouflu };
    public AnimationCurve curve;

    private List<Tile> allEmptyTileAround (List<Tile> heap)
    {
        List<Tile> emptyNeighbours = new List<Tile>();
        foreach (Tile tile in heap)
        {
            foreach (Tile neighbours in tile.neighbours)
            {
                if (neighbours.tileType == Tile.TypeOfTile.None)
                {
                    bool noEntry = false;
                    for (int i =0; i<emptyNeighbours.Count; i++)
                    {
                        if (emptyNeighbours.Count > 0)
                        {
                            if (emptyNeighbours[i] == neighbours)
                            {
                                noEntry = true;
                            }
                        }

                    }
                    if (noEntry == false)
                    {
                        emptyNeighbours.Add(neighbours);
                    }
                }
            }
        }

        return emptyNeighbours;
    }

    private List<Tile> AllNeighboursType (Tile start)
    {
        bool endOfCheck = false;
        List<Tile> neighboursToReturn = new List<Tile>();
        neighboursToReturn.Add(start);
        while (endOfCheck == false)
        {
            bool newTileAdd = false;
            int lenghOfArrayNow = neighboursToReturn.Count;
            for (int j =0; j<lenghOfArrayNow; j++)
            {
                foreach (Tile neighbours in neighboursToReturn[j].neighbours)
                {
                    if (neighbours.tileType == neighboursToReturn[j].tileType)
                    {
                        bool noEntry = false;
                        for (int i =0; i < neighboursToReturn.Count; i++)
                        {
                            if (neighboursToReturn[i] == neighbours)
                            {
                                noEntry = true;
                            }
                        }
                        if (noEntry == false)
                        {
                            neighboursToReturn.Add(neighbours);
                            neighbours.checkedForRespawn = true;
                            newTileAdd = true;
                        }
                    }
                }
            }
            if (newTileAdd == false)
            {
                endOfCheck = true;
            }
        }


        return neighboursToReturn;
    }

    public void ResetCheckedBool()
    {
        foreach(Tile tile in tiles)
        {
            tile.checkedForRespawn = false;
        }
    }

    void returnToNormalWorld()
    {
        DrawOffset(true);
        SetNormalColorOfTiles();
    }

    private void OnEnable()
    {
        ActionsButtons.ReturnMenu += returnToNormalWorld;
    }

    private void OnDestroy()
    {
        ActionsButtons.ReturnMenu -= returnToNormalWorld;
    }
}

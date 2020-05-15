using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

[ExecuteInEditMode]
public class TilesManager : Singleton<TilesManager>
{
    [SerializeField] List<Tile> tileReachable = new List<Tile>();

    [Tooltip("Launch the start of the code")]
    [SerializeField] bool startOrAwake;
    [Tooltip("Set To True if you want to set up tile around all tiles")]
    [SerializeField] bool checkAround = false;

    [Tooltip ("Set to true if you want to set up type of tiles")]
    [SerializeField] bool setTypeOfTiles = false;

    public List<Tile> myChild = new List<Tile>();

    [SerializeField]List<Tile> currentPath = null;



    void Start()
    {
        //ActionsButtons.Move += WhereYouCanGo;
    }


    void OnDestroy()
    {
        //ActionsButtons.Move -= WhereYouCanGo;
    }

    // Update is called once per frame
    void Update()
    {
        if (setTypeOfTiles == true)
        {
            CallAllTilesGetTypes();
        }

        if (startOrAwake == true)
        {
            startOrAwake = false;
            StartOrAwake();
        }

        if (checkAround == true)
        {
            CallAllTilesGetTylesAround();
        }
    }

    void OnValidate()
    {
       
    }

    void CallAllTilesGetTypes()
    {
        for (int i = 0; i < myChild.Count; i++)
        {
            if (myChild[i].tag == "Hexagone")
            {
                myChild[i].GetComponent<Tile>().SetTypeOfTile();
            }
        }
        setTypeOfTiles = false;
    }

    void CallAllTilesGetTylesAround()
    {
        for (int i = 0; i < myChild.Count; i++)
        {
            if (myChild[i].tag == "Hexagone")
            {
                myChild[i].GetComponent<Tile>().GetTileAround();
            }
        }
        checkAround = false;
    }

    public void SetNormalColorOfTiles()
    {
        foreach (Tile tile in myChild)
        {
            tile.SetNormalColor();
        }
    }


    void StartOrAwake()
    {
        myChild.Clear();
        for (int i = 0; i < transform.childCount; i++)
        {
            myChild.Add(transform.GetChild(i).GetComponent<Tile>());
        }
    }

    public List<Tile> GeneratePathTo(Tile start, Tile target)
    {
        Dictionary<Tile, float> distance = new Dictionary<Tile, float>();
        Dictionary<Tile, Tile> previous = new Dictionary<Tile, Tile>();

        //Setup the GameObject Not Visited Yet
        List<Tile> unvisitedTiles = new List<Tile>();

        distance[start] = 0;
        previous[start] = null;

        //Initialize everything to have inifity distance
        foreach (Tile go in myChild)
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
                if (go.tileType != Tile.typeOfTile.Blocker)
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
        foreach(Tile tile in myChild)
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
            if (go.tileType != Tile.typeOfTile.Blocker)
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
                    if (go.tileType != Tile.typeOfTile.Blocker)
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
            tile.GetComponent<MeshRenderer>().materials[1].color = Color.cyan;
        }


        Dictionary<Tile, float> distance = new Dictionary<Tile, float>();
        Dictionary<Tile, Tile> previous = new Dictionary<Tile, Tile>();

        //Setup the GameObject Not Visited Yet
        List<Tile> unvisitedTiles = new List<Tile>();

        distance[start] = 0;
        previous[start] = null;

        //Initialize everything to have inifity distance
        foreach (Tile go in myChild)
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
                if (go.GetComponent<Tile>().tileType != Tile.typeOfTile.Blocker)
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
}

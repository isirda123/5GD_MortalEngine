using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

[ExecuteInEditMode]
public class tileManager : Singleton<tileManager>
{


    [Tooltip("Launch the start of the code")]
    [SerializeField] bool startOrAwake;
    [Tooltip("Set To True if you want to set up tile around all tiles")]
    [SerializeField] bool checkAround = false;

    [Tooltip ("Set to true if you want to set up type of tiles")]
    [SerializeField] bool setTypeOfTiles = false;

    public List<tileInfos> myChild = new List<tileInfos>();

    [SerializeField]List<tileInfos> currentPath = null;


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
                myChild[i].GetComponent<tileInfos>().SetTypeOfTile();
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
                myChild[i].GetComponent<tileInfos>().GetTileAround();
            }
        }
        checkAround = false;
    }


    void StartOrAwake()
    {
        myChild.Clear();
        for (int i = 0; i < transform.childCount; i++)
        {
            myChild.Add(transform.GetChild(i).GetComponent<tileInfos>());
        }
    }

    public List<Vector3> GeneratePathTo(tileInfos start, tileInfos target)
    {
        Dictionary<tileInfos, float> distance = new Dictionary<tileInfos, float>();
        Dictionary<tileInfos, tileInfos> previous = new Dictionary<tileInfos, tileInfos>();

        //Setup the GameObject Not Visited Yet
        List<tileInfos> unvisitedTiles = new List<tileInfos>();

        distance[start] = 0;
        previous[start] = null;

        //Initialize everything to have inifity distance
        foreach (tileInfos go in myChild)
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
            tileInfos unvisitedGO = null;

            foreach (tileInfos possibleUnvisitedGO in unvisitedTiles)
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

            foreach (tileInfos go in unvisitedGO.GetComponent<tileInfos>().neighbours)
            {
                if (go.GetComponent<tileInfos>().tileType != tileInfos.typeOfTile.Blocker)
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
        foreach (tileInfos go in target.neighbours)
        {
            if (previous[go]!= null){
            }
        }
        if (previous[target] == null)
        {
            //The target is not reachable so he didn't have neighbours connected to him, or there is a gab between the start and the end
            return null;
        }

        currentPath = new List<tileInfos>();
        tileInfos currentGO = target;
        //Step through the previous chain of GameObject to create the path
        while(currentGO != null)
        {
            currentPath.Add(currentGO);
            currentGO = previous[currentGO];
        }

        //The path is invert. Going from Target to Start. Change it.
        currentPath.Reverse();
        List<Vector3> positionToGo = new List<Vector3>();
        for (int i = 0; i < currentPath.Count; i++)
        {
            positionToGo.Add(currentPath[i].transform.position);
        }

        return positionToGo;
    }
}

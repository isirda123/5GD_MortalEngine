﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.EventSystems;
using System;

public class Tile : MonoBehaviour
{
    public static event Action<Tile> TileTouched;

    public void OnMouseUp()
    {
        TileTouched?.Invoke(this);
    }

    public enum typeOfTile
    {
        None,
        Blocker,
        Mouflu,
        Rock,
        Wood,
        Berry
    }

    public enum StateOfResources
    {
        Available,
        Reloading,
    }

     public ResourcesInfos resourcesInfos = null;

    public typeOfTile tileType;

    private StateOfResources stateResources;
    public StateOfResources State { get { return stateResources; } set { SwitchState(value); } }
    private void SwitchState(StateOfResources stateFocused)
    {
        switch(stateFocused)
        {
            case StateOfResources.Available:
                break;
            case StateOfResources.Reloading:
                break;
        }
        stateResources = stateFocused;
    }

    public void DrawResourceHarvest()
    {
        GameObject popUpGo = Instantiate(GameManager.Instance.gameAssets.popUpResourceHarvest.gameObject, transform.position + transform.up, Quaternion.identity) as GameObject;
        PopUpResourceHarvest popUp = popUpGo.GetComponent<PopUpResourceHarvest>();
        popUp.SetImage(resourcesInfos);
        popUp.SetText((int)resourcesInfos.resourcesAmount);
    }

    public List<Tile> neighbours;

    float timerRespawn = 0;

    [SerializeField] GameObject visualResource;

    public bool avatarOnMe = false;

    void Update()
    {
        CheckState(stateResources);
    }

    void CheckState(StateOfResources sR)
    {
        if (sR == StateOfResources.Reloading)
        {
            ReloadTheResource();
        }
    }

    void ReloadTheResource()
    {
        if (visualResource != null)
        {
            visualResource.SetActive(false);
            if (avatarOnMe == false)
            {
                if (timerRespawn < resourcesInfos.resourcesRoundsToRespawn)
                {
                    timerRespawn += Time.deltaTime;
                }
                else
                {
                    stateResources = StateOfResources.Available;
                    VisualRespawnResource();
                    timerRespawn = 0;
                }
            }
        }
    }

    void VisualRespawnResource()
    {
        visualResource.SetActive(true);
    }

    public void GetTileAround()
    {
        neighbours = new List<Tile>();


        RaycastHit[] hits;

        //North
        //Le new vector3(0,-0.05f,0) c'est parce que sinon ça bug!
        hits = Physics.RaycastAll(transform.position + new Vector3(0, -0.05f, 0), Vector3.forward, transform.localScale.x + 0.5f);
        Debug.DrawRay(transform.position + new Vector3(0, -0.05f, 0), (Vector3.forward), Color.magenta);
        for (int i =0; i< hits.Length; i++)
        {
            RaycastHit hit = hits[i];
            if (hit.transform != this.transform)
            {
                neighbours.Add(hit.transform.GetComponent<Tile>());
            }
        }

        //North East

        hits = Physics.RaycastAll(transform.position + new Vector3(0, -0.05f, 0), Vector3.forward + Vector3.right, transform.localScale.x + 0.5f);
        Debug.DrawRay(transform.position + new Vector3(0, -0.05f, 0), (Vector3.forward + Vector3.right), Color.black);
        for (int i = 0; i < hits.Length; i++)
        {
            RaycastHit hit = hits[i];
            if (hit.transform != this.transform)
            {
                neighbours.Add(hit.transform.GetComponent<Tile>());
            }
        }


        //South East

        hits = Physics.RaycastAll(transform.position + new Vector3(0, -0.05f, 0), -Vector3.forward + Vector3.right, transform.localScale.x + 0.5f);
        Debug.DrawRay(transform.position + new Vector3(0, -0.05f, 0), (-Vector3.forward + Vector3.right), Color.yellow);
        for (int i = 0; i < hits.Length; i++)
        {
            RaycastHit hit = hits[i];
            if (hit.transform != this.transform)
            {
                neighbours.Add(hit.transform.GetComponent<Tile>());
            }
        }

        //South
        hits = Physics.RaycastAll(transform.position + new Vector3(0, -0.05f, 0), -Vector3.forward, transform.localScale.x + 0.5f);
        Debug.DrawRay(transform.position + new Vector3(0, -0.05f, 0), -Vector3.forward, Color.green);
        for (int i = 0; i < hits.Length; i++)
        {
            RaycastHit hit = hits[i];
            if (hit.transform != this.transform)
            {
                neighbours.Add(hit.transform.GetComponent<Tile>());
            }
        }

        //South West
        hits = Physics.RaycastAll(transform.position + new Vector3(0,-0.05f,0), -Vector3.forward - Vector3.right, transform.localScale.x);
        Debug.DrawRay(transform.position + new Vector3(0, -0.05f, 0), -Vector3.forward - Vector3.right, Color.red);
        for (int i = 0; i < hits.Length; i++)
        {
            
            RaycastHit hit = hits[i];
            if (hit.transform != this.transform)
            {
                neighbours.Add(hit.transform.GetComponent<Tile>());
            }
        }

        //North West
        hits = Physics.RaycastAll(transform.position + new Vector3(0, -0.05f, 0), Vector3.forward - Vector3.right, transform.localScale.x);
        Debug.DrawRay(transform.position + new Vector3(0, -0.05f, 0), (Vector3.forward - Vector3.right), Color.blue);
        for (int i = 0; i < hits.Length; i++)
        {

            RaycastHit hit = hits[i];
            if (hit.transform != this.transform)
            {
                neighbours.Add(hit.transform.GetComponent<Tile>());
            }
        }

        SetDirtyEditor();

    }

    void SetDirtyEditor()
    {
    #if UNITY_EDITOR
        EditorUtility.SetDirty(this);

    #endif
    }


    public void SetNormalColor()
    {
        switch (tileType)
        {

            case typeOfTile.None:
                GetComponent<MeshRenderer>().materials[1].color = (Resources.Load("MaterialTiles/None", typeof(Material)) as Material).color;
                break;
            case typeOfTile.Blocker:
                GetComponent<MeshRenderer>().materials[1].color = (Resources.Load("MaterialTiles/Blocker", typeof(Material)) as Material).color;
                break;
            case typeOfTile.Mouflu:
                GetComponent<MeshRenderer>().materials[1].color = (Resources.Load("MaterialTiles/Mouflu", typeof(Material)) as Material).color;
                break;
            case typeOfTile.Rock:
                GetComponent<MeshRenderer>().materials[1].color = (Resources.Load("MaterialTiles/Rock", typeof(Material)) as Material).color;
                break;
            case typeOfTile.Wood:
                GetComponent<MeshRenderer>().materials[1].color = (Resources.Load("MaterialTiles/Wood", typeof(Material)) as Material).color;
                break;
            case typeOfTile.Berry:
                GetComponent<MeshRenderer>().materials[1].color = (Resources.Load("MaterialTiles/Berry", typeof(Material)) as Material).color;
                break;
        }
    }


    public void SetTypeOfTile()
    {
        if (transform.childCount > 0)
        {
            DestroyImmediate(transform.GetChild(0).gameObject);
        }
        switch (tileType)
        {
            
            case typeOfTile.None:
                GetComponent<MeshRenderer>().materials[1].color = (Resources.Load("MaterialTiles/None", typeof(Material)) as Material).color;
                resourcesInfos = null;
                break;
            case typeOfTile.Blocker:
                GetComponent<MeshRenderer>().materials[1].color = (Resources.Load("MaterialTiles/Blocker", typeof(Material)) as Material).color;
                resourcesInfos = null;
                break;
            case typeOfTile.Mouflu:
                GetComponent<MeshRenderer>().materials[1].color = (Resources.Load("MaterialTiles/Mouflu", typeof(Material)) as Material).color;
                resourcesInfos = Resources.Load("ResourcesInfos/Mouflu", typeof(ResourcesInfos)) as ResourcesInfos;
                visualResource = Instantiate(Resources.Load("VisualResources/Mouflu", typeof(GameObject)) as GameObject, transform.position,Quaternion.identity, transform);
                break;
            case typeOfTile.Rock:
                GetComponent<MeshRenderer>().materials[1].color = (Resources.Load("MaterialTiles/Rock", typeof(Material)) as Material).color;
                resourcesInfos = Resources.Load("ResourcesInfos/Rock", typeof(ResourcesInfos)) as ResourcesInfos;
                visualResource = Instantiate(Resources.Load("VisualResources/Rock", typeof(GameObject)) as GameObject, transform.position,Quaternion.identity, transform);
                break;
            case typeOfTile.Wood:
                GetComponent<MeshRenderer>().materials[1].color = (Resources.Load("MaterialTiles/Wood", typeof(Material)) as Material).color;
                resourcesInfos = Resources.Load("ResourcesInfos/Wood", typeof(ResourcesInfos)) as ResourcesInfos;
                visualResource = Instantiate(Resources.Load("VisualResources/Wood", typeof(GameObject)) as GameObject, transform.position,Quaternion.identity, transform);
                break;
            case typeOfTile.Berry:
                GetComponent<MeshRenderer>().materials[1].color = (Resources.Load("MaterialTiles/Berry", typeof(Material)) as Material).color;
                resourcesInfos = Resources.Load("ResourcesInfos/Berry", typeof(ResourcesInfos)) as ResourcesInfos;
                visualResource = Instantiate(Resources.Load("VisualResources/Berry", typeof(GameObject)) as GameObject, transform.position,Quaternion.identity, transform);
                break;
        }

        SetDirtyEditor();
    }

}

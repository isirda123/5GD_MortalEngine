﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.EventSystems;
using System;

public class Tile : MonoBehaviour
{
    public static event Action<Tile> TileTouched;

    [HideInInspector] public bool checkedForRespawn = false;
    private int roundNbrOfDesable;
    public void OnMouseUp()
    {
        TileTouched?.Invoke(this);
    }

    public enum TypeOfTile
    {
        None,
        Blocker,
        Wood,
        Mouflu,
        Rock,
        Berry
    }

    public enum StateOfResources
    {
        Available,
        Reloading,
    }

     public ResourcesInfos resourcesInfos = null;

    public TypeOfTile tileType;

    private StateOfResources stateResources;
    public StateOfResources State { get { return stateResources; } set { SwitchState(value); } }
    private void SwitchState(StateOfResources stateFocused)
    {
        switch(stateFocused)
        {
            case StateOfResources.Available:
                DrawStateFeedBack(true);
                break;
            case StateOfResources.Reloading:
                roundNbrOfDesable = RoundManager.Instance.numberOfRound;
                DrawStateFeedBack(false);
                break;
        }
        stateResources = stateFocused;
    }

    private void DrawResourceHarvest()
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
        hits = Physics.RaycastAll(transform.position + new Vector3(0, -0.05f, 0), Vector3.forward, transform.localScale.x + 1f);
        Debug.DrawRay(transform.position + new Vector3(0, -0.05f, 0), (Vector3.forward), Color.magenta,5);
        for (int i =0; i< hits.Length; i++)
        {
            RaycastHit hit = hits[i];
            if (hit.transform != this.transform)
            {
                neighbours.Add(hit.transform.GetComponent<Tile>());
            }
        }

        //North East

        hits = Physics.RaycastAll(transform.position + new Vector3(0, -0.05f, 0), Vector3.forward + Vector3.right, transform.localScale.x + 1f);
        Debug.DrawRay(transform.position + new Vector3(0, -0.05f, 0), (Vector3.forward + Vector3.right), Color.black, 5);
        for (int i = 0; i < hits.Length; i++)
        {
            RaycastHit hit = hits[i];
            if (hit.transform != this.transform)
            {
                neighbours.Add(hit.transform.GetComponent<Tile>());
            }
        }


        //South East

        hits = Physics.RaycastAll(transform.position + new Vector3(0, -0.05f, 0), -Vector3.forward + Vector3.right, transform.localScale.x + 1f);
        Debug.DrawRay(transform.position + new Vector3(0, -0.05f, 0), (-Vector3.forward + Vector3.right), Color.yellow, 5);
        for (int i = 0; i < hits.Length; i++)
        {
            RaycastHit hit = hits[i];
            if (hit.transform != this.transform)
            {
                neighbours.Add(hit.transform.GetComponent<Tile>());
            }
        }

        //South
        hits = Physics.RaycastAll(transform.position + new Vector3(0, -0.05f, 0), -Vector3.forward, transform.localScale.x + 1f);
        Debug.DrawRay(transform.position + new Vector3(0, -0.05f, 0), -Vector3.forward, Color.green, 5);
        for (int i = 0; i < hits.Length; i++)
        {
            RaycastHit hit = hits[i];
            if (hit.transform != this.transform)
            {
                neighbours.Add(hit.transform.GetComponent<Tile>());
            }
        }

        //South West
        hits = Physics.RaycastAll(transform.position + new Vector3(0,-0.05f,0), -Vector3.forward - Vector3.right, transform.localScale.x +1f);
        Debug.DrawRay(transform.position + new Vector3(0, -0.05f, 0), -Vector3.forward - Vector3.right, Color.red, 5);
        for (int i = 0; i < hits.Length; i++)
        {
            
            RaycastHit hit = hits[i];
            if (hit.transform != this.transform)
            {
                neighbours.Add(hit.transform.GetComponent<Tile>());
            }
        }

        //North West
        hits = Physics.RaycastAll(transform.position + new Vector3(0, -0.05f, 0), Vector3.forward - Vector3.right, transform.localScale.x +1f);
        Debug.DrawRay(transform.position + new Vector3(0, -0.05f, 0), (Vector3.forward - Vector3.right), Color.blue, 5);
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

            case TypeOfTile.None:
                GetComponent<MeshRenderer>().materials[1].color = (Resources.Load("MaterialTiles/None", typeof(Material)) as Material).color;
                break;
            case TypeOfTile.Blocker:
                GetComponent<MeshRenderer>().materials[1].color = (Resources.Load("MaterialTiles/Blocker", typeof(Material)) as Material).color;
                break;
            case TypeOfTile.Mouflu:
                GetComponent<MeshRenderer>().materials[1].color = (Resources.Load("MaterialTiles/Mouflu", typeof(Material)) as Material).color;
                break;
            case TypeOfTile.Rock:
                GetComponent<MeshRenderer>().materials[1].color = (Resources.Load("MaterialTiles/Rock", typeof(Material)) as Material).color;
                break;
            case TypeOfTile.Wood:
                GetComponent<MeshRenderer>().materials[1].color = (Resources.Load("MaterialTiles/Wood", typeof(Material)) as Material).color;
                break;
            case TypeOfTile.Berry:
                GetComponent<MeshRenderer>().materials[1].color = (Resources.Load("MaterialTiles/Berry", typeof(Material)) as Material).color;
                break;
        }
    }


    public void DrawStateFeedBack(bool isActive)
    {
        if (isActive)
        {
            if (Application.isPlaying == true)
            {
                Destroy(transform.GetChild(0).gameObject);
            }
            else
            {
                DestroyImmediate(transform.GetChild(0).gameObject);
            }
        }
        else
        {
            switch (tileType)
            {
                case TypeOfTile.None:
                    GetComponent<MeshRenderer>().sharedMaterials[1].color = (Resources.Load("MaterialTiles/None", typeof(Material)) as Material).color;
                    resourcesInfos = null;
                    break;
                case TypeOfTile.Blocker:
                    GetComponent<MeshRenderer>().sharedMaterials[1].color = (Resources.Load("MaterialTiles/Blocker", typeof(Material)) as Material).color;
                    resourcesInfos = null;
                    break;
                case TypeOfTile.Mouflu:
                    GetComponent<MeshRenderer>().sharedMaterials[1].color = (Resources.Load("MaterialTiles/Mouflu", typeof(Material)) as Material).color;
                    resourcesInfos = Resources.Load("ResourcesInfos/Mouflu", typeof(ResourcesInfos)) as ResourcesInfos;
                    visualResource = Instantiate(Resources.Load("VisualResources/Mouflu", typeof(GameObject)) as GameObject, transform.position, Quaternion.identity, transform);
                    break;
                case TypeOfTile.Rock:
                    GetComponent<MeshRenderer>().sharedMaterials[1].color = (Resources.Load("MaterialTiles/Rock", typeof(Material)) as Material).color;
                    resourcesInfos = Resources.Load("ResourcesInfos/Rock", typeof(ResourcesInfos)) as ResourcesInfos;
                    visualResource = Instantiate(Resources.Load("VisualResources/Rock", typeof(GameObject)) as GameObject, transform.position, Quaternion.identity, transform);
                    break;
                case TypeOfTile.Wood:
                    GetComponent<MeshRenderer>().sharedMaterials[1].color = (Resources.Load("MaterialTiles/Wood", typeof(Material)) as Material).color;
                    resourcesInfos = Resources.Load("ResourcesInfos/Wood", typeof(ResourcesInfos)) as ResourcesInfos;
                    visualResource = Instantiate(Resources.Load("VisualResources/Wood", typeof(GameObject)) as GameObject, transform.position, Quaternion.identity, transform);
                    break;
                case TypeOfTile.Berry:
                    GetComponent<MeshRenderer>().sharedMaterials[1].color = (Resources.Load("MaterialTiles/Berry", typeof(Material)) as Material).color;
                    resourcesInfos = Resources.Load("ResourcesInfos/Berry", typeof(ResourcesInfos)) as ResourcesInfos;
                    visualResource = Instantiate(Resources.Load("VisualResources/Berry", typeof(GameObject)) as GameObject, transform.position, Quaternion.identity, transform);
                    break;
            }
        }
        SetDirtyEditor();
    }


    private void Regrow()
    {
        if (roundNbrOfDesable - RoundManager.Instance.numberOfRound > resourcesInfos.nbrOfTurnsToRegrow)
        {
            State = StateOfResources.Available;
        }
    }

    private void Start()
    {
        RoundManager.RoundStart += Regrow;
    }
    private void OnDestroy()
    {
        RoundManager.RoundStart -= Regrow;
    }
}

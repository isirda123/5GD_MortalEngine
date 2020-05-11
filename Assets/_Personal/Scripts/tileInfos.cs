using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class tileInfos : MonoBehaviour
{
    public enum typeOfTile
    {
        None,
        Blocker,
        Mouflu,
        Rock,
        Wood,
        Berry
    }

    public ResourcesInfos resourcesInfos = null;

    public typeOfTile tileType;

    public List<tileInfos> neighbours;


    // Start is called before the first frame update
    void Start()
    { 
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void GetTileAround()
    {
        neighbours = new List<tileInfos>();


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
                neighbours.Add(hit.transform.GetComponent<tileInfos>());
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
                neighbours.Add(hit.transform.GetComponent<tileInfos>());
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
                neighbours.Add(hit.transform.GetComponent<tileInfos>());
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
                neighbours.Add(hit.transform.GetComponent<tileInfos>());
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
                neighbours.Add(hit.transform.GetComponent<tileInfos>());
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
                neighbours.Add(hit.transform.GetComponent<tileInfos>());
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

    public void SetTypeOfTile()
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
                resourcesInfos = Resources.Load("ResourcesInfos/Mouflu", typeof(ResourcesInfos)) as ResourcesInfos;
                break;
            case typeOfTile.Rock:
                GetComponent<MeshRenderer>().materials[1].color = (Resources.Load("MaterialTiles/Rock", typeof(Material)) as Material).color;
                resourcesInfos = Resources.Load("ResourcesInfos/Rock", typeof(ResourcesInfos)) as ResourcesInfos;
                break;
            case typeOfTile.Wood:
                GetComponent<MeshRenderer>().materials[1].color = (Resources.Load("MaterialTiles/Wood", typeof(Material)) as Material).color;
                resourcesInfos = Resources.Load("ResourcesInfos/Wood", typeof(ResourcesInfos)) as ResourcesInfos;
                break;
            case typeOfTile.Berry:
                GetComponent<MeshRenderer>().materials[1].color = (Resources.Load("MaterialTiles/Berry", typeof(Material)) as Material).color;
                resourcesInfos = Resources.Load("ResourcesInfos/Berry", typeof(ResourcesInfos)) as ResourcesInfos;
                break;
        }

        SetDirtyEditor();
    }

}

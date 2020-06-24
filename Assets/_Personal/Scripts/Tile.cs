using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.EventSystems;
using System;

public class Tile : MonoBehaviour
{
    public static event Action<Tile> TileTouched;

    [HideInInspector] public bool checkedForRespawn = false;
    public int roundNbrOfDesable;

    Vector3 basePosition;
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
        Berry,
        Water
    }

    public enum StateOfResources
    {
        Available,
        Reloading,
    }

    public ResourcesInfos resourcesInfos = null;

    public TypeOfTile tileType;

    public StateOfResources stateResources;
    public StateOfResources State { get { return stateResources; } set { SwitchState(value); } }
    private void SwitchState(StateOfResources stateFocused)
    {
        switch(stateFocused)
        {
            case StateOfResources.Available:
                //DrawStateFeedBack(true);
                break;
            case StateOfResources.Reloading:
                roundNbrOfDesable = RoundManager.Instance.numberOfRound;
                // DrawStateFeedBack(false);
                SpriteRenderer sR = this.transform.GetChild(0).GetChild(0).GetComponent<SpriteRenderer>();
                sR.sprite = null;
                break;
        }
        stateResources = stateFocused;
    }



    public List<Tile> neighbours;

    float timerRespawn = 0;

    [SerializeField] GameObject visualResource;

    public bool avatarOnMe = false;

    public bool reachable = false;

    void Start()
    {
        basePosition = this.transform.position;
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

    public void DrawResourceHarvest()
    {
        GameObject popUpGo = Instantiate(GameManager.Instance.gameAssets.popUpResourceHarvest.gameObject, transform.position + (transform.up*0.5f), Quaternion.Euler(45,0,0)) as GameObject;
        PopUpResourceHarvest popUp = popUpGo.GetComponent<PopUpResourceHarvest>();
        popUp.SetImage(resourcesInfos);
        if (resourcesInfos.resourceType == GameManager.ResourceType.Mouflu)
        {
            popUp.SetText((int)resourcesInfos.resourcesAmount + DecretManager.Instance.totalDecreeInfos.collectQuantityMouflu);
        }
        else if (resourcesInfos.resourceType == GameManager.ResourceType.Rock)
        {
            popUp.SetText((int)resourcesInfos.resourcesAmount + DecretManager.Instance.totalDecreeInfos.collectQuantityRock);
        }
        else if (resourcesInfos.resourceType == GameManager.ResourceType.Wood)
        {
            popUp.SetText((int)resourcesInfos.resourcesAmount + DecretManager.Instance.totalDecreeInfos.collectQuantityWood);
        }
        else if (resourcesInfos.resourceType == GameManager.ResourceType.Berry)
        {
            popUp.SetText((int)resourcesInfos.resourcesAmount + DecretManager.Instance.totalDecreeInfos.collectQuantityBerry);
        }
    }


    public void SetNormalColor()
    {
        GetComponent<MeshRenderer>().materials[0] = GameManager.Instance.gameAssets.border;
        GetComponent<MeshRenderer>().materials[1] = GameManager.Instance.gameAssets.center;
        switch (tileType)
        {
            case TypeOfTile.None:
                GetComponent<MeshRenderer>().materials[1].color = (Resources.Load("MaterialTiles/None", typeof(Material)) as Material).color;
                break;
            case TypeOfTile.Blocker:
                GetComponent<MeshRenderer>().materials[1].color = (Resources.Load("MaterialTiles/Blocker", typeof(Material)) as Material).color;
                break;
            case TypeOfTile.Mouflu:
                GetComponent<MeshRenderer>().materials[1].color = (Resources.Load("MaterialTiles/Floor", typeof(Material)) as Material).color;
                break;
            case TypeOfTile.Rock:
                GetComponent<MeshRenderer>().materials[1].color = (Resources.Load("MaterialTiles/Floor", typeof(Material)) as Material).color;
                break;
            case TypeOfTile.Wood:
                GetComponent<MeshRenderer>().materials[1].color = (Resources.Load("MaterialTiles/Floor", typeof(Material)) as Material).color;
                break;
            case TypeOfTile.Berry:
                GetComponent<MeshRenderer>().materials[1].color = (Resources.Load("MaterialTiles/Floor", typeof(Material)) as Material).color;
                break;
            case TypeOfTile.Water:
                GetComponent<MeshRenderer>().materials[1].color = (Resources.Load("MaterialTiles/Water", typeof(Material)) as Material).color;
                break;
        }
    }

    public void drawOffsetTile(bool enable)
    {
        if (enable == false)
        {
            this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y - TilesManager.Instance.offSetTile, this.transform.position.z);
        }
        else
        {
            this.transform.position = basePosition;
        }
    }

    public void DrawDisableTile()
    {
        GetComponent<MeshRenderer>().materials[0] = GameManager.Instance.gameAssets.border;
        GetComponent<MeshRenderer>().materials[1] = GameManager.Instance.gameAssets.center;
        switch (tileType)
        {
            case TypeOfTile.None:
                GetComponent<MeshRenderer>().materials[1].color = (Resources.Load("MaterialTiles/NoneDisable", typeof(Material)) as Material).color;
                break;
            case TypeOfTile.Blocker:
                GetComponent<MeshRenderer>().materials[1].color = (Resources.Load("MaterialTiles/BlockerDisable", typeof(Material)) as Material).color;
                break;
            case TypeOfTile.Mouflu:
                GetComponent<MeshRenderer>().materials[1].color = (Resources.Load("MaterialTiles/FloorDisable", typeof(Material)) as Material).color;
                break;
            case TypeOfTile.Rock:
                GetComponent<MeshRenderer>().materials[1].color = (Resources.Load("MaterialTiles/FloorDisable", typeof(Material)) as Material).color;
                break;
            case TypeOfTile.Wood:
                GetComponent<MeshRenderer>().materials[1].color = (Resources.Load("MaterialTiles/FloorDisable", typeof(Material)) as Material).color;
                break;
            case TypeOfTile.Berry:
                GetComponent<MeshRenderer>().materials[1].color = (Resources.Load("MaterialTiles/FloorDisable", typeof(Material)) as Material).color;
                break;
            case TypeOfTile.Water:
                GetComponent<MeshRenderer>().materials[1].color = (Resources.Load("MaterialTiles/Water", typeof(Material)) as Material).color;
                break;
        }
    }

    public void DrawRandomVisualOnNoneTile()
    {
        if (tileType == TypeOfTile.None)
        {
            if (transform.childCount != 0)
            {
                DestroyImmediate(transform.GetChild(0).gameObject);
            }

            int random = UnityEngine.Random.Range(0, TilesManager.Instance.visualForNoneTile.Length);

            Vector3 offset = new Vector3(UnityEngine.Random.Range(-0.3f, 0.3f), 0, UnityEngine.Random.Range(-0.4f, 0.4f));
            visualResource = Instantiate(TilesManager.Instance.visualForNoneTile[random], transform.position + offset + (Vector3.up * 0.2f), Quaternion.identity, transform);
            visualResource.transform.Rotate(45, 0, 0);

        }
    }

    public void DrawRandomVisualOnWaterTile()
    {
        if (tileType == TypeOfTile.Water)
        {
            if (transform.childCount != 0)
            {
                DestroyImmediate(transform.GetChild(0).gameObject);
            }

            int random = UnityEngine.Random.Range(0, TilesManager.Instance.visualForWaterTile.Length);

            Vector3 offset = new Vector3(UnityEngine.Random.Range(-0.3f, 0.3f), 0, UnityEngine.Random.Range(-0.4f, 0.4f));
            visualResource = Instantiate(TilesManager.Instance.visualForWaterTile[random], transform.position + offset + (Vector3.up*0.2f), Quaternion.identity, transform);
            visualResource.transform.Rotate(45, 0, 0);

        }
    }

    public void DrawVisualTile()
    {
        if (transform.childCount != 0)
        {
            DestroyImmediate(transform.GetChild(0).gameObject);
        }

        GetComponent<MeshRenderer>().materials[0].shader = Shader.Find("Unlit/Color");
        GetComponent<MeshRenderer>().materials[1].shader = Shader.Find("Unlit/Color");
        switch (tileType)
        {
            case TypeOfTile.None:
                GetComponent<MeshRenderer>().materials[1].color = (Resources.Load("MaterialTiles/None", typeof(Material)) as Material).color;
                resourcesInfos = null;
                break;
            case TypeOfTile.Blocker:
                GetComponent<MeshRenderer>().materials[1].color = (Resources.Load("MaterialTiles/Blocker", typeof(Material)) as Material).color;
                visualResource = Instantiate(Resources.Load("VisualResources/Mountain", typeof(GameObject)) as GameObject, transform.position + (Vector3.up * 0.2f), Quaternion.identity, transform);
                resourcesInfos = null;
                break;
            case TypeOfTile.Mouflu:
                GetComponent<MeshRenderer>().materials[1].color = (Resources.Load("MaterialTiles/Floor", typeof(Material)) as Material).color;
                resourcesInfos = Resources.Load("ResourcesInfos/Mouflu", typeof(ResourcesInfos)) as ResourcesInfos;
                visualResource = Instantiate(Resources.Load("VisualResources/Mouflu", typeof(GameObject)) as GameObject, transform.position + (Vector3.up * 0.2f), Quaternion.identity, transform);
                break;
            case TypeOfTile.Rock:
                GetComponent<MeshRenderer>().materials[1].color = (Resources.Load("MaterialTiles/Floor", typeof(Material)) as Material).color;
                resourcesInfos = Resources.Load("ResourcesInfos/Rock", typeof(ResourcesInfos)) as ResourcesInfos;
                visualResource = Instantiate(Resources.Load("VisualResources/Rock", typeof(GameObject)) as GameObject, transform.position + (Vector3.up * 0.2f), Quaternion.identity, transform);
                break;
            case TypeOfTile.Wood:
                GetComponent<MeshRenderer>().materials[1].color = (Resources.Load("MaterialTiles/Floor", typeof(Material)) as Material).color;
                resourcesInfos = Resources.Load("ResourcesInfos/Wood", typeof(ResourcesInfos)) as ResourcesInfos;
                visualResource = Instantiate(Resources.Load("VisualResources/Wood", typeof(GameObject)) as GameObject, transform.position + (Vector3.up * 0.2f), Quaternion.identity, transform);
                break;
            case TypeOfTile.Berry:
                GetComponent<MeshRenderer>().materials[1].color = (Resources.Load("MaterialTiles/Floor", typeof(Material)) as Material).color;
                resourcesInfos = Resources.Load("ResourcesInfos/Berry", typeof(ResourcesInfos)) as ResourcesInfos;
                visualResource = Instantiate(Resources.Load("VisualResources/Berry", typeof(GameObject)) as GameObject, transform.position + (Vector3.up * 0.2f), Quaternion.identity, transform);
                break;
            case TypeOfTile.Water:
                GetComponent<MeshRenderer>().materials[1].color = (Resources.Load("MaterialTiles/Water", typeof(Material)) as Material).color;
                resourcesInfos = null;
                break;
        }
    }

    private void DrawStateFeedBack(bool isActive) => transform.GetChild(0).gameObject.SetActive(isActive);



    public bool CheckForSameTypeAround(List<Tile>neighbours)
    {
        bool sameType = false;
        foreach (Tile tile in neighbours)
        {
            if (tile.tileType == this.tileType)
            {
                
                if (tile.State == StateOfResources.Available)
                    sameType = true;
            }
        }

        return sameType;
    }

    private void Regrow()
    {
        if (resourcesInfos != null && State != StateOfResources.Available)
        {
            if (this.avatarOnMe == true)
            {
                roundNbrOfDesable = RoundManager.Instance.numberOfRound;
            }
            bool neighbourWithSameType = CheckForSameTypeAround(neighbours);
            
            if (neighbourWithSameType == false)
            {
                if (State == StateOfResources.Reloading)
                {
                    tileType = TypeOfTile.None;
                    DrawVisualTile();
                }
            }
            if (resourcesInfos != null)
            {
                if (RoundManager.Instance.numberOfRound - roundNbrOfDesable >= resourcesInfos.nbrOfTurnsToRegrow)
                {
                    State = StateOfResources.Available;
                    SpriteRenderer sR = this.transform.GetChild(0).GetChild(0).GetComponent<SpriteRenderer>();
                    if (tileType != TypeOfTile.Mouflu && tileType != TypeOfTile.Rock)
                        sR.sprite = resourcesInfos.visualOfRegrowingResource[RoundManager.Instance.numberOfRound - roundNbrOfDesable];
                }
                else
                {
                    SpriteRenderer sR= this.transform.GetChild(0).GetChild(0).GetComponent<SpriteRenderer>();
                    if (tileType != TypeOfTile.Mouflu && tileType != TypeOfTile.Rock)
                        sR.sprite = resourcesInfos.visualOfRegrowingResource[RoundManager.Instance.numberOfRound - roundNbrOfDesable];
                }
            }
        }
    }

    private void OnEnable()
    {
        RoundManager.RoundStart += Regrow;
    }

    private void OnDisable()
    {
        RoundManager.RoundStart -= Regrow;
    }
}

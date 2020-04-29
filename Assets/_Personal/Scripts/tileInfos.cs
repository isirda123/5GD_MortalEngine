using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public typeOfTile tileType;

    public List<GameObject> tileAround = new List<GameObject>();


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
        tileAround.Clear();
        /*Collider[] colliders = Physics.OverlapSphere(transform.position, (transform.localScale.x * 1f) + 0.1f);
        
        for (int i =0; i < colliders.Length; i++)
        {
            tileAround.Add(colliders[i].gameObject);
        }*/


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
                tileAround.Add(hit.transform.gameObject);
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
                tileAround.Add(hit.transform.gameObject);
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
                tileAround.Add(hit.transform.gameObject);
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
                tileAround.Add(hit.transform.gameObject);
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
                tileAround.Add(hit.transform.gameObject);
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
                tileAround.Add(hit.transform.gameObject);
            }
        }




        /*if (Physics.RaycastAll (transform.position, Vector3.forward, out hit, transform.localScale.x + 0.5f,))
        {
            Debug.DrawRay(transform.position, Vector3.forward, Color.red);
            if (hit.transform != this.transform)
            {
                if (hit.transform.tag == "Hexagone")
                {
                    tileAround.Add(hit.transform.gameObject);
                    print("oui");


                }
            }
        }
        //North East
        if (Physics.Raycast(transform.position, Vector3.forward+Vector3.right, out hit[], transform.localScale.x + 0.5f))
        {
            if (hit.transform != this.transform)
            {
                Debug.DrawRay(transform.position, Vector3.forward + Vector3.right, Color.blue);
                if (hit.transform.tag == "Hexagone")
                {
                    tileAround.Add(hit.transform.gameObject);
                    print("oui");

                }
            }
        }

        //South East
        if (Physics.Raycast(transform.position, -Vector3.forward + Vector3.right, out hit[], transform.localScale.x + 0.5f))
        {
            Debug.DrawRay(transform.position, -Vector3.forward + Vector3.right, Color.green);
            if (hit.transform != this.transform)
            {
                if (hit.transform.tag == "Hexagone")
                {
                    tileAround.Add(hit.transform.gameObject);
                    print("oui");

                }
            }
        }

        //South
        if (Physics.Raycast(transform.position, -Vector3.forward, out hit[], transform.localScale.x + 0.5f))
        {
            Debug.DrawRay(transform.position, -Vector3.forward, Color.yellow);
            if (hit.transform != this.transform)
            {
                if (hit.transform.tag == "Hexagone")
                {
                    tileAround.Add(hit.transform.gameObject);
                    print("oui");
                }
            }
        }

        //South West
        if (Physics.Raycast(transform.position, -Vector3.forward - Vector3.right, out hit[], transform.localScale.x + 0.5f))
        {
            Debug.DrawRay(transform.position, -Vector3.forward - Vector3.right, Color.magenta);
            if (hit.transform != this.transform)
            {
                if (hit.transform.tag == "Hexagone")
                {
                    tileAround.Add(hit.transform.gameObject);
                    print("oui");

                }
            }
        }

        //North West

        if (Physics.Raycast(transform.position, Vector3.forward - Vector3.right, out hit[], transform.localScale.x + 0.5f))
        {
            Debug.DrawRay(transform.position, Vector3.forward - Vector3.right, Color.red);
            if (hit.transform != this.transform)
            {
                if (hit.transform.tag == "Hexagone")
                {
                    tileAround.Add(hit.transform.gameObject);
                    print("oui");
                }
            }
        }*/
    }

    public void SetTypeOfTile()
    {

        switch (tileType)
        {
            case typeOfTile.None:
                GetComponent<MeshRenderer>().materials[1].color = (Resources.Load("MaterialTiles/None", typeof(Material)) as Material).color;
                break;
            case typeOfTile.Blocker:
                print("Blocker");
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

}

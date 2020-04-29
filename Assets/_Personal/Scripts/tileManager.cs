using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class tileManager : MonoBehaviour
{


    [Tooltip("Launch the start of the code")]
    [SerializeField] bool startOrAwake;
    [Tooltip("Set To True if you want to set up tile around all tiles")]
    [SerializeField] bool checkAround = false;

    [Tooltip ("Set to true if you want to set up type of tiles")]
    [SerializeField] bool setTypeOfTiles = false;

    public List<GameObject> myChild = new List<GameObject>();
    // Start is called before the first frame update
    void Awake()
    {
        
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
            myChild.Add(transform.GetChild(i).gameObject);
        }
    }
}

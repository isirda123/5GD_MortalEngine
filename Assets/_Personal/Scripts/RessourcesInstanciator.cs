using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RessourcesInstanciator : MonoBehaviour
{
    public List<GameObject> allRessources = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        for (int i =0; i < transform.childCount; i++)
        {
            allRessources.Add(transform.GetChild(i).gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator RespawnOfRessources(int timeToRespawn, GameObject objectToRespawn)
    {
        yield return new WaitForSeconds (timeToRespawn);
        objectToRespawn.SetActive(true);
        GameObject.Find("Fortress").GetComponent<CharaAvatar>().doItOneTime = false;
    }



}

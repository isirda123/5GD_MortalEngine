using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Linq;

public class CharaAvatar : MonoBehaviour
{
    [System.Serializable] public struct distanceRessource
    {
        public GameObject objectToCollect;
        public float distanceOfObject;
    }
    public List<distanceRessource> ressourcePos = new List<distanceRessource>();

    distanceRessource bufferPosInList = new distanceRessource();

    [Header("Work")]
    [SerializeField] RessourcesInstanciator respawner;
    [SerializeField] public GameObject workZone;
    [SerializeField] float rangeWorkZone;
    public bool mining = false;
    float timeToMineAll;
    [HideInInspector] public float miningTime =0;
    [HideInInspector] public bool stopped;
    [SerializeField] bool doItOneTime = false;
    bool findThePos = false;
    [Tooltip ("In Seconde")]
    [SerializeField] float timeBeforeVictory;

    [Header ("UI")]
    [SerializeField] GameObject mineCanvas;
    [SerializeField] Text woodText;
    [SerializeField] Text chickenText;
    [SerializeField] Text cornText;
    [SerializeField] Text rockText;
    [SerializeField] Text timingTime;
    [SerializeField] public Text buttonText;

    [SerializeField] Collider[] hitColliders;

    




    // Start is called before the first frame update
    void Start()
    {
        workZone.transform.localScale = new Vector3( rangeWorkZone/transform.localScale.x, rangeWorkZone / transform.localScale.x, rangeWorkZone / transform.localScale.x);
    }

    // Update is called once per frame
    void Update()
    {

        if (mining == true)
        {
            Mine();
            StartCoroutine(CheckForStability());
        }


        if (stopped == true)
        {
            if (doItOneTime == false)
            {
                mineCanvas.SetActive(true);
                timeToMineAll = 0;
                hitColliders = Physics.OverlapSphere(transform.position, rangeWorkZone / 2, 1 << 8);

                int wood = 0;
                int chicken = 0;
                int corn = 0;
                int rock = 0;
                for (int i = 0; i < hitColliders.Length; i++)
                {
                    findThePos = false;
                    if (hitColliders[i].GetComponent<RessourcesInfos>().name == GameManager.ResourceType.Wood)
                    {
                        wood += hitColliders[i].GetComponent<RessourcesInfos>().resourcesAmount;
                        timeToMineAll += hitColliders[i].GetComponent<RessourcesInfos>().resourcesTimeToMine;
                    }
                    else if (hitColliders[i].GetComponent<RessourcesInfos>().name == GameManager.ResourceType.Chicken)
                    {
                        chicken += hitColliders[i].GetComponent<RessourcesInfos>().resourcesAmount;
                        timeToMineAll += hitColliders[i].GetComponent<RessourcesInfos>().resourcesTimeToMine;
                    }
                    else if (hitColliders[i].GetComponent<RessourcesInfos>().name == GameManager.ResourceType.Corn)
                    {
                        corn += hitColliders[i].GetComponent<RessourcesInfos>().resourcesAmount;
                        timeToMineAll += hitColliders[i].GetComponent<RessourcesInfos>().resourcesTimeToMine;
                    }
                    else if (hitColliders[i].GetComponent<RessourcesInfos>().name == GameManager.ResourceType.Rock)
                    {
                        rock += hitColliders[i].GetComponent<RessourcesInfos>().resourcesAmount;
                        timeToMineAll += hitColliders[i].GetComponent<RessourcesInfos>().resourcesTimeToMine;
                    }

                    
                }
                woodText.text = "Wood : " + wood.ToString();
                chickenText.text = "Chicken : " + chicken.ToString();
                cornText.text = "Corn : " + corn.ToString();
                rockText.text = "Rock : " + rock.ToString();
                timingTime.text = "Time to mine : " + timeToMineAll.ToString();
                doItOneTime = true;
                print("Jusy Stopped");


                CheckForPos();
            }
        }
        else
        {
            mineCanvas.SetActive(false);
            doItOneTime = false;
        }
    }


    void CheckForPos()
    {
        distanceRessource buffer;
        ressourcePos.Clear();
        buffer.objectToCollect = null;
        buffer.distanceOfObject = 0;
        List<Collider> bufferCollider = new List<Collider>();
        bufferCollider = hitColliders.ToList();
        Collider whatToDestroy = null;

        for (int i =0; i< hitColliders.Length; i++)
        {
            for (int j =0; j < bufferCollider.Count; j++)
            {
                if (j == 0)
                {
                    buffer.objectToCollect = bufferCollider[j].gameObject;
                    buffer.distanceOfObject = Vector3.Distance(bufferCollider[j].transform.position, transform.position);
                    whatToDestroy = bufferCollider[j];
                }
                else
                {
                    float newDistance = Vector3.Distance(bufferCollider[j].transform.position, transform.position);
                    if (buffer.distanceOfObject > newDistance)
                    {
                        buffer.objectToCollect = bufferCollider[j].gameObject;
                        buffer.distanceOfObject = newDistance;
                        whatToDestroy = bufferCollider[j];
                    }
                }
                
            }
            ressourcePos.Add(buffer);
            bufferCollider.Remove(whatToDestroy);

            
        }
    }

    void Mine()
    {
        buttonText.text = "In Progress : " + (100*(miningTime / bufferPosInList.objectToCollect.GetComponent<RessourcesInfos>().resourcesTimeToMine)).ToString("0.0");
        miningTime += Time.deltaTime;
        if (miningTime/ bufferPosInList.objectToCollect.GetComponent<RessourcesInfos>().resourcesTimeToMine > 1)
        {
            bufferPosInList.objectToCollect.SetActive(false);
            StartCoroutine(respawner.RespawnOfRessources(bufferPosInList.objectToCollect.GetComponent<RessourcesInfos>().resourcesTimeToRespawn, bufferPosInList.objectToCollect));
            GameManager.Instance.ReturnResourceInStock(bufferPosInList.objectToCollect.GetComponent<RessourcesInfos>().name).numberInStock += bufferPosInList.objectToCollect.GetComponent<RessourcesInfos>().resourcesAmount;
            miningTime = 0;
            
            ressourcePos.Remove(bufferPosInList);

            if (ressourcePos.Count > 0)
            {
                bufferPosInList = ressourcePos[0];
            }
            else
            {
                buttonText.text = "Begin";
                
                mining = false;
            }
            doItOneTime = false;
            //destroyMinedObject();
        }
    }



    IEnumerator CheckForStability()
    {
        float woodInStock = GameManager.Instance.ReturnResourceInStock(GameManager.ResourceType.Wood).numberInStock;
        float chickenInStock = GameManager.Instance.ReturnResourceInStock(GameManager.ResourceType.Chicken).numberInStock;
        float cornInStock = GameManager.Instance.ReturnResourceInStock(GameManager.ResourceType.Corn).numberInStock;
        float rockInStock = GameManager.Instance.ReturnResourceInStock(GameManager.ResourceType.Rock).numberInStock;

        yield return new WaitForSeconds(timeBeforeVictory);

        if (woodInStock <= GameManager.Instance.ReturnResourceInStock(GameManager.ResourceType.Wood).numberInStock &&
            chickenInStock <= GameManager.Instance.ReturnResourceInStock(GameManager.ResourceType.Chicken).numberInStock &&
            cornInStock <= GameManager.Instance.ReturnResourceInStock(GameManager.ResourceType.Corn).numberInStock && 
            rockInStock <= GameManager.Instance.ReturnResourceInStock(GameManager.ResourceType.Rock).numberInStock)
        {
            print("Victory");
        }
        else
        {
            print("Try Again");
        }
    }

    void destroyMinedObject()
    {
        for (int i =0; i< hitColliders.Length; i++)
        {
            hitColliders[i].gameObject.SetActive(false);
            StartCoroutine(respawner.RespawnOfRessources(hitColliders[i].GetComponent<RessourcesInfos>().resourcesTimeToRespawn, hitColliders[i].gameObject));
            GameManager.Instance.ReturnResourceInStock(hitColliders[i].GetComponent<RessourcesInfos>().name).numberInStock += hitColliders[i].GetComponent<RessourcesInfos>().resourcesAmount;
            buttonText.text = "Begin";
        }
    }


    public void BeginMining()
    {
        if (mining == true)
        {
            mining = false;
            miningTime = 0;
            buttonText.text = "Begin";
        }
        else
        {
            mining = true;
            if (ressourcePos.Count >0)
                bufferPosInList = ressourcePos[0];
        }
    }


    void OnTriggerEnter (Collider collider)
    {
        if (collider.transform.tag == "Resources")
        {
            if (Vector3.Distance(collider.transform.position, transform.position) < this.transform.localScale.x)
            {
                collider.gameObject.SetActive(false);
                StartCoroutine(respawner.RespawnOfRessources(collider.GetComponent<RessourcesInfos>().resourcesTimeToRespawn, collider.gameObject));
            }
        }
    }
}

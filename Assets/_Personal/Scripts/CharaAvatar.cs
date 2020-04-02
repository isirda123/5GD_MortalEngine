using UnityEngine;
using UnityEngine.UI;

public class CharaAvatar : MonoBehaviour
{
    [Tooltip("Work")]
    [SerializeField] RessourcesInstanciator respawner;
    [SerializeField] public GameObject workZone;
    [SerializeField] float rangeWorkZone;
    public bool mining = false;
    float timeToMineAll;
    float miningTime =0;
    [HideInInspector] public bool stopped;


    [Header ("UI")]
    [SerializeField] GameObject mineCanvas;
    [SerializeField] Text woodText;
    [SerializeField] Text chickenText;
    [SerializeField] Text cornText;
    [SerializeField] Text rockText;
    [SerializeField] Text timingTime;
    [SerializeField] Text buttonText;

    [SerializeField] Collider[] hitColliders;




    // Start is called before the first frame update
    void Start()
    {
        workZone.transform.localScale = new Vector3( rangeWorkZone, rangeWorkZone, rangeWorkZone);
    }

    // Update is called once per frame
    void Update()
    {

        if (mining == true)
        {
            Mine();
        }


        if (stopped == true)
        {
            mineCanvas.SetActive(true);
            timeToMineAll = 0;
            hitColliders = Physics.OverlapSphere(transform.position, (this.transform.localScale.x * workZone.transform.localScale.x) / 2, 1 << 8);

            int wood = 0;
            int chicken = 0;
            int corn = 0;
            int rock = 0;
            for (int i = 0; i < hitColliders.Length; i++)
            {
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

                woodText.text = "Wood : " + wood.ToString();
                chickenText.text = "Chicken : " + chicken.ToString();
                cornText.text = "Corn : " + corn.ToString();
                rockText.text = "Rock : " + rock.ToString();
                timingTime.text = "Time to mine : " + timeToMineAll.ToString();


            }
        }
        else
        {
            mineCanvas.SetActive(false);
        }
    }

    void Mine()
    {
        buttonText.text = "In Progress : " + (100*(miningTime / timeToMineAll)).ToString("0.0");
        miningTime += Time.deltaTime;
        if (miningTime/timeToMineAll > 1)
        {
            mining = false;
            miningTime = 0;
            destroyMinedObject();
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
        mining = true;
        
    }
}

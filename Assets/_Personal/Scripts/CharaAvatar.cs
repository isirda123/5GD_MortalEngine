using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Linq;
using DG.Tweening;
public class CharaAvatar : MonoBehaviour
{
    [Header("Work")]
    [SerializeField] public GameObject workZone;
    [SerializeField] tileInfos tileStandingOn;
    [SerializeField] float rangeWorkZone;
    float timeToMineAll;
    [HideInInspector] public bool stopped;
    [SerializeField] public bool doItOneTime = false;
    bool findThePos = false;
    [Tooltip("In Seconde")]
    [SerializeField] float timeBeforeVictory;
    public float speedOfMove;

    [Header("UI")]
    [SerializeField] GameObject mineCanvas;
    [SerializeField] Text woodText;
    [SerializeField] Text chickenText;
    [SerializeField] Text cornText;
    [SerializeField] Text rockText;
    [SerializeField] Text timingTime;
    [SerializeField] public Text buttonText;

    [SerializeField] Collider[] hitColliders;
    [SerializeField] tileManager pathManager;
    DG.Tweening.Sequence sequence;

    public enum CharacterState
    {
        Moving,
        Mining,
    }
    private CharacterState actualState;

    public CharacterState State
    { get { return actualState; } set { SwitchState(value); } }

    private float startMiningTime;

    private tileInfos resourceFocused;

    private void SwitchState(CharacterState focusState)
    {
        switch (focusState)
        {
            case CharacterState.Moving:
                buttonText.text = "Begin";
                actualState = CharacterState.Moving;
                break;
            case CharacterState.Mining:
                // feedbacks (light target resource etc)
                //set variables for mine
                startMiningTime = Time.time;

                //Collider[] hitColliders = Physics.OverlapSphere(transform.position, rangeWorkZone / 2, 1 << 8);
                //get resources around & set mine text & check level validation

                List<tileInfos> neighbours = new List<tileInfos>();
                foreach (tileInfos tI in tileStandingOn.neighbours)
                {
                    if (tI.stateResources == tileInfos.stateOfResources.Available && tI.tileType != tileInfos.typeOfTile.Blocker && tI.tileType != tileInfos.typeOfTile.None)
                    {
                        neighbours.Add(tI);
                    }
                }
                print(neighbours.Count);
                List<tileInfos> resourcesInRange = GetResourcesAround(neighbours);
                if (neighbours.Count > 0)
                {
                    SetMineText(resourcesInRange);
                    CheckLevelValidation(resourcesInRange);
                    resourceFocused = GetOptimalResource(resourcesInRange);
                    actualState = CharacterState.Mining;
                }
                else
                {
                    print("Nothing there");
                    actualState = CharacterState.Moving;
                }
                break;
        }
    }

    private void CheckState(CharacterState characterState)
    {
        switch(characterState)
        {
            case CharacterState.Mining:
                //whent timer is over, destroy&assign stock
                if (resourceFocused!= null && (startMiningTime + resourceFocused.resourcesInfos.resourcesTimeToMine < Time.time))
                {
                    SetResourceInStock(resourceFocused);
                    State = CharacterState.Mining;
                }
                break;
        }
    }

    private void SetMineText(List<tileInfos> resourceInRange)
    {
        float wood = 0;
        float chicken = 0;
        float corn = 0;
        float rock = 0;

        for (int i = 0; i < resourceInRange.Count; i++)
        {
            findThePos = false;
            if (resourceInRange[i].resourcesInfos.resourceType == GameManager.ResourceType.Wood)
            {
                wood += resourceInRange[i].resourcesInfos.resourcesAmount;
                timeToMineAll += resourceInRange[i].resourcesInfos.resourcesTimeToMine;
            }
            else if (resourceInRange[i].resourcesInfos.resourceType == GameManager.ResourceType.Mouflu)
            {
                chicken += resourceInRange[i].resourcesInfos.resourcesAmount;
                timeToMineAll += resourceInRange[i].resourcesInfos.resourcesTimeToMine;
            }
            else if (resourceInRange[i].resourcesInfos.resourceType == GameManager.ResourceType.Berry)
            {
                corn += resourceInRange[i].resourcesInfos.resourcesAmount;
                timeToMineAll += resourceInRange[i].resourcesInfos.resourcesTimeToMine;
            }
            else if (resourceInRange[i].resourcesInfos.resourceType == GameManager.ResourceType.Rock)
            {
                rock += resourceInRange[i].resourcesInfos.resourcesAmount;
                timeToMineAll += resourceInRange[i].resourcesInfos.resourcesTimeToMine;
            }
        }
        woodText.text = "Wood : " + wood.ToString();
        chickenText.text = "Chicken : " + chicken.ToString();
        cornText.text = "Corn : " + corn.ToString();
        rockText.text = "Rock : " + rock.ToString();
        timingTime.text = "Time to mine : " + timeToMineAll.ToString();
        doItOneTime = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        PlayerInput.InputUp += Move;

        CheckTileUnder();
            
    }

    private void OnDestroy()
    {
        PlayerInput.InputUp -= Move;
    }

    private void Update()
    {
        CheckState(State);

    }

    private List<tileInfos> GetResourcesAround(List<tileInfos> neighbours)
    {
        List<tileInfos> resourcesInRange = new List<tileInfos>();
        for (int i = 0; i < neighbours.Count; i++)
        {
            if (neighbours[i].resourcesInfos != null)
            {
                resourcesInRange.Add(neighbours[i]);
            }
        }
        return resourcesInRange;
    }

    private void CheckLevelValidation(List<tileInfos> resourcesAround)
    {
        //use to check if needs < resourcesAround
        //count tiles needed
        int woodTilesNeeded = 0;
        int berryTilesNeeded = 0;
        for (int i = 0; i < GameManager.Instance.needs.Length; i++)
        {
            if (GameManager.Instance.needs[i].resourceUsed.resourceType == GameManager.ResourceType.Wood)
                woodTilesNeeded += GameManager.Instance.needs[i].TilesNeeded;
            else
            if (GameManager.Instance.needs[i].resourceUsed.resourceType == GameManager.ResourceType.Berry)
                berryTilesNeeded += GameManager.Instance.needs[i].TilesNeeded;
            //protection pour que le joueur mette des resources renouvlables pour le check de son lvl
            else
                woodTilesNeeded += 10;
        }

        //count resources around
        int woodAround = 0;
        int berryAround = 0;
        for (int i = 0; i < resourcesAround.Count; i++)
        {
            if (resourcesAround[i].resourcesInfos.resourceType == GameManager.ResourceType.Wood)
                woodAround++;
            if (resourcesAround[i].resourcesInfos.resourceType == GameManager.ResourceType.Berry)
                berryAround++;
        }

        //compare both
        if (woodAround >= woodTilesNeeded && berryAround >= berryTilesNeeded)
            GameManager.Instance.EndLevel(true);
    }

    private tileInfos GetOptimalResource(List<tileInfos> resourcesInRange)
    {
        Need[] needs = new Need[GameManager.Instance.needs.Length];
        GameManager.Instance.needs.CopyTo(needs,0);
        System.Array.Sort(needs, (x, y) => x.LifeTime.CompareTo(y.LifeTime));
        tileInfos firstOptimalResourceInGame = null;
        for (int n = 0; n < needs.Length; n++)
        {
            Need need = needs[n];
            GameManager.ResourceType resourceTypeToTake = need.resourceUsed.resourcesInfos.resourceType;
            //check if there is a optimal resource in range
            for (int i = 0; i < resourcesInRange.Count; i++)
            {
                if (resourcesInRange[i].resourcesInfos.resourceType == resourceTypeToTake)
                    firstOptimalResourceInGame = resourcesInRange[i];
            }
            //if there is none, take the first resource that can be use for this need
            if (firstOptimalResourceInGame == null)
            {
                for (int i = 0; i < resourcesInRange.Count; i++)
                {
                    for (int j = 0; j < need.resourcesUsable.Length; j++)
                    {
                        if (resourcesInRange[i].resourcesInfos.resourceType == need.resourcesUsable[j])
                            firstOptimalResourceInGame = resourcesInRange[i];
                    }
                }
            }
            if (firstOptimalResourceInGame != null)
                n = 1000;
        }
        return firstOptimalResourceInGame;
    }

    private void Move(GameObject hit)
    {
        if (hit.GetComponent<tileInfos>().tileType == tileInfos.typeOfTile.Blocker)
        {
            return;
        }
        if (State == CharacterState.Moving)
        {
            CheckTileUnder();
            sequence.Kill();
        }
        List<Vector3> positionToGo = new List<Vector3>();
        print(hit);
        positionToGo = pathManager.GeneratePathTo(tileStandingOn, hit.GetComponent<tileInfos>());
        sequence = DOTween.Sequence();
        
        State = CharacterState.Moving;
        stopped = false;
        workZone.SetActive(false);

        Vector3 start = transform.position;
        for (int i = 0; i < positionToGo.Count; i++)
        {
            Vector3 point = positionToGo[i];
            float time = Vector3.Distance(start, point);
            sequence.AppendCallback(() => transform.LookAt(point));
            sequence.Append(transform.DOMove(point, time).SetEase(Ease.Linear));
            start = positionToGo[i];
        }
        

        sequence.timeScale = speedOfMove;
        sequence.onComplete += EndMove;
        sequence.onComplete += CheckTileUnder;

        //transform.DOMove(hit.transform.position, distance).SetEase(Ease.Linear).onComplete += EndMove;
    }

    private void CheckTileUnder()
    {

        RaycastHit hitTile;
        LayerMask layerMask = 1 << 10;
        // new vector 3 est ici pour empecher les bugs. Offset!
        Debug.DrawRay(transform.position - new Vector3 (0, -0.5f, 0), -Vector3.up, Color.red, 10);
        if (Physics.Raycast(transform.position - new Vector3(0, -0.5f, 0), -Vector3.up, out hitTile, 3, layerMask))
        {
            if (tileStandingOn != null)
            {
                tileStandingOn.avatarOnMe = false;
            }
            tileStandingOn = hitTile.transform.GetComponent<tileInfos>();
            tileStandingOn.avatarOnMe = true;
        }
    }

    private void EndMove()
    {
        workZone.SetActive(true);
        stopped = true;
    }

    private void SetResourceInStock(tileInfos resourceFocused)
    {
        GameManager.Instance.GetResourceInStock(resourceFocused.resourcesInfos.resourceType).NumberInStock += resourceFocused.resourcesInfos.resourcesAmount;
        //resourceFocused.gameObject.SetActive(false);
        //StartCoroutine(GameManager.Instance.RespawnOfRessources(resourceFocused.resourcesInfos.resourcesTimeToRespawn, resourceFocused.gameObject));
        resourceFocused.stateResources = tileInfos.stateOfResources.Reloading;
    }

    public void BeginMining()
    {
        if(State != CharacterState.Mining)
            State = CharacterState.Mining;
    }

    void OnTriggerEnter (Collider collider)
    {
        if (collider.transform.tag == "Resources")
        {
            //collider.gameObject.SetActive(false);
            //StartCoroutine(GameManager.Instance.RespawnOfRessources(collider.GetComponent<ResourceInGame>().resourcesInfos.resourcesTimeToRespawn, collider.gameObject));
            collider.GetComponent<tileInfos>().stateResources = tileInfos.stateOfResources.Reloading;
        }
    }
}

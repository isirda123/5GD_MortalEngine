using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
public class DecretManager : MonoBehaviour
{
    int numberOfDecreeAvailable = 0;
    [SerializeField] int timeBetweenTwoDecree;
    [SerializeField] GameObject canvasMinage;
    [SerializeField] GameObject canvasResources;
    [SerializeField] GameObject canvasDecree;
    [SerializeField] GameObject availableDecree;
    [SerializeField] GameObject textNoDecreeAvailable;
    [SerializeField] GameObject numberOfDecreeGO;
    [SerializeField] GameObject validateButton;
    [SerializeField] GameObject decretValidateManager;
    [SerializeField] GameObject decretValidatePrefab;

    [SerializeField] Sprite baseDecreeColor;
    [SerializeField] Sprite selectedDecree;
    [SerializeField] Sprite NotValideDecree;

    [SerializeField] List<DecreeScriptable> allDecree = new List<DecreeScriptable>();
    [SerializeField] List<DecreeScriptable> decreeChoosen = new List<DecreeScriptable>();
    [SerializeField] List<DecreeScriptable> decreeValidate = new List<DecreeScriptable>();



    bool decreeCanvasOpen = false;
    bool decreeChoiceCanvasOpen = false;
    bool decreeAlreadySeen = false;

    int choosenDecree = 0;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(NewDecree());
        allDecree = Resources.LoadAll<DecreeScriptable>("Decree").ToList();
    }

    // Update is called once per frame
    void Update()
    {
        if (numberOfDecreeAvailable > 0)
        {
            numberOfDecreeGO.SetActive(true);
            numberOfDecreeGO.transform.GetChild(0).GetComponent<Text>().text = numberOfDecreeAvailable.ToString();
        }
        else
        {
            numberOfDecreeGO.SetActive(false);
        }
    }


    IEnumerator NewDecree()
    {
        yield return new WaitForSeconds(timeBetweenTwoDecree);
        numberOfDecreeAvailable += 1;
        print("New Decree Available");
        StartCoroutine(NewDecree());
    }




    public void ChangeDecreeInterface()
    {
        HideItAll();
        if (decreeCanvasOpen == false)
        {
            decreeCanvasOpen = true;
            canvasDecree.SetActive(true);
            if (numberOfDecreeAvailable == 0)
                EmptyDecreeCanvas();
            else if (numberOfDecreeAvailable > 0)
            {
                ChoiceDecreeCanvas();
            }
        }
    }

    void ShowAllChoosenDecree()
    {
        decretValidateManager.SetActive(true);
    }

    public void ReturntoGame()
    {
        HideItAll();
        ShowNormalUI();
    }

    public void ChoiceADecree(int WhichDecree)
    {
        UnSelectAllDecree();
        availableDecree.transform.GetChild(WhichDecree - 1).GetComponent<Image>().sprite = selectedDecree;
        choosenDecree = WhichDecree - 1;
        print(choosenDecree);
        validateButton.SetActive(true);
    }

    void UnSelectAllDecree()
    {
        for (int i = 0; i < availableDecree.transform.childCount; i++)
        {
            availableDecree.transform.GetChild(i).GetComponent<Image>().sprite = baseDecreeColor;
        }
        validateButton.SetActive(false);
    }

    void ShowNormalUI()
    {
        canvasMinage.SetActive(true);
        canvasResources.SetActive(true);
        decreeCanvasOpen = false;
        UnSelectAllDecree();
        print("Show Game");
    }
    void HideItAll()
    {
        canvasMinage.SetActive(false);
        canvasResources.SetActive(false);
        canvasDecree.SetActive(false);
        textNoDecreeAvailable.SetActive(false);
        availableDecree.SetActive(false);
        decretValidateManager.SetActive(false);
        print("HideAll");
    }

    void EmptyDecreeCanvas()
    {
        textNoDecreeAvailable.SetActive(true);
    }

    void ChoiceDecreeCanvas()
    {
        if (decreeAlreadySeen == false)
        {
            GetDecreesAvailable();
        }
        else
        {
            GetDecreesAlreadyGet();
        }
    }

    void GetDecreesAvailable()
    {
        decreeAlreadySeen = true;
        availableDecree.SetActive(true);
        GetDecreeFromPool();
    }

    void GetDecreeFromPool()
    {
        for (int i = 0; i < 3; i++)
        {
            print(i);
            int randomDecreeIndex = Random.Range(0, allDecree.Count);
            decreeChoosen.Add(allDecree[randomDecreeIndex]);
            availableDecree.transform.GetChild(i).GetComponent<DecretsUI>().showInfoDecret(allDecree[randomDecreeIndex]);
            allDecree.Remove(decreeChoosen[i]);
        }
    }

    void GetDecreesAlreadyGet()
    {
        availableDecree.SetActive(true);
    }


    public void ValidateDecree()
    {
        numberOfDecreeAvailable -= 1;
        decreeValidate.Add(decreeChoosen[choosenDecree]);
        GameObject dVP = Instantiate(decretValidatePrefab, decretValidateManager.transform.GetChild(0));
        dVP.GetComponent<DecretsValidate>().personalDecree = decreeChoosen[choosenDecree];
        print(decreeChoosen[choosenDecree]);
        decreeChoosen.RemoveAt(choosenDecree);
        for (int i = 0; i < 2; i++)
        {
            allDecree.Add(decreeChoosen[0]);
            decreeChoosen.RemoveAt(0);
        }
        decreeAlreadySeen = false;
        UnSelectAllDecree();
        decreeCanvasOpen = false;

        if (numberOfDecreeAvailable > 0)
        {
            HideItAll();
            ChangeDecreeInterface();
        }
        else
        {
            HideItAll();
            ShowNormalUI();
        }

    }
}

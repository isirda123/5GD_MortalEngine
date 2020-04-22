using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    [SerializeField] Sprite baseDecreeColor;
    [SerializeField] Sprite selectedDecree;
    [SerializeField] Sprite NotValideDecree;



    bool decreeCanvasOpen = false;
    bool decreeAlreadySeen = false;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(NewDecree());
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
        else
        {
            decreeCanvasOpen = false;
            ShowNormalUI();
        }
    }

    public void ChoiceADecree(int WhichDecree)
    {
        UnSelectAllDecree();
        availableDecree.transform.GetChild(WhichDecree-1).GetComponent<Image>().sprite = selectedDecree;
        validateButton.SetActive(true);
    }

    void UnSelectAllDecree()
    {
        for (int i =0; i < availableDecree.transform.childCount; i++)
        {
            availableDecree.transform.GetChild(i).GetComponent<Image>().sprite = baseDecreeColor;
        }
        validateButton.SetActive(false);
    }

    void ShowNormalUI()
    {
        canvasMinage.SetActive(true);
        canvasResources.SetActive(true);
        UnSelectAllDecree();
    }
    void HideItAll()
    {
        canvasMinage.SetActive(false);
        canvasResources.SetActive(false);
        canvasDecree.SetActive(false);
        textNoDecreeAvailable.SetActive(false);
        availableDecree.SetActive(false);
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
        availableDecree.SetActive(true);
    }
    
    void GetDecreesAlreadyGet()
    {

    }


    public void ValidateDecree()
    {
        ShowNormalUI();
    }
}

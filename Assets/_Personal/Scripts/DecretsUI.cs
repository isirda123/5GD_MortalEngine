using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DecretsUI : MonoBehaviour
{
    [SerializeField] public Text nameOfDecree;
    [SerializeField] public Text description;
    [SerializeField] public Text effect;

    DecreeScriptable personalDecree;


    // Start is called before the first frame update
    void Start()
    {
        personalDecree = new DecreeScriptable();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void showInfoDecret (DecreeScriptable dS)
    {
        nameOfDecree.text = dS.decretsInfos.title;
        description.text = dS.decretsInfos.flavorText;
    }
}

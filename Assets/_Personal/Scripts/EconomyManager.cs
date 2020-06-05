using PlayFab.PfEditor.EditorModels;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;

public class EconomyManager : MonoBehaviour
{
    // Start is called before the first frame update
    public void GetPlayerData()
    {
        PlayFabClientAPI.ExecuteCloudScript(new ExecuteCloudScriptRequest()
        {
            FunctionName = "AddExperienceCurrency",
            FunctionParameter = null,
            GeneratePlayStreamEvent = true

        }, cloudResult =>
        {

        }, cloudError =>
        {

        }
        );
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecretsValidate : MonoBehaviour
{
    public DecreeScriptable personalDecree;

    // Start is called before the first frame update
    void Start()
    {
        personalDecree = new DecreeScriptable();
    }

    // Update is called once per frame
    void OnEnable()
    {
        
    }
}

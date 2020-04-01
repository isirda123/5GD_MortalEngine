using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharaAvatar : MonoBehaviour
{
    [Tooltip ("Work")]
    [SerializeField] GameObject workZone;
    [SerializeField] float rangeWorkZone;




    // Start is called before the first frame update
    void Start()
    {
        workZone.transform.localScale = new Vector3( rangeWorkZone, rangeWorkZone, rangeWorkZone);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;

public class PlayerInput : Singleton<PlayerInput>
{
    [Header("Click")]
    float startTime;
    [SerializeField] float lenghtOfAClick;

    [Header("Camera")]
    
    [SerializeField] float speedOfCamera;
    [SerializeField] LayerMask myMask;
    [HideInInspector] public Need needSelected;
    [SerializeField] public CharaAvatar cityPlayer;
    [SerializeField] float speedOFZoom;
    [SerializeField] float minLenghCamera;
    [SerializeField] float maxLenghCamera;
    float valueOfWheel;

    [Header("Cheat Ma Mene")]
    #region CHEAT
    [SerializeField] KeyCode[] cheatInput;
    [SerializeField] GameObject[] stateOfCity;
    #endregion




    private RaycastHit ReturnRaycastHit()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        LayerMask layerMask = 1 << 8;
        layerMask = ~layerMask;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity,layerMask))
        {

        }
        return hit;
    }

    public void ChangeNeedSelectedResourceUse(ResourcesInfos resourceToSet)
    {
        print("change need selected resource use");
        needSelected.ResourceUsed = cityPlayer.GetResourceInStock(resourceToSet.resourceType);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            startTime = Time.time;
        }

        if (Input.GetMouseButton(0))
        {
            if (Time.time - startTime > lenghtOfAClick)
            {
                Camera.main.transform.localPosition -= new Vector3(Input.GetAxisRaw("Mouse X") * Time.deltaTime
                    * speedOfCamera, 0.0f, Input.GetAxisRaw("Mouse Y") * Time.deltaTime * speedOfCamera);
            }
        }

        for (int i = 0; i < cheatInput.Length; i++)
        {
            if (Input.GetKeyDown(cheatInput[i]))
            {
                for (int j = 0; j < stateOfCity.Length; j++)
                {
                    stateOfCity[j].SetActive(false);
                }
                stateOfCity[i].SetActive(true);
            }
        }

        //Zoom
        valueOfWheel = Input.GetAxis("Mouse ScrollWheel");
        ScaleCameraZoom(valueOfWheel);
    }

    void ScaleCameraZoom(float valueOfChange)
    {
        
        Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize -  (valueOfChange * speedOFZoom),minLenghCamera,maxLenghCamera);
    }
}

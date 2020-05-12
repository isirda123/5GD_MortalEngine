﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;

public class PlayerInput : MonoBehaviour
{
    [Header("Click")]
    float startTime;
    [SerializeField] float lenghtOfAClick;

    [Header("Camera")]
    
    [SerializeField] float speedOfCamera;

    [SerializeField] LayerMask myMask;

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

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            startTime = Time.time;
        }

        if (Input.GetMouseButton(0))
        {
            if (Time.time - startTime > lenghtOfAClick )
            {
                Camera.main.transform.localPosition -= new Vector3(Input.GetAxisRaw("Mouse X") * Time.deltaTime 
                    * speedOfCamera, 0.0f, Input.GetAxisRaw("Mouse Y") * Time.deltaTime * speedOfCamera);
            }
        }
    }
}

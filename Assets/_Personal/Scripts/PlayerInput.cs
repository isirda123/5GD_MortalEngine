using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerInput : MonoBehaviour
{
    [Header("Click")]
    float startTime;
    [SerializeField] float lenghtOfAClick;

    [Header("Camera")]
    
    [SerializeField] float speedOfCamera;

    [Header("Avatar")]
    [SerializeField] GameObject fortressNavMesh;

    [SerializeField] LayerMask myMask;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (fortressNavMesh.GetComponent<NavMeshAgent>().hasPath == true)
        {
            fortressNavMesh.GetComponent<CharaAvatar>().workZone.SetActive(false);
            fortressNavMesh.GetComponent<CharaAvatar>().stopped = false;
        }
        else
        {
            fortressNavMesh.GetComponent<CharaAvatar>().workZone.SetActive(true);
            fortressNavMesh.GetComponent<CharaAvatar>().stopped = true;

        }

        if (Input.GetMouseButtonDown(0))
        {
            startTime = Time.time;
        }
        if (Input.GetMouseButton(0))
        {
            if (Time.time - startTime > lenghtOfAClick )
            {
                if (Input.GetAxis("Mouse X") > 0)
                {
                    Camera.main.transform.position -= new Vector3(Input.GetAxisRaw("Mouse X") * Time.deltaTime * speedOfCamera,
                                               0.0f, Input.GetAxisRaw("Mouse Y") * Time.deltaTime * speedOfCamera);
                }

                else if (Input.GetAxis("Mouse X") < 0)
                {
                    Camera.main.transform.transform.position -= new Vector3(Input.GetAxisRaw("Mouse X") * Time.deltaTime * speedOfCamera,
                                               0.0f, Input.GetAxisRaw("Mouse Y") * Time.deltaTime * speedOfCamera);
                }
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (Time.time - startTime <= lenghtOfAClick && fortressNavMesh.GetComponent<CharaAvatar>().mining == false)
            {


                //Make the Fortress move;


                var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, 10000, myMask))
                {
                    if (hit.transform.gameObject.layer == 9)
                    {
                        var clickPosition = hit.point;
                        fortressNavMesh.GetComponent<NavMeshAgent>().SetDestination(clickPosition);
                    }
                    else if (hit.transform.gameObject.layer == 5)
                    {

                    }
                }

                
            }
        }
    }
}

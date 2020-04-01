using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerInput : MonoBehaviour
{
    [Tooltip("Click")]
    float startTime;
    [SerializeField] float lenghtOfAClick;

    [Tooltip("Camera")]
    
    [SerializeField] float speedOfCamera;

    [Tooltip("Avatar")]
    [SerializeField] GameObject fortressNavMesh;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
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
            if (Time.time - startTime <= lenghtOfAClick)
            {


                //Make the Fortress move;
                print(Time.time - startTime);

                var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, 1000))
                {
                    
                    var clickPosition = hit.point;
                    print(clickPosition);
                    fortressNavMesh.GetComponent<NavMeshAgent>().SetDestination(clickPosition);
                }

                
            }
        }
    }
}

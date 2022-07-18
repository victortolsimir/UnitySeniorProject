using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMotor))]
public class PlayeController : MonoBehaviour
{
    public LayerMask movementMask;

    Camera cam;
    PlayerMotor motor;

    void Start()
    {
        //Create a camera to work with a ray object
        cam = Camera.main;
        motor = GetComponent<PlayerMotor>();
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //To determine where a mouse click was made in an evironment you need a cam and ray object
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if(Physics.Raycast(ray,out hit,100,movementMask))
            {

                //Debug.Log("We hit " + hit.collider.name + " " + hit.point);
                //Move player to what we clicked on/hit
                motor.MoveToPoint(hit.point);

                //Stop focusing any objects
            }
 
            
        }

        //handle right mouse click for interacting with objects
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100, movementMask))
            {
                //Check if we hit an iteractable
                //if we did, set it as our focus
                
            }

        }
    }
}

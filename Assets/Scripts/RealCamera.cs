﻿using UnityEngine;
using System.Collections;

public class RealCamera : MonoBehaviour
{

    [Header("Camera Properties")]
    private float DistanceAway;                     //how far the camera is from the player.

    public float minDistance = 1;                //min camera distance
    public float maxDistance = 2;                //max camera distance

    public float DistanceUp = -2;                    //how high the camera is above the player
    public float smooth = 4.0f;                    //how smooth the camera moves into place
    public float rotateAround = 70f;            //the angle at which you will rotate the camera (on an axis)

    [Header("Player to follow")]
    public Transform target;                    //the target the camera follows

    [Header("Layer(s) to include")]
    public LayerMask CamOcclusion;                //the layers that will be affected by collision

    [Header("Map coordinate script")]
    //    public worldVectorMap wvm;
    RaycastHit hit;
    float cameraHeight = 55f;
    float cameraPan = 0f;
    float camRotateSpeed = 180f;
    Vector3 camPosition;
    Vector3 camMask;
    Vector3 followMask;

    private float HorizontalAxis;
    private float VerticalAxis;
    private float smoothDef;

    // Use this for initialization
    void Start()
    {
        //the statement below automatically positions the camera behind the target.
        rotateAround = target.eulerAngles.y - 45f;

        smoothDef = smooth;

        smooth = 100;
        transform.position = target.position;
        smooth = smoothDef;
    }

    void LateUpdate()
    {
        DistanceAway = maxDistance;

        HorizontalAxis = Input.GetAxis("Horizontal");
        VerticalAxis = Input.GetAxis("Vertical");

        //Offset of the targets transform (Since the pivot point is usually at the feet).
        Vector3 targetOffset = new Vector3(target.position.x, (target.position.y + 1.3f), target.position.z);
        //Quaternion rotation = Quaternion.Euler(cameraHeight, rotateAround, cameraPan);
        Quaternion rotation = Quaternion.Euler(cameraHeight, rotateAround, cameraPan);
        Vector3 vectorMask = Vector3.one;
        Vector3 rotateVector = rotation * vectorMask;
        //this determines where both the camera and it's mask will be.
        //the camMask is for forcing the camera to push away from walls.
        camPosition = targetOffset + Vector3.up * DistanceUp - rotateVector * DistanceAway;
        camMask = targetOffset + Vector3.up * DistanceUp - rotateVector * DistanceAway;

        camPosition.x = target.position.x;
        /*
        if (camPosition.z > target.position.z)
        {
            camPosition.z = target.position.z - 0.1f;
        }
        */
        OccludeRay(ref targetOffset);
        SmoothCamMethod();
        //transform.position = camPosition;

        if (camPosition.z > target.position.z - 0.05f)
        {
            camPosition.z = target.position.z - 0.1f;
        }

        transform.LookAt(target);
        /*
        #region wrap the cam orbit rotation
        if (rotateAround > 360)
        {
            rotateAround = 0f;
        }
        else if (rotateAround < 0f)
        {
            rotateAround = (rotateAround + 360f);
        }
        #endregion

        rotateAround += HorizontalAxis * camRotateSpeed * Time.deltaTime;
        */
        //DistanceUp = Mathf.Clamp(DistanceUp += VerticalAxis, -0.79f, 2.3f);
        DistanceAway = Mathf.Clamp(DistanceAway += VerticalAxis, minDistance, maxDistance);

    }
    void SmoothCamMethod()
    {
        smooth = smoothDef;

        transform.position = Vector3.Lerp(transform.position, camPosition, Time.deltaTime * smooth);
    }
    void OccludeRay(ref Vector3 targetFollow)
    {
        #region prevent wall clipping
        //declare a new raycast hit.
        RaycastHit wallHit = new RaycastHit();
        //linecast from your player (targetFollow) to your cameras mask (camMask) to find collisions.
        if (Physics.Linecast(targetFollow, camMask, out wallHit, CamOcclusion))
        {
            //the smooth is increased so you detect geometry collisions faster.
            smooth = 10f;
            //the x and z coordinates are pushed away from the wall by hit.normal.
            //the y coordinate stays the same.
            camPosition = new Vector3(camPosition.x, wallHit.point.y + wallHit.normal.y * 0.5f, wallHit.point.z + wallHit.normal.z * 0.5f);
            //camPosition = new Vector3(wallHit.point.x + wallHit.normal.x * 0.5f, camPosition.y, wallHit.point.z + wallHit.normal.z * 0.5f);
        }
        #endregion
    }
}
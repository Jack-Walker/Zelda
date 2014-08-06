﻿using UnityEngine;
using System.Collections;

// Script by Jack Walker
public class Player : MonoBehaviour 
{
    public enum LinkStates
    {
        Idle,
        Running,
        Rolling,
        Falling
    };
    public UnityEngine.Camera mainCamera;
    private CameraController cameraController;
    public LinkStates state;
    public Vector3 lastPosition;
    public int health, maxHealth;
	// Use this for initialization
	void Start() 
    {
        mainCamera = GameObject.Find("PositionCam/DefaultCam").camera;
        cameraController = GameObject.Find("SC_Camera").GetComponent<CameraController>();
        lastPosition = transform.position;
        maxHealth = 12;
        health = maxHealth;
	}
	
	// Update is called once per frame
	void FixedUpdate() 
    {
        FallCheck();
	    switch (state)
        {
            case LinkStates.Idle:
                UpdateIdle();
                break;
            case LinkStates.Running:
                UpdateRunning();
                break;
            case LinkStates.Rolling:

                break;
        }
	}

    void OnCollisionEnter(Collision other)
    {
        
    }

    void FallCheck()
    {
        if (Mathf.Abs(lastPosition.y - transform.position.y) >= 0.01f && lastPosition.y > transform.position.y)
        {
            state = LinkStates.Falling;
        }
        else
        {
            if (state == LinkStates.Falling)
                state = LinkStates.Idle;
        }
        lastPosition = transform.position;
    }

    void ObstacleTest()
    {
        RaycastHit hitTopMost;
        RaycastHit hitTop;
        RaycastHit hitMid;
        Vector3 fwd = transform.TransformDirection(Vector3.forward);
        // Top-most vector
        bool topmostRaycast = Physics.Raycast(transform.position + new Vector3(0, 2f, 0), fwd, out hitTopMost, 100);
        bool topRaycast = Physics.Raycast(transform.position + new Vector3(0, 1, 0), fwd, out hitTop, 100);
        bool midRaycast = Physics.Raycast(transform.position + new Vector3(0, 0, 0), fwd, out hitMid, 100);
        Debug.DrawRay(transform.position + new Vector3(0, 2f, 0), fwd * 50, Color.blue);
        Debug.DrawRay(transform.position + new Vector3(0, 1, 0), fwd * 50, Color.blue);
        Debug.DrawRay(transform.position + new Vector3(0, 0, 0), fwd * 50, Color.blue);
        // Top ray
        if ((hitTopMost.collider == null || hitTopMost.distance >= 0.7f) && (hitTop.collider != null && hitTop.distance <= 0.65f) && hitMid.distance <= 0.65f)
        {
            if (hitTop.collider.gameObject.name.Contains("ob"))
            {
                Debug.Log("Toppoint obstacle");
                 transform.position += (transform.forward * 1.0f) + (transform.up * 2.5f);
            }
        }
        // Mid ray
        if ((hitTopMost.collider == null || hitTopMost.distance >= 0.7f) && (hitTop.collider == null || hitTop.distance >= 0.7f) && (hitMid.collider != null && hitMid.distance <= 0.6f))
        {
            if (hitMid.collider.gameObject.name.Contains("ob"))
            {
                Debug.Log("Midpoint obstacle");
                transform.position += (transform.forward * 1.0f) + (transform.up * 0.80f);
            }
        }
        //Debug.Log("Topmost " + hitTopMost.distance);
        //Debug.Log("Top " + hitTop.distance);
        //Debug.Log("Mid " + hitMid.distance);
        //Debug.Log("Topmost " + topmostRaycast);
        //Debug.Log("Top " + topRaycast);
        //Debug.Log("Mid " + midRaycast);
    }

    void UpdateIdle()
    {
        if (Input.GetKey(KeyCode.W) || (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)))
            state = LinkStates.Running;
        ObstacleTest();
    }
    void UpdateRunning()
    {
         if (!Input.GetKey(KeyCode.W) && (!Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D)))
             state = LinkStates.Idle;
         ObstacleTest();
         Vector3 cameraEulerAngles = mainCamera.transform.eulerAngles;
         cameraEulerAngles.x = 0;
         cameraEulerAngles.z = 0;
        if (Input.GetKey(KeyCode.W))
        {
            if (cameraController.isZTargeting)
                transform.position += (mainCamera.transform.forward * 4.0f) * Time.deltaTime;
            else
                transform.eulerAngles = cameraEulerAngles;
        }
        if (Input.GetKey(KeyCode.S))
        {
            if (cameraController.isZTargeting)
                transform.position += (mainCamera.transform.forward * -4.0f) * Time.deltaTime;
            else
                transform.eulerAngles = cameraEulerAngles + new Vector3(0, 180, 0);
        }
        if (Input.GetKey(KeyCode.A))
        {
            if (cameraController.isZTargeting)
                transform.position += (mainCamera.transform.right * -4.0f) * Time.deltaTime;
            else
                transform.eulerAngles = cameraEulerAngles - new Vector3(0, 90, 0);
        }
        if (Input.GetKey(KeyCode.D))
        {
            if (cameraController.isZTargeting)
                transform.position += (mainCamera.transform.right * 4.0f) * Time.deltaTime;
            else
                transform.eulerAngles = cameraEulerAngles + new Vector3(0, 90, 0);
        }
        if (!cameraController.isZTargeting)
            transform.position += (transform.forward * 4.0f) * Time.deltaTime;
    }
}

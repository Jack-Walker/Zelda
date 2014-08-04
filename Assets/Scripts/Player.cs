using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour 
{
    public enum LinkStates
    {
        Idle,
        Running,
        Rolling
    };
    public UnityEngine.Camera mainCamera;
    public LinkStates state;
	// Use this for initialization
	void Start () 
    {
        mainCamera = GameObject.Find("PositionCam/DefaultCam").camera;
	}
	
	// Update is called once per frame
	void FixedUpdate () 
    {
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

    void UpdateIdle()
    {
        if (Input.GetKey(KeyCode.W) || (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)))
            state = LinkStates.Running;
    }
    void UpdateRunning()
    {
         if (!Input.GetKey(KeyCode.W) && (!Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D)))
             state = LinkStates.Idle;
         Vector3 cameraEulerAngles = mainCamera.transform.eulerAngles;
         cameraEulerAngles.x = 0;
         cameraEulerAngles.z = 0;
        if (Input.GetKey(KeyCode.W))
        {
            transform.eulerAngles = cameraEulerAngles;
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.eulerAngles = cameraEulerAngles + new Vector3(0, 180, 0);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.eulerAngles = cameraEulerAngles - new Vector3(0, 90, 0);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.eulerAngles = cameraEulerAngles + new Vector3(0, 90, 0);
        }
        transform.position += (transform.forward * 4.0f) * Time.deltaTime;
    }
}

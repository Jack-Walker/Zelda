using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour 
{
    public UnityEngine.Camera mainCamera;
	// Use this for initialization
	void Start () 
    {
        mainCamera = GameObject.Find("PositionCam/DefaultCam").camera;
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}
}

using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour 
{
    private Player player;

	// Use this for initialization
	void Start () 
    {
        player = GameObject.Find("Player").GetComponent<Player>();
	}
	
	// Update is called once per frame
	void Update () 
    {
        transform.position = player.transform.position + (transform.forward * -4.0f) + (transform.up * 2.0f) + (transform.right * 2.0f);
        
	}
}

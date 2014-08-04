using UnityEngine;
using System.Collections;

public class ItemMenu : MonoBehaviour 
{
    private HUD hud;
    private Player player;
	// Use this for initialization
	void Start () 
    {
        hud = GameObject.Find("RotationCam/GUICam/HUD").GetComponent<HUD>();
        player = GameObject.Find("/Player").GetComponent<Player>();
	}
	
	// Update is called once per frame
	void Update () 
    {
	    if (Input.GetKeyDown(KeyCode.Return))
        {
            camera.enabled = !camera.enabled;
        }
        hud.enabled = camera.enabled;
        player.enabled = !camera.enabled;
	}
}

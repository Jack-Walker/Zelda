using UnityEngine;
using System.Collections;

// Script by Jack Walker
public class ItemMenu : MonoBehaviour 
{
    private HUD hud;
    private Player player;
    private AudioSource[] itemMenuSounds;
	// Use this for initialization
	void Start () 
    {
        hud = GameObject.Find("RotationCam/GUICam/HUD").GetComponent<HUD>();
        player = GameObject.Find("/Player").GetComponent<Player>();
        // Initialize sound effects
        itemMenuSounds = new AudioSource[2];
        // Item Menu Open SFX
        itemMenuSounds[0] = gameObject.AddComponent<AudioSource>();
        itemMenuSounds[0].clip = Resources.Load<AudioClip>("Audio/SFX/Menus/OOT_PauseMenu_Open");
        // Item Menu Close SFX
        itemMenuSounds[1] = gameObject.AddComponent<AudioSource>();
        itemMenuSounds[1].clip = Resources.Load<AudioClip>("Audio/SFX/Menus/OOT_PauseMenu_Close");
	}
	
	// Update is called once per frame
	void Update () 
    {
	    if (Input.GetKeyDown(KeyCode.Return))
        {
            camera.enabled = !camera.enabled;
            if (camera.enabled)
            {
                itemMenuSounds[1].Stop();
                itemMenuSounds[0].Play();
            }
            else
            {
                itemMenuSounds[0].Stop();
                itemMenuSounds[1].Play();
            }
        }
        hud.enabled = camera.enabled;
        player.enabled = !camera.enabled;
	}
}

using UnityEngine;
using System.Collections;

public class GreenRupee : MonoBehaviour 
{
    // Todo: Make rupee sound one audio source for all rupee objects.
    AudioSource rupeeSound;
    private bool shouldDestroy = false;
    // Use this for initialization
	void Start () 
    {
        rupeeSound = gameObject.AddComponent<AudioSource>();
        rupeeSound.clip = GameEngine.GetSound("OoT:Items/OOT_Get_Rupee");
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (shouldDestroy)
        {
            if (!rupeeSound.isPlaying)
                GameObject.DestroyObject(gameObject);
        }
        
	}

    void OnTriggerEnter(Collider other)
    {
        CH_Player player = GameObject.Find("CH_Player").GetComponent<CH_Player>();
        player.rupeeCount++;
        rupeeSound.Play();
        renderer.enabled = false;
        shouldDestroy = true;
    }
}

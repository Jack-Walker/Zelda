using UnityEngine;
using System.Collections;

public class ZoraTest : MonoBehaviour 
{

	// Use this for initialization
	void Start () 
    {
	    
	}
	
	// Update is called once per frame
	void Update () 
    {
	    
	}

    void OnCollisionEnter(Collision other)
    {
        Debug.Log("Some collision...");
        if (other.gameObject.tag == "Player")
        {
            CH_Player player = other.gameObject.GetComponent<CH_Player>();
            if (player.invincibilityTime > 0.0f)
                return;
            player.invincibilityTime = 2.0f;
            player.health -= 2;
            player.linkSounds[(int)CH_Player.LinkSoundsEnum.Hurt0].Play();
        }
    }
}

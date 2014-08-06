using UnityEngine;
using System.Collections;

public class HUD_Drawer : MonoBehaviour 
{
    private Player player;
	// Use this for initialization
	void Start() 
    {
        player = GameObject.Find("Player").GetComponent<Player>();

        // Test: Delete pre-defined heart containers.
        GameObject heartContainerObject = GameObject.Find("Health");
        for (int i = 0; i < heartContainerObject.transform.childCount; i++)
        {
            GameObject.DestroyObject(heartContainerObject.transform.GetChild(i).gameObject);
        }
        heartContainerObject.transform.DetachChildren();
        player = GameObject.Find("Player").GetComponent<Player>();
	}
	
	// Update is called once per frame
	void Update() 
    {
         DrawHearts();
	}

   void DrawHearts()
    {
        GameObject heartContainerObject = GameObject.Find("Health");
        int desiredHeartCount = player.maxHealth / 4;
        if (heartContainerObject.transform.childCount != desiredHeartCount)
        {
            // Clear old children
            for (int i = 0; i < heartContainerObject.transform.childCount; i++)
            {
                GameObject.DestroyObject(heartContainerObject.transform.GetChild(i).gameObject);
            }
            heartContainerObject.transform.DetachChildren();
            // Add the new heart containers.
            for (int i = 0; i < desiredHeartCount; i++)
            {
                GameObject heartContainer = new GameObject("Heart " + i);
                heartContainer.transform.parent = heartContainerObject.transform;
                heartContainer.transform.localPosition = new Vector3(-4.057156f + (i * 0.312654f), 3.902238f, -1.394146e-07f);
                heartContainer.transform.localScale = new Vector3(2.196093f, 2.736454f, 0.9999999f);
                heartContainer.transform.rotation = new Quaternion(0, 0, 0, 0);
                SpriteRenderer spriteRenderer = heartContainer.AddComponent<SpriteRenderer>();
                spriteRenderer.sprite = Resources.Load<Sprite>("Data/Gameplay/Textures/Hearts/4-4 Heart1");
                heartContainer.renderer.material.shader = Resources.Load<Shader>("Data/Gameplay/Shaders/Sprite");
                heartContainer.renderer.material.SetColor("_Color", new Color32(253, 98, 98, 255));
                heartContainer.layer = 5;
            }
        }
    }
}

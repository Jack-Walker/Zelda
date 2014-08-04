using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour 
{
    private Player player;
    private float cameraTurnDelay;

	// Use this for initialization
	void Start () 
    {
        player = GameObject.Find("Player").GetComponent<Player>();
	}
	
	// Update is called once per frame
	void Update () 
    {
        transform.position = player.transform.position + (transform.forward * -5.5f) + (transform.up * 2.0f);
        Vector3 eulerAngles = transform.eulerAngles;
        eulerAngles.x = 20 + (Mathf.Abs(player.transform.position.x - transform.position.x));
        transform.eulerAngles = eulerAngles;
        CameraTurnCheck();
	}

    void CameraTurnCheck()
    {
        Vector3 cameraEulerAngles = transform.eulerAngles;
        if (Time.time > cameraTurnDelay)
        {
            if (player.state != Player.LinkStates.Idle)
            {
                cameraTurnDelay = Time.time + 0.8f;
                return;
            }
            if (transform.eulerAngles.y > player.transform.eulerAngles.y)
            {
                transform.eulerAngles -= new Vector3(0, 1, 0);
            }
            if (transform.eulerAngles.y < player.transform.eulerAngles.y)
            {
                transform.eulerAngles += new Vector3(0, 1, 0);
            }
        }
    }
}

using UnityEngine;
using System.Collections;

public class CH_Player : MonoBehaviour
{
	public enum LinkStates
	{
		Idle,
		Running,
		Rolling,
		Falling
	};

	public enum LinkSoundsEnum
	{
		Step0,
		Step1,
		Step2,
        Hurt0,
        Hurt1,
        Hurt2
	};
    public enum TunicTypes
    {
        KokiriTunic,
        GoronTunic,
        ZoraTunic
    };

	public UnityEngine.Camera mainCamera;
	private SC_Camera cameraController;
	public LinkStates state;
	public Vector3 lastPosition;
	public AudioSource[] linkSounds = new AudioSource[8];
	private int stepCounter = 0;
	public int health, maxHealth;
    public int rupeeCount;
    public float invincibilityTime = 0.0f;
    public TunicTypes currentTunic = TunicTypes.KokiriTunic;


	void Start()
	{
		if (GameObject.FindGameObjectsWithTag("Player").Length > 1)
		{
			GameObject.Destroy(gameObject);
			return;
		}
		mainCamera = GameObject.Find("Position/DefaultCam").camera;
		cameraController = GameObject.FindGameObjectWithTag("SC_Camera").GetComponent<SC_Camera>();
		lastPosition = transform.position;
		maxHealth = 76;
		health = maxHealth - 3;
        rupeeCount = 0;

		// Load sound effects
		linkSounds[(int)LinkSoundsEnum.Step0] = gameObject.AddComponent<AudioSource>();
		linkSounds[(int)LinkSoundsEnum.Step0].clip = GameEngine.GetSound("OoT:Footsteps/Dirt1");
		linkSounds[(int)LinkSoundsEnum.Step1] = gameObject.AddComponent<AudioSource>();
		linkSounds[(int)LinkSoundsEnum.Step1].clip = GameEngine.GetSound("OoT:Footsteps/Dirt2");
		linkSounds[(int)LinkSoundsEnum.Step2] = gameObject.AddComponent<AudioSource>();
		linkSounds[(int)LinkSoundsEnum.Step2].clip = GameEngine.GetSound("OoT:Footsteps/Dirt3");
        linkSounds[(int)LinkSoundsEnum.Hurt0] = gameObject.AddComponent<AudioSource>();
        linkSounds[(int)LinkSoundsEnum.Hurt0].clip = GameEngine.GetSound("OoT:AdultLink/Hurt1");
	}
	
	void FixedUpdate() 
	{
		FallCheck();
        TunicCheck();
        InvincibilityCheck();
        switch (state)
		{
			case LinkStates.Idle:
                if (!cameraController.isTargeting)
                {
                    transform.GetChild(0).animation["Idle"].speed = 1.0f;
                    transform.GetChild(0).animation.Play("Idle");
                }
                else if (cameraController.isTargeting)
                {
                    transform.GetChild(0).animation["Targetting"].speed = 0.5f;
                    transform.GetChild(0).animation.Play("Targetting");
                }
				UpdateIdle();
				break;
			case LinkStates.Running:
			{
                if (cameraController.isTargeting)
                {
                    float h = 0;
                    if (cameraController.oldEnemy == null)
                        h = Input.GetAxis("H-Axis");
                    else
                        h = Input.GetAxis("V-Axis");
                    if (h < -.5f)
                    {
                        transform.GetChild(0).animation["SidestepL"].speed = 1.5f;
                        transform.GetChild(0).animation.Play("SidestepL");
                    }
                    else if (h > .5f)
                    {
                        transform.GetChild(0).animation["SidestepR"].speed = 1.5f;
                        transform.GetChild(0).animation.Play("SidestepR");
                    }
                    else if (cameraController.oldEnemy != null)
                    {
                        transform.GetChild(0).animation["Targetting"].speed = 0.5f;
                        transform.GetChild(0).animation.Play("Targetting");
                    }
                    else
                    {
                        transform.GetChild(0).animation["Run"].speed = 1.5f;
                        transform.GetChild(0).animation.Play("Run");
                    }
                }
                else
                {
                    transform.GetChild(0).animation["Run"].speed = 1.5f;
                    transform.GetChild(0).animation.Play("Run");
                    //Debug.Log(transform.GetChild(0).animation["Run"].normalizedTime % 1.0f);
                    // Test
                    if (transform.GetChild(0).animation["Run"].normalizedTime == (0.0f % 1.0f))
                    {
                        //StepSFX();
                    }
                }
				UpdateRunning();
				break;
			}
			case LinkStates.Rolling:
				
				break;
		}
	}
	
	void OnCollisionEnter(Collision other)
	{   
	}

    void InvincibilityCheck()
    {   
        if (invincibilityTime > 0.0f)
        {
            invincibilityTime -= Time.deltaTime;
        }
    }

    void TunicCheck()
    {
        GameObject tunicPart1 = GameObject.Find("CH_Player/CH_AdultLink/ob_06034FA0");
        GameObject tunicPart2 = GameObject.Find("CH_Player/CH_AdultLink/ob_00035860");
        GameObject tunicPart3 = GameObject.Find("CH_Player/CH_AdultLink/ob_06032970");
        GameObject tunicPart4 = GameObject.Find("CH_Player/CH_AdultLink/ob_06035250");
        switch (currentTunic)
        {
            case TunicTypes.KokiriTunic:
                foreach (Material material in tunicPart1.renderer.materials)
                {
                    material.color = new Color32(30, 105, 27, 255);
                }
                foreach (Material material in tunicPart2.renderer.materials)
                {
                    material.color = new Color32(30, 105, 27, 255);
                }
                foreach (Material material in tunicPart3.renderer.materials)
                {
                    material.color = new Color32(30, 105, 27, 255);
                }
                foreach (Material material in tunicPart4.renderer.materials)
                {
                    material.color = new Color32(30, 105, 27, 255);
                }
                
                break;
            case TunicTypes.GoronTunic:
                foreach (Material material in tunicPart1.renderer.materials)
                {
                    material.color = new Color32(105, 30, 27, 255);
                }
                foreach (Material material in tunicPart2.renderer.materials)
                {
                    material.color = new Color32(105, 30, 27, 255);
                }
                foreach (Material material in tunicPart3.renderer.materials)
                {
                    material.color = new Color32(105, 30, 27, 255);
                }
                foreach (Material material in tunicPart4.renderer.materials)
                {
                    material.color = new Color32(105, 30, 27, 255);
                }
                break;
            case TunicTypes.ZoraTunic:
                foreach (Material material in tunicPart1.renderer.materials)
                {
                    material.color = new Color32(27, 30, 105, 255);
                }
                foreach (Material material in tunicPart2.renderer.materials)
                {
                    material.color = new Color32(27, 30, 105, 255);
                }
                foreach (Material material in tunicPart3.renderer.materials)
                {
                    material.color = new Color32(27, 30, 105, 255);
                }
                foreach (Material material in tunicPart4.renderer.materials)
                {
                    material.color = new Color32(27, 30, 105, 255);
                }
                break;
        }
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
				Debug.Log("toppt");
				Debug.Log("Topmost " + hitTopMost.distance);
				Debug.Log("Top " + hitTop.distance);
				Debug.Log("Mid " + hitMid.distance);
				transform.position += (transform.forward * 1.0f) + (transform.up * 2.5f);
			}
		}
		// Mid ray
		if ((hitTopMost.collider == null || hitTopMost.distance >= 0.7f) && (hitTop.collider == null || hitTop.distance >= 0.7f) && (hitMid.collider != null && hitMid.distance <= 0.6f))
		{
			if (hitMid.collider.gameObject.name.Contains("ob"))
			{
				Debug.Log("midpt");
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
	
	void StepSFX()
	{
		stepCounter++;
		if (linkSounds[stepCounter % 2].isPlaying == false)
			linkSounds[stepCounter % 2].Play();
	}
	
	void UpdateIdle()
	{
		if (Input.GetKey(KeyCode.UpArrow) || (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow)))
			state = LinkStates.Running;
		ObstacleTest();
	}
	void UpdateRunning()
	{
		float h = Input.GetAxis("H-Axis");
		float v = Input.GetAxis("V-Axis");
		
		if (h == 0.0f && v == 0.0f)
			state = LinkStates.Idle;
		StepSFX();
		ObstacleTest();
		Vector3 cameraEulerAngles = mainCamera.transform.eulerAngles;
		cameraEulerAngles.x = 0;
		cameraEulerAngles.z = 0;
		if (v != 0.0f)
		{
			if (cameraController.isTargeting)
				transform.position += (mainCamera.transform.forward * v * 5.5f) * Time.deltaTime;
			else
			{
				if (v < 0.0f)
					transform.eulerAngles = cameraEulerAngles + new Vector3(0, 180, 0);
				else
					transform.eulerAngles = cameraEulerAngles;
			}
		}
		if (h != 0.0f)
		{
			if (cameraController.isTargeting)
				transform.position += (mainCamera.transform.right * h * 5.5f) * Time.deltaTime;
			else
			{
				if (h < 0.0f)
					transform.eulerAngles = cameraEulerAngles - new Vector3(0, 90, 0);
				else
					transform.eulerAngles = cameraEulerAngles + new Vector3(0, 90, 0);
			}
		}
		if (!cameraController.isTargeting)
			transform.position += (transform.forward * 5.5f) * Time.deltaTime;
	}
}

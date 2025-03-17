using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;

public class CameraManager : MonoBehaviour 
{

	public float followSpeed = 3; //Speed ​​at which the camera follows us
	public float mouseSpeed = 2; //Speed ​​at which we rotate the camera with the mouse
	//public float controllerSpeed = 5; //Speed ​​at which we rotate the camera with the joystick
	public float cameraDist = 3; //Distance to which the camera is located

	public Transform target; //Player the camera follows

	[HideInInspector]
	public Transform pivot; //Pivot on which the camera rotates(distance that we want between the camera and our character)
	[HideInInspector]
	public Transform camTrans; //Camera position

	float turnSmoothing = .1f; //Smooths all camera movements (Time it takes the camera to reach the rotation indicated with the joystick)
	public float minAngle = -35; //Minimum angle that we allow the camera to reach
	public float maxAngle = 35; //Maximum angle that we allow the camera to reach

	float smoothX;
	float smoothY;
	float smoothXvelocity;
	float smoothYvelocity;
	public float lookAngle; //Angle the camera has on the Y axis
	public float tiltAngle; //Angle the camera has up / down
	[SerializeField] private bool isFinish = false;
	[SerializeField] private bool FreeCam = false;
	[SerializeField] private bool ranking = false;
	[SerializeField] private List<GameObject> otherTarget;
	[SerializeField] private int Index;

	NakamaConnect gameManager;
	public IDictionary<int, GameObject> playersList;
	public TMP_Text CameraMode;

	public void Init()
	{
		camTrans = transform.GetChild(0).transform.GetChild(0);//Camera.main.transform;
		pivot = camTrans.parent;
		//target = GameObject.FindGameObjectWithTag("Player").transform;
	}

	void Start()
	{
		gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<NakamaConnect>();
	}
	public void OnEnable()
	{
		CharacterControls.FinishRun += FinishRun;
		//NakamaConnect.EndGame += Ranking;
	}
	public void OnDisable()
	{
		CharacterControls.FinishRun -= FinishRun;
		//NakamaConnect.EndGame -= Ranking;
	}

	public void FollowTarget(float d)
	{ 
		//Function that makes the camera follow the player
		float speed = d * followSpeed; //Set speed regardless of fps
		Vector3 targetPosition = Vector3.Lerp(new Vector3(0, transform.localPosition.y, 0), new Vector3(0, target.transform.localPosition.y, 0), speed); //Bring the camera closer to the player interpolating with the velocity(0.5 half, 1 everything)
		transform.localPosition = targetPosition; //Update the camera position
	}

	public void FollowTarget(Vector3 Pos)
	{ 
		//Function that makes the camera follow the player
		//float speed = d * followSpeed; //Set speed regardless of fps
		//Vector3 targetPosition = Vector3.Lerp(transform.position, target.position, speed); //Bring the camera closer to the player interpolating with the velocity(0.5 half, 1 everything)
		transform.position = Pos; //Update the camera position
	}

	void HandleRotations(float d, float v, float h, float targetSpeed)
	{ 
		//Function that rotates the camera correctly
		if (turnSmoothing > 0)
		{
			smoothX = Mathf.SmoothDamp(smoothX, h, ref smoothXvelocity, turnSmoothing); //Gradually change a value toward a desired goal over time.
			smoothY = Mathf.SmoothDamp(smoothY, v, ref smoothYvelocity, turnSmoothing);
		}
		else
		{
			smoothX = h;
			smoothY = v;
		}

		tiltAngle -= smoothY * targetSpeed; //Update the angle at which the camera will move
		tiltAngle = Mathf.Clamp(tiltAngle, minAngle, maxAngle); //Limits with respect to the maximum and minimum
		pivot.localRotation = Quaternion.Euler(tiltAngle, 0, 0); //Modify the up / down angle

		lookAngle += smoothX * targetSpeed; //Updates the rotation angle in y smoothly
		transform.rotation = Quaternion.Euler(0, lookAngle, 0); //Apply the angle

	}

	private void FixedUpdate()
	{
		//Function that correctly rotates the camera based on the joystick / mouse and follows the player (the delta time is sent to be independent of the fps)
		float h = Input.GetAxis("Mouse X");
		float v = Input.GetAxis("Mouse Y");

		//float c_h = Input.GetAxis("RightAxis X");
		//float c_v = Input.GetAxis("RightAxis Y");

		float targetSpeed = mouseSpeed;

		/*if (c_h != 0 || c_v != 0)
		{ //Overwrites if i use joystick
			h = c_h;
			v = -c_v;
			targetSpeed = controllerSpeed; 
		}*/
		if(target != null)
		{
			if(!FreeCam) FollowTarget(Time.deltaTime); //Follow player
			HandleRotations(Time.deltaTime, v, h, targetSpeed); //Rotates camera
			//transform.localPosition = new Vector3(0, target.transform.localPosition.y ,0);
		}
		else
		{
			if(!isFinish && !ranking)
			{
				GameObject player = GameObject.FindGameObjectWithTag("LocalPlayer");
				if(player != null) 
				{
					target = player.transform.GetChild(0).transform;
					transform.SetParent(player.transform);
					transform.localPosition = new Vector3(0, 1.5f ,0);
				}
			}		
		}		
	}

	private void LateUpdate()
	{
		//Here begins the code that is responsible for bringing the camera closer by detecting wall
		float dist = cameraDist + 1.0f; // distance to the camera + 1.0 so the camera doesnt jump 1 unit in if it hits someting far out
		Ray ray = new Ray(camTrans.parent.position, camTrans.position - camTrans.parent.position);// get a ray in space from the target to the camera.
		RaycastHit hit;
		// read from the taret to the targetPosition;
		if (Physics.Raycast(ray, out hit, dist))
		{
			if (hit.transform.tag == "Wall")
			{
				// store the distance;
				dist = hit.distance - 0.25f;
			}
		}
		// check if the distance is greater than the max camera distance;
		if (dist > cameraDist) dist = cameraDist;
		camTrans.localPosition = new Vector3(0, 0, -dist);
	}

	void Update()
	{
		// if(target != null)
		// {
		// 	transform.localPosition = new Vector3(0, target.transform.localPosition.y ,0);
		// }
		// foreach(var value in otherTarget)
		// {
		// 	if(value == null)
		// 		otherTarget.Remove(value);
		// }
		if(!isFinish || ranking)
		{
			CameraMode.text = "";
		}
		if(Input.GetKeyDown(KeyCode.E))
		{
			if(isFinish && !ranking && !FreeCam)
			{
				if(target != null)
				{
					Index = Index == otherTarget.Count-1? 0 : Index+1;
					ChangeTarget(Index);
					Debug.Log("Change Target ++");
				}
				else if(target == null && otherTarget.Count > 0)
				{
					Index = 0;
					ChangeTarget(Index);
				}
			}
			
		}
		if(Input.GetKeyDown(KeyCode.Q))
		{
			if(isFinish && !ranking && !FreeCam)
			{	
				if(target != null)
				{	
					Index = Index == 0? otherTarget.Count-1 : Index-1;
					ChangeTarget(Index);
					Debug.Log("Change Target --");
				}
				else if(target == null && otherTarget.Count > 0)
				{
					Index = 0;
					ChangeTarget(Index);
				}
			}
		}

		if(Input.GetKeyDown(KeyCode.Space))
		{
			if(isFinish && !ranking)
			{
				ToggleFreeCamMode();
			}
		}

		if(FreeCam)
		{
			CameraMode.text = "Press Space To Change Camera Mode\nFree Camera; Move: W,A,S,D; Up: E; Down: Q; Speed: Left Shift";
			float speed = Input.GetKey(KeyCode.LeftShift)? 15 : 10f;
			var h = Input.GetAxisRaw("Horizontal");
			var v = Input.GetAxisRaw("Vertical");
        	Vector3 v2 = v * transform.forward; //Vertical axis to which I want to move with respect to the camera
			Vector3 h2 = h * transform.right; //Horizontal axis to which I want to move with respect to the camera
        	Vector3 movDir = (v2+h2).normalized;
			bool XBoundary = transform.position.x <= 500 && transform.position.x >= -500;
			bool YBoundary = transform.position.y <= 50 && transform.position.y >= -20;
			bool ZBoundary = transform.position.z <= 500 && transform.position.z >= -500;
					
			if(Input.GetKey(KeyCode.E))
			{
				if(XBoundary && YBoundary && ZBoundary)
					transform.Translate((movDir + Vector3.up) * Time.deltaTime * speed, Space.World);
				else
					transform.Translate((-movDir + Vector3.down) * Time.deltaTime * 30, Space.World);
			}
			else if(Input.GetKey(KeyCode.Q))
			{
				if(XBoundary && YBoundary && ZBoundary)
					transform.Translate((movDir + Vector3.down) * Time.deltaTime * speed, Space.World);
				else
					transform.Translate((-movDir + Vector3.up) * Time.deltaTime * 30, Space.World);
			}
			else
			{
				if(XBoundary && YBoundary && ZBoundary)
					transform.Translate(movDir * Time.deltaTime * speed, Space.World);
				else
					transform.Translate(-movDir * Time.deltaTime * 30, Space.World);
			}

		}			
	}
	public void ToggleFreeCamMode()
	{
		FreeCam = !FreeCam;
		if(!FreeCam && isFinish && !ranking)
		{
			ChangeTarget(Index);
		}
		else if(FreeCam && isFinish && !ranking)
		{
			transform.parent = null;
		}
	}

	public void FinishRun(GameObject var)
	{
		isFinish = true;
		playersList = gameManager.players;
		//otherTarget = GameObject.FindGameObjectsWithTag("Bot").ToList();
		foreach(var otherPlayer in playersList.Values)
		{
			otherTarget.Add(otherPlayer.transform.GetChild(0).gameObject);
		}
		Index = 0;
		if(otherTarget.Count <= 0)
			target = null;
		//target = otherTarget[Index].transform;
		CameraMode.text = "Press Space To Change Camera Mode\nFix Camera TargetID: "
		+ otherTarget[Index].transform.GetChild(2).GetChild(0).GetComponent<TMP_Text>().text;
	}

	public void ChangeTarget(int IndexValue)
	{
		if(otherTarget[IndexValue] != null)
		{
			target = otherTarget[IndexValue].transform;		
			transform.SetParent(otherTarget[IndexValue].transform.parent.transform);
			transform.localPosition = new Vector3(0, otherTarget[IndexValue].transform.localPosition.y, 0);
			CameraMode.text = "Press Space To Change Camera Mode\nFix Camera TargetID: "
			+ otherTarget[Index].transform.GetChild(2).GetChild(0).GetComponent<TMP_Text>().text;
		}
		else
		{
			otherTarget.RemoveAt(IndexValue);
			Index = 0;
			target = otherTarget[0].transform;		
			transform.SetParent(otherTarget[0].transform.parent.transform);
			transform.localPosition = new Vector3(0, otherTarget[0].transform.localPosition.y, 0);
			CameraMode.text = "Press Space To Change Camera Mode\nFix Camera TargetID: "
			+ otherTarget[0].transform.GetChild(2).GetChild(0).GetComponent<TMP_Text>().text;
		}			
	}

	public void Ranking(bool value)
	{
		ranking = true;
		FreeCam = false;
		target = null;
	}

	public void ChangeTargetList(int Id, GameObject targetRemoved)
	{
		//Debug.Log("Ve Dich roi xoa thang khac thoi");
		if(isFinish)
		{	
			if(!FreeCam)
				ChangeTarget(Index-1 < 0? 0 : Index-1);
			//Debug.Log("Xoa Xoa Xoa");
			playersList.Remove(Id);
			foreach(var value in otherTarget)
			{
				Debug.Log(value);
				if(value == targetRemoved || value == null)
				{
					otherTarget.Remove(value);
				}
			}
		}
	}
	public static CameraManager singleton; //You can call CameraManager.singleton from other script (There can be only one)
	void Awake()
	{
		singleton = this; //Self-assigns
		Init();
	}

}

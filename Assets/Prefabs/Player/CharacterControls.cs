using System;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using Unity.Netcode;
using DG.Tweening;
using TMPro;

[RequireComponent (typeof (Rigidbody))]
[RequireComponent (typeof (CapsuleCollider))]

public class CharacterControls : MonoBehaviour 
{	
	public float speed;
	public float norSpeed = 10f;
	public float Slowspeed = 5f;
	public float Runspeed = 15f;
	public float airVelocity = 8f;
	public float gravity = 9.81f;
	public float maxVelocityChange = 9.81f;
	public float jumpHeight = 2.0f;
	public float maxFallSpeed = 20.0f;
	public float rotateSpeed = 25f; //Speed the player rotate
	private Vector3 moveDir;
	public GameObject cam;
	private Rigidbody rb;
	private bool canJump;
	private Vector3 verticalMove;
	private Vector3 horizontalMove;
	public float distToGround;
	private bool isPush = false;
	private bool isPull = false;
	private Vector3 direction;
	public bool canMove = false; //Lock player movement or not
	private bool isStuned = false;
	private bool wasStuned = false; //If player was stunned before get stunned another time
	private float pushForce;
	private Vector3 pushDir;
	public Transform startCheckPoint;
	private Vector3 checkPoint;
	private bool slide = false;
    public bool isDead = false;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    [SerializeField] private bool checkGround;
	public static event Action<GameObject> FinishRun;

	private bool isClient;
	public bool isGroundCheck = true;
	public float disSetTest = 1;
	public bool isMove = false;
	public bool isFall = false;
	public Animator ChaAnimator;
    //private bool isRunning = false;
	//private float gravityValue = -9.81f;

	// public override void OnNetworkSpawn()
	// {
	// 	isClient = !IsOwner;
	// }

	void Awake() 
	{
		rb = GetComponent<Rigidbody>();
		rb.freezeRotation = true;
		rb.useGravity = false;
		verticalMove = Vector3.zero;
		horizontalMove = Vector3.zero;
		//checkPoint = GameObject.FindGameObjectWithTag("StartCheckPoint").transform.position;
		//cam = GameObject.FindGameObjectWithTag("FreeLook3rdCam");
	}
	void  Start()
	{
		// get the distance to ground
		distToGround = GetComponent<Collider>().bounds.extents.y;
		checkPoint = transform.position;
		speed = norSpeed;
		ChaAnimator = transform.GetChild(1).GetComponent<Animator>();
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
		//if(checkPoint != null) LoadCheckPoint();
		//checkPoint = startCheckPoint.position;
	}
	
	void FixedUpdate () 
	{
		// if(!isClient)
		moveDir = (verticalMove + horizontalMove).normalized;
		PlayerMovement();
	}

	private void Update()
	{
		//float horizontalMove = Input.GetAxis("Horizontal");
		//float verticalMove = Input.GetAxis("Vertical");
		if(Input.GetKeyDown(KeyCode.LeftAlt))
        {
            transform.GetChild(2).GetChild(0).GetComponent<TMP_Text>().enabled = !transform.GetChild(2).GetChild(0).GetComponent<TMP_Text>().enabled;
			transform.GetChild(2).GetChild(1).GetComponent<MeshRenderer>().enabled = !transform.GetChild(2).GetChild(1).GetComponent<MeshRenderer>().enabled;
        }
		if(IsGrounded())
		{
			ChaAnimator.SetBool("InAir", false);
		}
		// else
		// {
		// 	ChaAnimator.SetBool("InAir", false);
		// }
		if(Input.GetKey(KeyCode.Alpha1))
		{
			ChaAnimator.SetTrigger("Happy");
			
		}
		else if(Input.GetKey(KeyCode.Alpha2))
		{
			ChaAnimator.SetTrigger("Hello");
			
		}
		else if(Input.GetKey(KeyCode.Alpha3))
		{
			ChaAnimator.SetTrigger("Sad");
			
		}
		else if(Input.GetKey(KeyCode.Alpha4))
		{
			ChaAnimator.SetTrigger("Talk");
			
		}
		else if(Input.GetKey(KeyCode.Alpha5))
		{
			ChaAnimator.SetTrigger("Tired");
			
		}
		else if(Input.GetKey(KeyCode.Alpha6))
		{
			ChaAnimator.SetTrigger("Touch");
			
		}
		else if(Input.GetKey(KeyCode.Alpha7))
		{
			ChaAnimator.SetTrigger("Dance1");
			
		}
		else if(Input.GetKey(KeyCode.Alpha8))
		{
			ChaAnimator.SetTrigger("Dance2");
			
		}
		else if(Input.GetKey(KeyCode.Alpha9))
		{
			ChaAnimator.SetTrigger("Dance3");
			
		}

		if((transform.localPosition.x != 0 || transform.localPosition.z != 0) && canMove)
		{
			transform.localPosition = new Vector3(0, 0, 0);
		}
		if(transform.localPosition.y < 0 && !isFall)
		{
			StartCoroutine(KillZoneReach());
		}

		// Vector3 v2 = verticalMove * cam.transform.forward; //Vertical axis to which I want to move with respect to the camera
		// Vector3 h2 = horizontalMove * cam.transform.right; //Horizontal axis to which I want to move with respect to the camera
		moveDir = (verticalMove + horizontalMove).normalized; //Global position to which I want to move in magnitude 1
		//moveDir = new Vector3(horizontalMove, 0, verticalMove).normalized;
		RaycastHit hit;
		if (Physics.Raycast(transform.position, -Vector3.up, out hit, distToGround + disSetTest))
		{
			if (hit.transform.tag == "Slide")
			{
				slide = true;
			}
			else
			{
				slide = false;
			}
		}
	}

	public void OnCollisionEnter(Collision other) 
	{
		if(other.gameObject.tag == "Startplat" || other.gameObject.tag == "NorPlat"
		|| other.gameObject.tag == "FastPlat" || other.gameObject.tag == "SlowPlat" 
		|| other.gameObject.tag == "EndPlat" || other.gameObject.tag == "PullPlat"
		|| other.gameObject.tag == "PushPlat")
		{
			checkGround = true;
		}
	}
	public void OnCollisionExit(Collision other) 
	{
		if(other.gameObject.tag == "Startplat" || other.gameObject.tag == "NorPlat"
		|| other.gameObject.tag == "FastPlat" || other.gameObject.tag == "SlowPlat" 
		|| other.gameObject.tag == "EndPlat" || other.gameObject.tag == "PullPlat"
		|| other.gameObject.tag == "PushPlat")
		{
			checkGround = false;
		}
	}
	bool IsGrounded()
	{
		isGroundCheck = Physics.Raycast(transform.position, -Vector3.up, distToGround + disSetTest);
		return isGroundCheck;
	}

	public void SetJump(bool JumpInput)
	{
		canJump = JumpInput;
	}	
	public void InputProcess(Vector3 h, Vector3 v)
	{
		horizontalMove = h;
		verticalMove = v;
	}

	public void PlayerMovement()
	{
		if(canMove)
		{
			if((moveDir.x != 0 || moveDir.z != 0) && IsGrounded())
			{
				Vector3 targetDir = moveDir; //Direction of the character

				targetDir.y = 0;
				if (targetDir == Vector3.zero)
					targetDir = transform.forward;
				Quaternion tr = Quaternion.LookRotation(targetDir); //Rotation of the character to where it moves
				Quaternion targetRotation = Quaternion.Slerp(transform.rotation, tr, Time.deltaTime * rotateSpeed); //Rotate the character little by little
				transform.rotation = targetRotation;
				isMove = true;
				//ChaAnimator.SetBool("isIdle", true);				
			}
			else if(moveDir.x == 0 && moveDir.z == 0)
			{
				//ChaAnimator.SetTrigger("Idle");
				isMove = false;
			}
			ChaAnimator.SetBool("IsMove", isMove);
			if(Input.GetKey(KeyCode.Space) && IsGrounded())
			{
				//rb.velocity = new Vector3(0, CalculateJumpVerticalSpeed(), 0);
				rb.AddForce(new Vector3(0, CalculateJumpVerticalSpeed(), 0), ForceMode.Impulse);
				ChaAnimator.SetBool("InAir", true);
				//ChaAnimator.SetTrigger("Jump");
				//Debug.Log("Jumping");
				//transform.DOMoveY(3f, 0.25f).SetEase(Ease.Linear).SetLoops(1, LoopType.Yoyo).SetLink(transform.gameObject);
			}
			// if(horizontalMove != 0 && IsGrounded())
			// {
			// 	Vector3 targetDir = moveDir; //Direction of the character

			// 	transform.Rotate(0.0f, horizontalMove * 5, 0.0f);
			// }

			// if(checkGround)
			// {
			//  	// // Calculate how fast we should be moving
			// 	// Vector3 targetVelocity = moveDir;
			// 	// targetVelocity *= speed;

			// 	// // // Apply a force that attempts to reach our target velocity
			// 	// Vector3 velocity = rb.velocity;
			// 	// if(targetVelocity.magnitude < velocity.magnitude) //If I'm slowing down the character
			// 	// {
			// 	// 	targetVelocity = velocity;
			// 	// 	rb.velocity /= 1.1f;
			// 	// }
			// 	// Vector3 velocityChange = (targetVelocity - velocity);
			// 	// velocityChange.x = Mathf.Clamp(velocityChange.x, -maxVelocityChange, maxVelocityChange);
			// 	// velocityChange.z = Mathf.Clamp(velocityChange.z, -maxVelocityChange, maxVelocityChange);
			// 	// velocityChange.y = 0;
			// 	// if(!slide)
			// 	// {
			// 	// 	if(Mathf.Abs(rb.velocity.magnitude) < speed * 1.0f)
			// 	// 		rb.AddForce(velocityChange, ForceMode.VelocityChange);
			// 	// }
			// 	// else if(Mathf.Abs(rb.velocity.magnitude) < speed * 1.0f)
			// 	// {
			// 	// 	rb.AddForce(moveDir * 0.15f, ForceMode.VelocityChange);
			// 	// 	//Debug.Log(rb.velocity.magnitude);
			// 	// }
			// 	// if(verticalMove > 0)
			// 	// {
			// 	// 	rb.AddForce(transform.forward * speed * Time.deltaTime, ForceMode.Impulse);
			// 	// }
			// 	// else if(verticalMove < 0)
			// 	// {
			// 	// 	rb.AddForce(-transform.forward * speed * Time.deltaTime, ForceMode.Impulse);
			// 	// }

			// 	// Jump
			// 	if(Input.GetKey(KeyCode.Space))
			// 	{
			// 		rb.velocity = new Vector3(0, CalculateJumpVerticalSpeed(), 0);
			// 		ChaAnimator.SetBool("InAir", true);
			// 		//ChaAnimator.SetTrigger("Jump");
			// 		//Debug.Log("Jumping");
			// 		//transform.DOMoveY(3f, 0.25f).SetEase(Ease.Linear).SetLoops(1, LoopType.Yoyo).SetLink(transform.gameObject);
			// 	}
			// }
			// else
			// {
			// 	if(!slide)
			// 	{
			// 		Vector3 targetVelocity = new Vector3(moveDir.x * airVelocity, rb.velocity.y, moveDir.z * airVelocity);
			// 		Vector3 velocity = rb.velocity;
			// 		Vector3 velocityChange = (targetVelocity - velocity);
			// 		velocityChange.x = Mathf.Clamp(velocityChange.x, -maxVelocityChange, maxVelocityChange);
			// 		velocityChange.z = Mathf.Clamp(velocityChange.z, -maxVelocityChange, maxVelocityChange);
			// 		rb.AddForce(velocityChange, ForceMode.VelocityChange);
			// 		if (velocity.y < -maxFallSpeed)
			// 			rb.velocity = new Vector3(velocity.x, -maxFallSpeed, velocity.z);
			// 	}
			// 	else if (Mathf.Abs(rb.velocity.magnitude) < speed * 1.0f)
			// 	{
			// 		rb.AddForce(moveDir * 0.15f, ForceMode.VelocityChange);
			// 	}
			// }
			if(isPush)
			{
				transform.Translate(direction * speed*0.75f * Time.deltaTime, Space.World);
			}
			if(isPull)
			{
				transform.Translate(direction * -speed*0.75f * Time.deltaTime, Space.World);
			}
		}
		else
		{
			rb.velocity = pushDir * pushForce;
		}
		// We apply gravity manually for more tuning control
		rb.AddForce(new Vector3(0, -gravity * GetComponent<Rigidbody>().mass, 0));
	}

	float CalculateJumpVerticalSpeed () {
		// From the jump height and gravity we deduce the upwards speed 
		// for the character to reach at the apex.
		return Mathf.Sqrt(2 * jumpHeight * gravity);
	}

	// public void HitPlayer(Vector3 velocityF, float time)
	// {
	// 	rb.velocity = velocityF;

	// 	pushForce = velocityF.magnitude;
	// 	pushDir = Vector3.Normalize(velocityF);
	// 	StartCoroutine(Decrease(velocityF.magnitude, time));
	// }

	public void SetCheckPoint(Vector3 newCheckPoint)
	{
		checkPoint = newCheckPoint;
	}
	public void LoadCheckPoint()
	{
		Debug.Log("Respawn");
		transform.position = checkPoint;
	}
	IEnumerator KillZoneReach()
	{
		isFall = true;
		ChaAnimator.SetBool("InAir", true);
		yield return new WaitForSeconds(1f);
		transform.localPosition = new Vector3(0, 6f, 0);
		isFall = false;
	}

	// private IEnumerator Decrease(float value, float duration)
	// {
	// 	if (isStuned)
	// 		wasStuned = true;
	// 	isStuned = true;
	// 	canMove = false;

	// 	float delta = 0;
	// 	delta = value / duration;

	// 	for (float t = 0; t < duration; t += Time.deltaTime)
	// 	{
	// 		yield return null;
	// 		if (!slide) //Reduce the force if the ground isnt slide
	// 		{
	// 			pushForce = pushForce - Time.deltaTime * delta;
	// 			pushForce = pushForce < 0 ? 0 : pushForce;
	// 			//Debug.Log(pushForce);
	// 		}
	// 		rb.AddForce(new Vector3(0, -gravity * GetComponent<Rigidbody>().mass, 0)); //Add gravity
	// 	}

	// 	if (wasStuned)
	// 	{
	// 		wasStuned = false;
	// 	}
	// 	else
	// 	{
	// 		isStuned = false;
	// 		canMove = true;
	// 	}
	// }

	public void SpeedUp()
	{
		speed = Runspeed;
	}

	public void Slow()
	{
		speed = Slowspeed;
	}

	public void BackToNorSpeed()
	{
		speed = norSpeed;
	}

	public void Push(bool value, Vector3 direc = default(Vector3))
	{
		isPush = value;
		if(isPush)
			direction = direc;
	}
	public void Pull(bool value, Vector3 direc = default(Vector3))
	{
		isPull = value;
		if(isPull)
			direction = direc;
	}

	public void Winning()
	{
		//Player Win, do st...
		Debug.Log("You Win");
		FinishRun?.Invoke(gameObject);
		canMove = false;
		gameObject.SetActive(false);
	}

	public void TurnOffMove(bool value)
	{
		canMove = false;
	}
	void OnEnable() 
    {
        NakamaConnect.EndGame += TurnOffMove;
    }

    void OnDisable()
    {
        NakamaConnect.EndGame -= TurnOffMove;
    }
}

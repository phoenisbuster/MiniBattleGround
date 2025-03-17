using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerInputController : MonoBehaviour
{
    [HideInInspector] public float HorizontalInput = 0;
    [HideInInspector] public float VerticalInput = 0;
    [HideInInspector] public bool Jump;
    public bool InputChanged;
    public bool isMoving = false;
    public bool willRotateIfMoving = false;
    public float FacingAngular;
    public float CamAngular;
    public GameObject cam;
    public Transform player;
    public float LerpTime = 0.25f;
    private NakamaConnect gameManager;
    private float lerpTimer;
    private Vector3 lerpFromPosition;
    private Vector3 lerpToPosition;
    public bool lerpPosition;
    private bool Send = false;
    private CharacterControls playerMovementController;
    //private PlayerWeaponController playerWeaponController;

    /// <summary>
    /// Called by Unity when this GameObject starts.
    /// </summary>
    void Awake()
    {
        cam = GameObject.FindGameObjectWithTag("FreeLook3rdCam");
    }
    private void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<NakamaConnect>();
        playerMovementController = GetComponentInChildren<CharacterControls>();
        player = transform.GetChild(0);
        FacingAngular = player.rotation.eulerAngles.y;
        CamAngular = cam.transform.rotation.eulerAngles.y;
        //playerWeaponController = GetComponentInChildren<PlayerWeaponController>();
    }

    /// <summary>
    /// Called by Unity every frame.
    /// </summary>
    private void Update()
    {
        // Get the current input states.
        var h = Input.GetAxisRaw("Horizontal");
		var v = Input.GetAxisRaw("Vertical");
        Vector3 v2 = v * cam.transform.forward; //Vertical axis to which I want to move with respect to the camera
		Vector3 h2 = h * cam.transform.right; //Horizontal axis to which I want to move with respect to the camera
        Vector3 movDir = (v2+h2).normalized;
        // var jump = Input.GetButton("Jump");
        //FacingAngular = player.rotation.eulerAngles.y;
        //CamAngular = cam.transform.rotation.eulerAngles.y;

        InputChanged = (h != HorizontalInput || v != VerticalInput || CamAngular != cam.transform.rotation.eulerAngles.y);
        if(!playerMovementController.canMove)
        {
            InputChanged = false;
        }
        
        if(h != 0 || v != 0)
        {
            isMoving = true;
            if(v > 0 && h == 0)
            {
                FacingAngular = cam.transform.rotation.eulerAngles.y;
            }
            if(v > 0 && h > 0)
            {
                FacingAngular = cam.transform.rotation.eulerAngles.y + 45f;
            }
            if(v == 0 && h > 0)
            {
                FacingAngular = cam.transform.rotation.eulerAngles.y + 90f;
            }
            if(v < 0 && h > 0)
            {
                FacingAngular = cam.transform.rotation.eulerAngles.y + 135f;
            }
            if(v < 0 && h == 0)
            {
                FacingAngular = cam.transform.rotation.eulerAngles.y <= 0? cam.transform.rotation.eulerAngles.y + 180f : cam.transform.rotation.eulerAngles.y - 180f;
            }
            if(v < 0 && h < 0)
            {
                FacingAngular = cam.transform.rotation.eulerAngles.y - 135f;
            }
            if(v == 0 && h < 0)
            {
                FacingAngular = cam.transform.rotation.eulerAngles.y - 90f;
            }
            if(v > 0 && h < 0)
            {
                FacingAngular = cam.transform.rotation.eulerAngles.y - 45f;
            }
            //player.rotation = Quaternion.Euler(0, FacingAngular, 0);
        }
        else
        {
            isMoving = false;
            FacingAngular = player.rotation.eulerAngles.y;
        }

        // if(FacingAngular != CamAngular)
        // {
        //     CamAngular = FacingAngular;
        //     willRotateIfMoving = true;
        // }
        // Cache the new input states in public variables that can be read elsewhere.
        HorizontalInput = h;
        VerticalInput = v;
        CamAngular = cam.transform.rotation.eulerAngles.y;
        // Set inputs on Player Controllers.
        playerMovementController.InputProcess(h2, v2);
        //playerMovementController.SetJump(Jump);
        //playerMovementController.SetJumpHeld(JumpHeld);

        // var angle = player.rotation.eulerAngles.y;
        // var move = true;
        // CamAngular = cam.transform.rotation.eulerAngles.y;
        // if(playerMovementController.canMove)
        // {
        //     if(Input.GetKey(KeyCode.W))
        //     {
        //         angle = CamAngular;
        //         gameManager.SendWebSocketMessage(move, angle);
        //     }
        //     if(Input.GetKey(KeyCode.A))
        //     {
        //         angle = CamAngular - 90f;
        //         gameManager.SendWebSocketMessage(move, angle);
        //     }
        //     if(Input.GetKey(KeyCode.S))
        //     {
        //         angle = CamAngular - 180f;
        //         gameManager.SendWebSocketMessage(move, angle);
        //     }
        //     if(Input.GetKey(KeyCode.D))
        //     {
        //         angle = CamAngular + 90f;
        //         gameManager.SendWebSocketMessage(move, angle);
        //     }
        //     if(Input.GetKeyUp(KeyCode.W) ||Input.GetKeyUp(KeyCode.A) ||Input.GetKeyUp(KeyCode.S) ||Input.GetKeyUp(KeyCode.D))
        //     {
        //         angle = player.rotation.eulerAngles.y;
        //         gameManager.SendWebSocketMessage(false, angle);
        //     }
        //     player.localRotation = Quaternion.Euler(0, angle, 0);
        // }
    }

    public void UpdateVelocityAndPositionFromState(Vector3 position)
    {
        //var stateDictionary = GetStateAsDictionary(state);

        //playerRigidbody.velocity = new Vector3(float.Parse(stateDictionary["velocity.x"]), float.Parse(stateDictionary["velocity.y"]), float.Parse(stateDictionary["velocity.z"]));

        // var position = new Vector3(
        //     float.Parse(stateDictionary["position.x"]),
        //     float.Parse(stateDictionary["position.y"]),
        //     float.Parse(stateDictionary["position.z"]));

        // Begin lerping to the corrected position.
        lerpFromPosition = transform.GetChild(0).position;
        lerpToPosition = position;
        lerpTimer = 0;
        lerpPosition = true;
    }

    public void UpdatePos(Vector3 Pos, float ping3)
    {
        //transform.DOMove(new Vector3(Pos.x, 0, Pos.z), 0.02f*0.1f + ping3*0.9f).SetEase(Ease.Linear).SetLink(gameObject);
        transform.position = new Vector3(Pos.x, 0, Pos.z);
        //cam.GetComponent<CameraManager>().FollowTarget(transform.position);
    }
    private void LateUpdate()
    {
        // If we aren't trying to interpolate the player's position then return early.
        if (!lerpPosition)
        {
            return;
        }

        // Interpolate the player's position based on the lerp timer progress.
        transform.GetChild(0).position = Vector3.Lerp(lerpFromPosition, lerpToPosition, lerpTimer / LerpTime);
        lerpTimer += Time.deltaTime;

        // If we have reached the end of the lerp timer, explicitly force the player to the last known correct position.
        if (lerpTimer >= LerpTime)
        {
            transform.GetChild(0).position = lerpToPosition;
            lerpPosition = false;
        }
    }
}

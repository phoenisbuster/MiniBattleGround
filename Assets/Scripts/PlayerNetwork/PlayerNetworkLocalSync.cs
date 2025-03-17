using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Syncs the local player's state across the network by sending frequent network packets containing relevent information such as velocity, position and inputs.
/// </summary>
public class PlayerNetworkLocalSync : MonoBehaviour
{
    /// <summary>
    /// How often to send the player's velocity and position across the network, in seconds.
    /// </summary>
    public float StateFrequency = 0.1f;

    private NakamaConnect gameManager;
    //private PlayerHealthController playerHealthController;
    private PlayerInputController playerInputController;
    private Rigidbody playerRigidbody;
    private Transform playerTransform;
    private float stateSyncTimer;
    private bool Sending = false;
    private bool AnotherSending = false;
    private float RotationAngular = 0;
    public int LocalUserID;

    /// <summary>
    /// Called by Unity when this GameObject starts.
    /// </summary>
    private void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<NakamaConnect>();
        playerInputController = GetComponent<PlayerInputController>();
        playerRigidbody = GetComponentInChildren<Rigidbody>();
        playerTransform = playerRigidbody.GetComponent<Transform>();
        LocalUserID = PlayerPrefs.GetInt("LocalUserID");
        playerTransform.transform.localPosition = new Vector3(0, 1.5f, 0);
    }

    /// <summary>
    /// Called by Unity every frame after all Update calls have been made.
    /// </summary>
    private void LateUpdate()
    {
        
        //Send the players current velocity and position every StateFrequency seconds.
        // if (stateSyncTimer <= 0)
        // {
        //     // Send a network packet containing the player's velocity and position.
        //     // gameManager.SendMatchState(
        //     //     OpCode.VelocityAndPosition,
        //     //     MatchDataJson.VelocityAndPosition(playerRigidbody.velocity, playerTransform.position, playerTransform.rotation));

        //     stateSyncTimer = StateFrequency;
        // }
        // stateSyncTimer -= Time.deltaTime;
        

        // If the players input hasn't changed, return early.
        if (!playerInputController.InputChanged)
        {
            return;
        }
        else if(playerInputController.InputChanged)
        {
            if(!playerInputController.isMoving && !Sending)
            {
                gameManager.SendWebSocketMessage(playerInputController.isMoving, playerInputController.FacingAngular);
                Sending = true;
                AnotherSending = false;
            }
            else if(playerInputController.isMoving)
            {
                gameManager.SendWebSocketMessage(playerInputController.isMoving, playerInputController.FacingAngular);
                Sending = false;
            }
            //gameManager.SendWebSocketMessage(playerInputController.isMoving, playerInputController.FacingAngular);          
        }
        // Send network packet with the player's current input.
        // gameManager.SendMatchState(
        //     OpCode.Input, 
        //     MatchDataJson.Input(playerInputController.HorizontalInput, playerInputController.VerticalInput, playerInputController.Jump)
        // );   
    }
    public void UpdatePos(Vector3 Pos, float ping3)
    {
        //transform.DOMove(new Vector3(Pos.x, 0, Pos.z), 0.02f*0.1f + ping3*0.9f).SetEase(Ease.Linear).SetLink(gameObject);
        transform.position = new Vector3(Pos.x, 0, Pos.z);
        //playerTransform.GetComponent<CharacterControls>().ChaAnimator.SetTrigger("Run");
        //cam.GetComponent<CameraManager>().FollowTarget(transform.position);
    } 
}

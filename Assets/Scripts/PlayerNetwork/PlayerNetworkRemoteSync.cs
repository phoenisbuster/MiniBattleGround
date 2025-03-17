/*
Copyright 2021 Heroic Labs

Licensed under the Apache License, Version 2.0 (the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at

    http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.
*/

//using Nakama;
//using Nakama.TinyJson;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using TMPro;

/// <summary>
/// Syncs a remotely connected player's character using received network data.
/// </summary>
public class PlayerNetworkRemoteSync : MonoBehaviour
{
    public RemotePlayerNetworkData NetworkData;

    /// <summary>
    /// The speed (in seconds) in which to smoothly interpolate to the player's actual position when receiving corrected data.
    /// </summary>
    public float LerpTime = 0.05f;

    private NakamaConnect gameManager;
    private CharacterControls playerMovementController;
    //private PlayerWeaponController playerWeaponController;
    private Rigidbody playerRigidbody;
    private Transform playerTransform;
    private float lerpTimer;
    private Vector3 lerpFromPosition;
    private Vector3 lerpToPosition;
    private bool lerpPosition;
    float OldPosx;
    float OldPosz;
    public Animator ChaAnimator;
    public bool isFall = false;

    /// <summary>
    /// Called by Unity when this GameObject starts.
    /// </summary>
    public void Awake()
    {
        ChaAnimator = transform.GetChild(0).GetChild(1).GetComponent<Animator>();
    }
    private void Start()
    {
        // Cache a reference to the required components.
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<NakamaConnect>();
        //playerMovementController = GetComponentInChildren<CharacterControls>();
        playerRigidbody = GetComponentInChildren<Rigidbody>();
        playerTransform = playerRigidbody.GetComponent<Transform>();
        playerTransform.transform.localPosition = new Vector3(0, 1.5f, 0);
        OldPosx = transform.position.x;
        OldPosz = transform.position.z;
        //ChaAnimator = transform.GetChild(0).GetChild(1).GetComponent<Animator>();
        // Add an event listener to handle incoming match state data.
        //gameManager.NakamaConnection.Socket.ReceivedMatchState += EnqueueOnReceivedMatchState;
    }

    /// <summary>
    /// Called by Unity after all Update calls have been made.
    /// </summary>
    private void LateUpdate()
    {
        // If we aren't trying to interpolate the player's position then return early.
        if (!lerpPosition)
        {
            return;
        }

        // Interpolate the player's position based on the lerp timer progress.
        playerTransform.position = Vector3.Lerp(lerpFromPosition, lerpToPosition, lerpTimer / LerpTime);
        lerpTimer += Time.deltaTime;

        Vector3 targetDir = lerpToPosition; //Direction of the character
        targetDir.y = 0;
        if (targetDir == Vector3.zero)
            targetDir = transform.forward;
        Quaternion tr = Quaternion.LookRotation(targetDir); //Rotation of the character to where it moves
        Quaternion targetRotation = Quaternion.Slerp(transform.rotation, tr, Time.deltaTime * 25); //Rotate the character little by little
        transform.rotation = targetRotation;
        
        // If we have reached the end of the lerp timer, explicitly force the player to the last known correct position.
        if (lerpTimer >= LerpTime)
        {
            playerTransform.position = lerpToPosition;
            lerpPosition = false;
        }
    }

    /// <summary>
    /// Called when this GameObject is being destroyed.
    /// </summary>
    private void OnDestroy()
    {
        if(gameManager != null)
        {
            //gameManager.NakamaConnection.Socket.ReceivedMatchState -= EnqueueOnReceivedMatchState;
        }
    }

    /// <summary>
    /// Passes execution of the event handler to the main unity thread so that we can interact with GameObjects.
    /// </summary>
    /// <param name="matchState">The incoming match state data.</param>
    // private void EnqueueOnReceivedMatchState(IMatchState matchState)
    // {
    //     var mainThread = UnityMainThreadDispatcher.Instance();
    //     mainThread.Enqueue(() => OnReceivedMatchState(matchState));
    // }

    /// <summary>
    /// Called when receiving match data from the Nakama server.
    /// </summary>
    /// <param name="matchState">The incoming match state data.</param>
    // private void OnReceivedMatchState(IMatchState matchState)
    // {
    //     // If the incoming data is not related to this remote player, ignore it and return early.
    //     if (matchState.UserPresence.SessionId != NetworkData.User.SessionId)
    //     {
    //         return;
    //     }
    //     // Decide what to do based on the Operation Code of the incoming state data as defined in OpCodes.
    //     switch (matchState.OpCode)
    //     {
    //         case OpCode.VelocityAndPosition:
    //             UpdateVelocityAndPositionFromState(matchState.State);
    //             break;
    //         case OpCode.Input:
    //             SetInputFromState(matchState.State);
    //             break;
    //         case OpCode.Died:
    //             //playerMovementController.PlayDeathAnimation();
    //             break;
    //         default:
    //             break;
    //     }
    // }

    /// <summary>
    /// Converts a byte array of a UTF8 encoded JSON string into a Dictionary.
    /// </summary>
    /// <param name="state">The incoming state byte array.</param>
    /// <returns>A Dictionary containing state data as strings.</returns>
    // private IDictionary<string, string> GetStateAsDictionary(byte[] state)
    // {
    //     return Encoding.UTF8.GetString(state).FromJson<Dictionary<string, string>>();
    // }

    /// <summary>
    /// Sets the appropriate input values on the PlayerMovementController and PlayerWeaponController based on incoming state data.
    /// </summary>
    /// <param name="state">The incoming state Dictionary.</param>
    // private void SetInputFromState(byte[] state)
    // {
    //     var stateDictionary = GetStateAsDictionary(state);

    //     playerMovementController.InputProcess(float.Parse(stateDictionary["horizontalInput"]), float.Parse(stateDictionary["verticalInput"]));
    //     playerMovementController.SetJump(bool.Parse(stateDictionary["jump"]));
    // }

    /// <summary>
    /// Updates the player's velocity and position based on incoming state data.
    /// </summary>
    /// <param name="state">The incoming state byte array.</param>
    // private void UpdateVelocityAndPositionFromState(byte[] state)
    // {
    //     var stateDictionary = GetStateAsDictionary(state);

    //     playerRigidbody.velocity = new Vector3(float.Parse(stateDictionary["velocity.x"]), float.Parse(stateDictionary["velocity.y"]), float.Parse(stateDictionary["velocity.z"]));

    //     var position = new Vector3(
    //         float.Parse(stateDictionary["position.x"]),
    //         float.Parse(stateDictionary["position.y"]),
    //         float.Parse(stateDictionary["position.z"]));

    //     // Begin lerping to the corrected position.
    //     lerpFromPosition = playerTransform.position;
    //     lerpToPosition = position;
    //     lerpTimer = 0;
    //     lerpPosition = true;
    // }
    public void UpdateVelocityAndPositionFromState(Vector3 position)
    {
        // var stateDictionary = GetStateAsDictionary(state);

        // playerRigidbody.velocity = new Vector3(float.Parse(stateDictionary["velocity.x"]), float.Parse(stateDictionary["velocity.y"]), float.Parse(stateDictionary["velocity.z"]));

        // var position = new Vector3(
        //     float.Parse(stateDictionary["position.x"]),
        //     float.Parse(stateDictionary["position.y"]),
        //     float.Parse(stateDictionary["position.z"]));

        // Begin lerping to the corrected position.
        lerpFromPosition = playerTransform.position;
        lerpToPosition = position;
        lerpTimer = 0;
        lerpPosition = true;
    }

    public void UpdatePos(Vector3 Pos, float ping3, float Facing)
    {
        //transform.DOMove(new Vector3(Pos.x, 0, Pos.z), 0.02f*0.1f + ping3*0.9f).SetEase(Ease.Linear).SetLink(gameObject);
        //cam.GetComponent<CameraManager>().FollowTarget(transform.position);
        transform.position = new Vector3(Pos.x, 0, Pos.z);
        transform.GetChild(0).transform.localRotation = Quaternion.Euler(0, Facing, 0);
        if(Pos.x != OldPosx || Pos.z != OldPosz)
        {
            ChaAnimator.SetBool("IsMove", true);
        }
        else
        {
            ChaAnimator.SetBool("IsMove", false);
        }
        OldPosx = Pos.x;
        OldPosz = Pos.z;
    } 
    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.LeftAlt))
        {
            transform.GetChild(0).GetChild(2).GetChild(0).GetComponent<TMP_Text>().enabled = !transform.GetChild(0).GetChild(2).GetChild(0).GetComponent<TMP_Text>().enabled;
            //transform.GetChild(2).GetChild(1).GetComponent<MeshRenderer>().enabled = !transform.GetChild(2).GetChild(1).GetComponent<MeshRenderer>().enabled;
        }
        if(transform.GetChild(0).localPosition.y < 0 && !isFall)
		{
			StartCoroutine(KillZoneReach());
		}
    }

    IEnumerator KillZoneReach()
	{
		isFall = true;
		//ChaAnimator.SetBool("InAir", true);
		yield return new WaitForSeconds(1f);
		transform.GetChild(0).localPosition = new Vector3(0, 6f, 0);
		isFall = false;
	}
}

using Google.Protobuf;
using MuziNakamaBuffer;
using Networking;
using Opsive.UltimateCharacterController.Character;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NakamaAnimatorMonitor : AnimatorMonitor
{
    private int m_SnappedAbilityIndex;
    private short m_DirtyFlag;
    private byte m_ItemDirtySlot;

    private float m_NetworkHorizontalMovement;
    private float m_NetworkForwardMovement;
    private float m_NetworkPitch;
    private float m_NetworkYaw;
    private float m_NetworkSpeed;
    private float m_NetworkAbilityFloatData;
    public bool isMine = false;

    public NakamaNetworkPlayer nakamaNetworkPlayer;
    float intervalRate = 0.02f;

    protected override void Awake()
    {
        base.Awake();
    }
    protected override void Start()
    {
        base.Start();
        //StartCoroutine(WritingThread());
    }
    protected override void SnapAnimator()
    {
        base.SnapAnimator();

        m_SnappedAbilityIndex = AbilityIndex;
    }
    public void Init()
    {
        nakamaNetworkPlayer = GetComponent<NakamaNetworkPlayer>();
        if (GetComponent<NakamaMyNetworkPlayer>() != null)
        {
            isMine = true;
            //        //Debug.LogError("This is my character");
        }

    }
    //IEnumerator WritingThread(float tickRate = 60)
    //{
    //    yield return new WaitForSeconds(0.1f);
    //    if (GetComponent<NakamaMyNetworkPlayer>() != null)
    //    {
    //        isMine = true;
    //        //Debug.LogError("This is my character");
    //    }
    //    if (isMine)
    //    {
    //        tickRate = 1.0f / tickRate;
    //        while (true)
    //        {
    //            Debug.Log("AAAAAAAAAAAAAAAAAAA: " + tickRate);
    //            yield return new WaitForSeconds(tickRate);
    //            SendAnimatorData();
    //        }
    //        Debug.Log("BBBBBBBBBBBBBBB");
    //    }
    //}
    bool lastMoveing;
    bool lastAmiming;
    float lastPitch;
    float lastYaw;
    float lastHorizontalMovement;
    float lastForwardMovement;
    float lastSpeed;
    int lastHeight;
    int lastMovementSetID;
    int lastAbilityIndex;
    int lastAbilityIntData;
    float lastAbilityFloatData;
    void SendAnimatorData()
    {
        UNBufUCCAnimatorStates states = new UNBufUCCAnimatorStates { InMatchUserId = nakamaNetworkPlayer.userInfo.InMatchUserId };

        if (HorizontalMovement != lastHorizontalMovement)
        {
            states.States.Add(new UNBufUCCAnimatorState { Flag = (int)ParameterDirtyFlags.HorizontalMovement, Value = HorizontalMovement });
            lastHorizontalMovement = HorizontalMovement;
        }
        if (ForwardMovement != lastForwardMovement)
        {
            states.States.Add(new UNBufUCCAnimatorState { Flag = (int)ParameterDirtyFlags.ForwardMovement, Value = ForwardMovement });
            lastForwardMovement = ForwardMovement;
        }
        //if (Pitch != 0)
            if (lastPitch != Pitch)
            {
                states.States.Add(new UNBufUCCAnimatorState { Flag = (int)ParameterDirtyFlags.Pitch, Value = Pitch });
                lastPitch = Pitch;
            }
        if (Yaw != lastYaw)
        {
            states.States.Add(new UNBufUCCAnimatorState { Flag = (int)ParameterDirtyFlags.Yaw, Value = Yaw });
            lastYaw = Yaw;
        }
        if (Speed != lastSpeed)
        {
            states.States.Add(new UNBufUCCAnimatorState { Flag = (int)ParameterDirtyFlags.Speed, Value = Speed });
            lastSpeed = Speed;
        }
        if (Height != lastHeight)
        {
            states.States.Add(new UNBufUCCAnimatorState { Flag = (int)ParameterDirtyFlags.Height, Value = Height });
            lastHeight= Height;
        }
        //if (Moving != 0)
            if (lastMoveing != Moving)
            {
                states.States.Add(new UNBufUCCAnimatorState { Flag = (int)ParameterDirtyFlags.Moving, Value = Moving ? 1 : 0 });
                lastMoveing = Moving;
            }
        //if ((m_DirtyFlag & (short)ParameterDirtyFlags.Aiming) != 0)
            if (lastAmiming != Aiming)
            {
                states.States.Add(new UNBufUCCAnimatorState { Flag = (int)ParameterDirtyFlags.Aiming, Value = Aiming ? 1 : 0 });
                lastAmiming = Aiming;
            }
        if (MovementSetID != lastMovementSetID)
        {
            states.States.Add(new UNBufUCCAnimatorState { Flag = (int)ParameterDirtyFlags.MovementSetID, Value = MovementSetID });
            lastMovementSetID= MovementSetID;
        }
        if (AbilityIndex != lastAbilityIndex)
        {
            states.States.Add(new UNBufUCCAnimatorState { Flag = (int)ParameterDirtyFlags.AbilityIndex, Value = AbilityIndex });
            lastAbilityIndex = AbilityIndex;
        }
        if (AbilityIntData != lastAbilityIntData)
        {
            states.States.Add(new UNBufUCCAnimatorState { Flag = (int)ParameterDirtyFlags.AbilityIntData, Value = AbilityIntData });
            lastAbilityIntData = AbilityIntData;
        }
        if (AbilityFloatData != lastAbilityFloatData)
        {
            states.States.Add(new UNBufUCCAnimatorState { Flag = (int)ParameterDirtyFlags.AbilityFloatData, Value = AbilityFloatData });
            lastAbilityFloatData = AbilityFloatData;
        }
        if (states.States.Count > 0)
        {
            SendUCCState(states);
            //Debug.Log("AAAAAAAA: " + states.ToString());
        }
    }
    public void ReceiveAnimatorData(UNBufUCCAnimatorStates states)
    {
        foreach (UNBufUCCAnimatorState i in states.States)
        {
            switch (i.Flag)
            {
                case (int)ParameterDirtyFlags.HorizontalMovement:
                    m_NetworkHorizontalMovement = i.Value;
                    break;
                case (int)ParameterDirtyFlags.ForwardMovement:
                    m_NetworkForwardMovement = i.Value;
                    break;
                case (int)ParameterDirtyFlags.Pitch:
                    m_NetworkPitch = i.Value;
                    break;
                case (int)ParameterDirtyFlags.Yaw:
                    m_NetworkYaw = i.Value;
                    break;
                case (int)ParameterDirtyFlags.Speed:
                    m_NetworkSpeed = i.Value;
                    break;
                case (int)ParameterDirtyFlags.Height:
                    SetHeightParameter((int)i.Value);
                    break;
                case (int)ParameterDirtyFlags.Moving:
                    SetMovingParameter(i.Value == 0 ? false : true);
                    break;
                case (int)ParameterDirtyFlags.Aiming:
                    SetAimingParameter(i.Value == 0 ? false : true);
                    break;
                case (int)ParameterDirtyFlags.MovementSetID:
                    SetMovementSetIDParameter((int)i.Value);
                    break;
                case (int)ParameterDirtyFlags.AbilityIndex:
                    var abilityIndex = (int)i.Value;
                    if (m_SnappedAbilityIndex == 0 || abilityIndex == m_SnappedAbilityIndex)
                    {
                        SetAbilityIndexParameter(abilityIndex);
                        m_SnappedAbilityIndex = 0;
                    }
                    break;
                case (int)ParameterDirtyFlags.AbilityIntData:
                    SetAbilityIntDataParameter((int)i.Value);
                    break;
                case (int)ParameterDirtyFlags.AbilityFloatData:
                    m_NetworkAbilityFloatData = i.Value;
                    break;
            }
        }
    }
    async void SendUCCState(UNBufUCCAnimatorStates transformBuf)
    {
        if (NakamaNetworkManager.instance.connection.Socket != null && NakamaNetworkManager.instance.connection.Socket.IsConnected && NakamaContentManager.instance.currentMatch != null)
        {
            await NakamaNetworkManager.instance.connection.Socket.SendMatchStateAsync(NakamaContentManager.instance.currentMatch.Id, (long)OptCode.OP_UN_UCCAnimatorState, transformBuf.ToByteArray());
        }
    }
    float lastTime = -1;
    void Update()
    {
        if (isMine)
        {
            if (Time.time - lastTime < intervalRate) return;
            lastTime = Time.time;
            if(NakamaMyNetworkPlayer.instance.transformSynchronizationMethod == TransformSynchronizationMethod.POSITION_VELOCITY_ROTATION)
                SendAnimatorData();
        }
        else
        {
            SetHorizontalMovementParameter(m_NetworkHorizontalMovement, 1);
            SetForwardMovementParameter(m_NetworkForwardMovement, 1);
            SetPitchParameter(m_NetworkPitch, 1);
            SetYawParameter(m_NetworkYaw, 1);
            SetSpeedParameter(m_NetworkSpeed, 1);
            SetAbilityFloatDataParameter(m_NetworkAbilityFloatData, 1);
        }

    }
    public override bool SetHorizontalMovementParameter(float value, float timeScale, float dampingTime)
    {
        // The animator may not be enabled. Return silently.
        if (!m_Animator.isActiveAndEnabled)
        {
            Debug.LogError("A123");
            return false;
        }
        if (base.SetHorizontalMovementParameter(value, timeScale, dampingTime))
        {
            m_DirtyFlag |= (short)ParameterDirtyFlags.HorizontalMovement;
            return true;
        }
        return false;
    }

    /// <summary>
    /// Sets the Forward Movement parameter to the specified value.
    /// </summary>
    /// <param name="value">The new value.</param>
    /// <param name="timeScale">The time scale of the character.</param>
    /// <param name="dampingTime">The time allowed for the parameter to reach the value.</param>
    /// <returns>True if the parameter was changed.</returns>
    public override bool SetForwardMovementParameter(float value, float timeScale, float dampingTime)
    {
        // The animator may not be enabled. Return silently.
        if (!m_Animator.isActiveAndEnabled)
        {
            return false;
        }
        if (base.SetForwardMovementParameter(value, timeScale, dampingTime))
        {
            m_DirtyFlag |= (short)ParameterDirtyFlags.ForwardMovement;
            return true;
        }
        return false;
    }

    /// <summary>
    /// Sets the Pitch parameter to the specified value.
    /// </summary>
    /// <param name="value">The new value.</param>
    /// <param name="timeScale">The time scale of the character.</param>
    /// <param name="dampingTime">The time allowed for the parameter to reach the value.</param>
    /// <returns>True if the parameter was changed.</returns>
    public override bool SetPitchParameter(float value, float timeScale, float dampingTime)
    {
        // The animator may not be enabled. Return silently.
        if (!m_Animator.isActiveAndEnabled)
        {
            return false;
        }
        if (base.SetPitchParameter(value, timeScale, dampingTime))
        {
            m_DirtyFlag |= (short)ParameterDirtyFlags.Pitch;
            return true;
        }
        return false;
    }

    /// <summary>
    /// Sets the Yaw parameter to the specified value.
    /// </summary>
    /// <param name="value">The new value.</param>
    /// <param name="timeScale">The time scale of the character.</param>
    /// <param name="dampingTime">The time allowed for the parameter to reach the value.</param>
    /// <returns>True if the parameter was changed.</returns>
    public override bool SetYawParameter(float value, float timeScale, float dampingTime)
    {
        // The animator may not be enabled. Return silently.
        if (!m_Animator.isActiveAndEnabled)
        {
            return false;
        }
        if (base.SetYawParameter(value, timeScale, dampingTime))
        {
            m_DirtyFlag |= (short)ParameterDirtyFlags.Yaw;
            return true;
        }
        return false;
    }

    /// <summary>
    /// Sets the Speed parameter to the specified value.
    /// </summary>
    /// <param name="value">The new value.</param>
    /// <param name="timeScale">The time scale of the character.</param>
    /// <param name="dampingTime">The time allowed for the parameter to reach the value.</param>
    /// <returns>True if the parameter was changed.</returns>
    public override bool SetSpeedParameter(float value, float timeScale, float dampingTime)
    {
        // The animator may not be enabled. Return silently.
        if (!m_Animator.isActiveAndEnabled)
        {
            return false;
        }
        if (base.SetSpeedParameter(value, timeScale, dampingTime))
        {
            m_DirtyFlag |= (short)ParameterDirtyFlags.Speed;
            return true;
        }
        return false;
    }

    /// <summary>
    /// Sets the Height parameter to the specified value.
    /// </summary>
    /// <param name="value">The new value.</param>
    /// <returns>True if the parameter was changed.</returns>
    public override bool SetHeightParameter(int value)
    {
        // The animator may not be enabled. Return silently.
        if (!m_Animator.isActiveAndEnabled)
        {
            return false;
        }
        if (base.SetHeightParameter(value))
        {
            m_DirtyFlag |= (short)ParameterDirtyFlags.Height;
            return true;
        }
        return false;
    }

    /// <summary>
    /// Sets the Moving parameter to the specified value.
    /// </summary>
    /// <param name="value">The new value.</param>
    /// <returns>True if the parameter was changed.</returns>
    public override bool SetMovingParameter(bool value)
    {
        // The animator may not be enabled. Return silently.
        if (!m_Animator.isActiveAndEnabled)
        {
            return false;
        }
        if (base.SetMovingParameter(value))
        {
            m_DirtyFlag |= (short)ParameterDirtyFlags.Moving;
            return true;
        }
        return false;
    }

    /// <summary>
    /// Sets the Aiming parameter to the specified value.
    /// </summary>
    /// <param name="value">The new value.</param>
    /// <returns>True if the parameter was changed.</returns>
    public override bool SetAimingParameter(bool value)
    {
        // The animator may not be enabled. Return silently.
        if (!m_Animator.isActiveAndEnabled)
        {
            return false;
        }
        if (base.SetAimingParameter(value))
        {
            m_DirtyFlag |= (short)ParameterDirtyFlags.Aiming;
            return true;
        }
        return false;
    }

    /// <summary>
    /// Sets the Movement Set ID parameter to the specified value.
    /// </summary>
    /// <param name="value">The new value.</param>
    /// <returns>True if the parameter was changed.</returns>
    public override bool SetMovementSetIDParameter(int value)
    {
        // The animator may not be enabled. Return silently.
        if (!m_Animator.isActiveAndEnabled)
        {
            return false;
        }
        if (base.SetMovementSetIDParameter(value))
        {
            m_DirtyFlag |= (short)ParameterDirtyFlags.MovementSetID;
            return true;
        }
        return false;
    }

    /// <summary>
    /// Sets the Ability Index parameter to the specified value.
    /// </summary>
    /// <param name="value">The new value.</param>
    /// <returns>True if the parameter was changed.</returns>
    public override bool SetAbilityIndexParameter(int value)
    {
        // The animator may not be enabled. Return silently.
        if (!m_Animator.isActiveAndEnabled)
        {
            return false;
        }
        if (base.SetAbilityIndexParameter(value))
        {
            m_DirtyFlag |= (short)ParameterDirtyFlags.AbilityIndex;
            return true;
        }
        return false;
    }

    /// <summary>
    /// Sets the Int Data parameter to the specified value.
    /// </summary>
    /// <param name="value">The new value.</param>
    /// <returns>True if the parameter was changed.</returns>
    public override bool SetAbilityIntDataParameter(int value)
    {
        // The animator may not be enabled. Return silently.
        if (!m_Animator.isActiveAndEnabled)
        {
            return false;
        }
        if (base.SetAbilityIntDataParameter(value))
        {
            m_DirtyFlag |= (short)ParameterDirtyFlags.AbilityIntData;
            return true;
        }
        return false;
    }

    /// <summary>
    /// Sets the Ability Float parameter to the specified value.
    /// </summary>
    /// <param name="value">The new value.</param>
    /// <param name="timeScale">The time scale of the character.</param>
    /// <param name="dampingTime">The time allowed for the parameter to reach the value.</param>
    /// <returns>True if the parameter was changed.</returns>
    public override bool SetAbilityFloatDataParameter(float value, float timeScale, float dampingTime)
    {
        // The animator may not be enabled. Return silently.
        if (!m_Animator.isActiveAndEnabled)
        {
            return false;
        }
        if (base.SetAbilityFloatDataParameter(value, timeScale, dampingTime))
        {
            m_DirtyFlag |= (short)ParameterDirtyFlags.AbilityFloatData;
            return true;
        }
        return false;
    }

    /// <summary>
    /// Sets the Item ID parameter with the indicated slot to the specified value.
    /// </summary>
    /// <param name="slotID">The slot that the item occupies.</param>
    /// <param name="value">The new value.</param>
    public override bool SetItemIDParameter(int slotID, int value)
    {
        // The animator may not be enabled. Return silently.
        if (!m_Animator.isActiveAndEnabled)
        {
            return false;
        }
        if (base.SetItemIDParameter(slotID, value))
        {
            m_ItemDirtySlot |= (byte)(slotID + 1);
            return true;
        }
        return false;
    }

    /// <summary>
    /// Sets the Primary Item State Index parameter with the indicated slot to the specified value.
    /// </summary>
    /// <param name="slotID">The slot that the item occupies.</param>
    /// <param name="value">The new value.</param>
    /// <returns>True if the parameter was changed.</returns>
    public override bool SetItemStateIndexParameter(int slotID, int value)
    {
        // The animator may not be enabled. Return silently.
        if (!m_Animator.isActiveAndEnabled)
        {
            return false;
        }
        if (base.SetItemStateIndexParameter(slotID, value))
        {
            m_ItemDirtySlot |= (byte)(slotID + 1);
            return true;
        }
        return false;
    }

    /// <summary>
    /// Sets the Item Substate Index parameter with the indicated slot to the specified value.
    /// </summary>
    /// <param name="slotID">The slot that the item occupies.</param>
    /// <param name="value">The new value.</param>
    /// <returns>True if the parameter was changed.</returns>
    public override bool SetItemSubstateIndexParameter(int slotID, int value)
    {
        // The animator may not be enabled. Return silently.
        if (!m_Animator.isActiveAndEnabled)
        {
            return false;
        }
        if (base.SetItemSubstateIndexParameter(slotID, value))
        {
            m_ItemDirtySlot |= (byte)(slotID + 1);
            return true;
        }
        return false;
    }

    private enum ParameterDirtyFlags : short
    {
        HorizontalMovement = 1,     // The Horizontal Movement parameter has changed.
        ForwardMovement = 2,        // The Forward Movement parameter has changed.
        Pitch = 4,                  // The Pitch parameter has changed.
        Yaw = 8,                    // The Yaw parameter has changed.
        Speed = 16,                 // The Speed parameter has changed.
        Height = 32,                // The Height parameter has changed.
        Moving = 64,                // The Moving parameter has changed.
        Aiming = 128,               // The Aiming parameter has changed.
        MovementSetID = 256,        // The Movement Set ID parameter has changed.
        AbilityIndex = 512,         // The Ability Index parameter has changed.
        AbilityIntData = 1024,      // The Ability Int Data parameter has changed.
        AbilityFloatData = 2048     // The Ability Float Data parameter has changed.
    }
}

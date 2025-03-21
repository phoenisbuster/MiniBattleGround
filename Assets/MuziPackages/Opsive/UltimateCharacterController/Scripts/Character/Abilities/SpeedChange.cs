﻿/// ---------------------------------------------
/// Ultimate Character Controller
/// Copyright (c) Opsive. All Rights Reserved.
/// https://www.opsive.com
/// ---------------------------------------------

namespace Opsive.UltimateCharacterController.Character.Abilities
{
    using Opsive.UltimateCharacterController.Utility;
    using UnityEngine;

    /// <summary>
    /// The SpeedChange ability will update the controller's horizontal and forward movement values based on the multiplier. This value will then be used
    /// by the controller and Animator to change the character's speed.
    /// </summary>
    [AllowDuplicateTypes]
    [DefaultInputName("Change Speeds")]
    [DefaultStartType(AbilityStartType.ButtonDownContinuous)]
    [DefaultStopType(AbilityStopType.ButtonUp)]
    public class SpeedChange : Ability
    {
        [Tooltip("The speed multiplier when the ability is active.")]
        [SerializeField] protected float m_SpeedChangeMultiplier = 2;
        [Tooltip("The minimum value the SpeedChangeMultiplier can change the InputVector value to.")]
        [SerializeField] protected float m_MinSpeedChangeValue = -2;
        [Tooltip("The maximum value the SpeedChangeMultiplier can change the InputVector to.")]
        [SerializeField] protected float m_MaxSpeedChangeValue = 2;
        [Tooltip("Specifies the value to set the Speed Animator parameter to.")]
        [SerializeField] protected float m_SpeedParameter = 2;
        [Tooltip("Does the ability require movement in order to stay active?")]
        [SerializeField] protected bool m_RequireMovement = true;
        [Tooltip("The duration for which player have been hold-shift-running continously.")]
        [SerializeField] public float runDuration = 0;
        [Tooltip("The time that player need to run to activate auto-run.")]
        [SerializeField] protected float autoRunTime = 2;
        public bool isAutoRun;

        public float SpeedChangeMultiplier { get { return m_SpeedChangeMultiplier; } set { m_SpeedChangeMultiplier = value; } }
        public float MinSpeedChangeValue { get { return m_MinSpeedChangeValue; } set { m_MinSpeedChangeValue = value; } }
        public float MaxSpeedChangeValue { get { return m_MaxSpeedChangeValue; } set { m_MaxSpeedChangeValue = value; } }
        public float SpeedParameter { get { return m_SpeedParameter; } set { m_SpeedParameter = value; } }
        public bool RequireMovement { get { return m_RequireMovement; } set { m_RequireMovement = value; } }

        public override bool IsConcurrent { get { return true; } }

        private HeightChange m_heightChangeAbility;

        public override void Awake()
        {
            base.Awake();

            m_heightChangeAbility = m_CharacterLocomotion.GetAbility<HeightChange>();
        }

        /// <summary>
        /// Called when the ablity is tried to be started. If false is returned then the ability will not be started.
        /// </summary>
        /// <returns>True if the ability can be started.</returns>
        public override bool CanStartAbility()
        {
            // An attribute may prevent the ability from starting.
            if (!base.CanStartAbility()) {
                return false;
            }

            m_CharacterLocomotion.TryStopAbility(m_heightChangeAbility); // stop crouch/height change when run

            return !m_RequireMovement || m_CharacterLocomotion.Moving;
        }

        /// <summary>
        /// Should the input be checked to ensure button up is using the correct value?
        /// </summary>
        /// <returns>True if the input should be checked.</returns>
        protected override bool ShouldCheckInput() { return false; }

        /// <summary>
        /// The ability has started.
        /// </summary>
        protected override void AbilityStarted()
        {
            base.AbilityStarted();

            if (m_SpeedParameter != -1) {
                SetSpeedParameter(m_SpeedParameter);
            }
        }

        /// <summary>
        /// Updates the ability. Applies a multiplier to the horizontal and forward movement values.
        /// </summary>
        public override void Update()
        {
            base.Update();

            // autorun related
            if (Input.GetButton("Change Speeds"))
            {
                if (runDuration <= autoRunTime)
                {
                    runDuration += Time.deltaTime;
                    if (runDuration >= autoRunTime)
                        isAutoRun = true;
                }
            }
            else
            {
                if (!isAutoRun)
                    runDuration = 0;
            }
            //

            // If RequireMovement is true then the character must be moving in order for the ability to be active.
            if (m_RequireMovement && !m_CharacterLocomotion.Moving) {
                StopAbility(true);
                return;
            }

            var inputVector = m_CharacterLocomotion.InputVector;
            inputVector.x = Mathf.Clamp(inputVector.x * m_SpeedChangeMultiplier, m_MinSpeedChangeValue, m_MaxSpeedChangeValue);
            inputVector.y = Mathf.Clamp(inputVector.y * m_SpeedChangeMultiplier, m_MinSpeedChangeValue, m_MaxSpeedChangeValue);
            m_CharacterLocomotion.InputVector = inputVector;

            // The raw input vector should be updated as well. This allows other abilities to know if the character has a different speed.
            inputVector = m_CharacterLocomotion.RawInputVector;
            inputVector.x = Mathf.Clamp(inputVector.x * m_SpeedChangeMultiplier, m_MinSpeedChangeValue, m_MaxSpeedChangeValue);
            inputVector.y = Mathf.Clamp(inputVector.y * m_SpeedChangeMultiplier, m_MinSpeedChangeValue, m_MaxSpeedChangeValue);
            m_CharacterLocomotion.RawInputVector = inputVector;
        }

        /// <summary>
        /// The ability has stopped running.
        /// </summary>
        /// <param name="force">Was the ability force stopped?</param>
        protected override void AbilityStopped(bool force)
        {
            base.AbilityStopped(force);
            
            SetSpeedParameter(0);

            // autorun related
            runDuration = 0;
            isAutoRun = false;
            //
        }
    }
}
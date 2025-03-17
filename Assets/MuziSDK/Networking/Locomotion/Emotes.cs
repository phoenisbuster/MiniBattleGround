using System.Collections;
using System.Collections.Generic;
using Opsive.UltimateCharacterController.Character.Abilities;
using UnityEngine;

/// <summary>
/// Ability to do some special animations
/// </summary>
public class Emotes : Ability
{
    public float FloatData;
    public override float AbilityFloatData {
        get
        {
            stopped = false;
            return FloatData;
        }
    }

    private bool stopped = false;
    public override void Update()
    {
        if (m_CharacterLocomotion.Moving)
        {
            if (!stopped)
            {
                stopped = StopAbility(true);
            }
        }
    }
}

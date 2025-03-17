using Opsive.UltimateCharacterController.Character.Abilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dancing : Ability
{
    public override float AbilityFloatData
    {
        get
        {
            if (NetworkObject != null)
            {
                return NetworkObject.abilityData;
            }
            else return UnityEngine.Random.Range(0f, 4f);
        }
    } 

    public Networking.NakamaNetworkPlayer NetworkObject
    {
        get
        {
            if (networkObject == null)
            {
                networkObject = GetComponent<Networking.NakamaNetworkPlayer>();
            }
            return networkObject;
        }
    }

    private Networking.NakamaNetworkPlayer networkObject;

    public override void Update()
    {
        if (m_CharacterLocomotion.Moving)
        {
            StopAbility(true);
            return;
        }
    }
}

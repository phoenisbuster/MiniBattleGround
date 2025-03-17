using Networking;
using Opsive.UltimateCharacterController.Character;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToanTestNetworkAbility : MonoBehaviour
{
    public NakamaNetworkPlayer networkPlayer;
    UltimateCharacterLocomotion characterLocomotion;
    void Start()
    {
        characterLocomotion = GetComponent<UltimateCharacterLocomotion>();
        networkPlayer = GetComponent<NakamaNetworkPlayer>();
    }

    // Update is called once per frame
    void Update()
    {
#if UNITY_EDITOR
        if(Input.GetKeyUp(KeyCode.Y))
        {
            int index = 5;
            bool r = characterLocomotion.TryStartAbility(characterLocomotion.Abilities[index + 1]);
            Debug.Log("TryStartAbility:" + characterLocomotion.Abilities[index + 1] + " " + characterLocomotion.Abilities[index + 1].Index + " ");
        }

        if (Input.GetKeyUp(KeyCode.U))  // play drums
        {
            int index = 7;
            bool r = characterLocomotion.TryStartAbility(characterLocomotion.Abilities[index + 1]);
            StartCoroutine(AbilityStatesProcess(characterLocomotion.Abilities[index + 1].State));
        }

        if (Input.GetKeyUp(KeyCode.I))  // play piano
        {
            int index = 8;
            bool r = characterLocomotion.TryStartAbility(characterLocomotion.Abilities[index + 1]);
            StartCoroutine(AbilityStatesProcess(characterLocomotion.Abilities[index + 1].State));
        }

        if (Input.GetKeyUp(KeyCode.O))  // just sitting
        {
            int index = 9;
            bool r = characterLocomotion.TryStartAbility(characterLocomotion.Abilities[index + 1]);
            StartCoroutine(AbilityStatesProcess(characterLocomotion.Abilities[index + 1].State));
        }
#endif
    }

    IEnumerator AbilityStatesProcess(string state)
    {
        yield return new WaitForSeconds(1.5f);
        switch (state)
        {
            case "SitToPlayDrums":
                networkPlayer.PlayDrums();
                break;
            case "SitToPlayPiano":
                networkPlayer.PlayPiano();
                break;
            case "JustSitting":
                networkPlayer.PlaySitting();
                break;
        }
    }
}

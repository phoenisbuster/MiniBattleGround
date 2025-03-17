using Opsive.UltimateCharacterController.Character;
using Opsive.UltimateCharacterController.Character.Abilities;
using Opsive.UltimateCharacterController.Game;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Networking {
    public class ToanTestNetworkMyPlayer : MonoBehaviour
    {
        // Start is called before the first frame update
        //NetworkObject myNetworkObject;
        UltimateCharacterLocomotion m_CharacterLocomotion;
        Ability[] abilities;
        void Start()
        {
            //Debug.Log("a123");
            m_CharacterLocomotion = GetComponent<UltimateCharacterLocomotion>();
            abilities = m_CharacterLocomotion.GetAbilities<Ability>();

            StartCoroutine(AutoMove());
        }
        public bool ismove = false;
        IEnumerator AutoMove()
        {
            yield return new WaitForSeconds(Random.Range(0,1.0f));
            while (true)
            {
                yield return new WaitForSeconds(1);
                int r = Random.Range(0, 10);
                
                if ( r <=2)
                {
                    ismove = true;
                    //((MoveTowards)abilities[2]).
                    Vector3 targetPos = new Vector3(transform.position.x + Random.Range(-10, 10), 0, transform.position.z + Random.Range(-10, 10));
                    m_CharacterLocomotion.MoveTowardsAbility.MoveTowardsLocation(targetPos);
                    m_CharacterLocomotion.TryStartAbility(abilities[2], true, true);
                }
                else if(r==3)
                {
                    m_CharacterLocomotion.TryStartAbility(abilities[0]);
                }
                else if (r == 4)
                {
                    ismove = false;
                    // m_CharacterLocomotion.TryStartAbility(abilities[4]);
                }
            }
        }
        // Update is called once per frame
        void Update()
        {
            if (ismove)
            {
                

                // KinematicObjectManager.SetCharacterMovementInput(m_CharacterLocomotion.KinematicObjectIndex, 0, 1);
                // KinematicObjectManager.SetCharacterDeltaYawRotation(m_CharacterLocomotion.KinematicObjectIndex, 1);
            }
        }
       
    }
}
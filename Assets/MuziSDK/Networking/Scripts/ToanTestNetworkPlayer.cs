using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Networking {
    public class ToanTestNetworkPlayer : MonoBehaviour
    {
        // Start is called before the first frame update
        NakamaNetworkPlayer myNetworkObject;
        void Start()
        {
            myNetworkObject = GetComponent<NakamaNetworkPlayer>();
            myNetworkObject.isFakeSubCharacter = true;
            StartCoroutine(AutoMove());
        }
        IEnumerator AutoMove()
        {
            yield return new WaitForSeconds(Random.Range(0,1.0f));
            while (true)
            {
                yield return new WaitForSeconds(1);
                int r = Random.Range(0, 10);
                if ( r <=1)
                {
                    myNetworkObject.SetMovePosition(new Vector3(transform.position.x + Random.Range(-10, 10), 0, transform.position.z + Random.Range(-10, 10)));
                }
                else if(r==2)
                {
                    myNetworkObject.SetStateMachineCharacter((ushort)eStateMachineCharacter.Jump, true, 23);
                }
            }
        }
        // Update is called once per frame
        void Update()
        {

        }

        void aaa()
        {
            Debug.Log("ccc");
        }
    }
}
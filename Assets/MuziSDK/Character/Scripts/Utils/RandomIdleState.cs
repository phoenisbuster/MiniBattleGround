using System.Collections;
using UnityEngine;

public class RandomIdleState : MonoBehaviour
{
    [SerializeField]
    Animator currentAnimator;

    [SerializeField]
    float idleDurationMin = 5.0f;
    [SerializeField]
    float idleDurationMax = 10.0f;

    int triggerID = 0;
    float elapsedTime = 0f;
    // Start is called before the first frame update
    IEnumerator Start()
    {
        triggerID = Animator.StringToHash("idle2");
        while(true)
        {
            elapsedTime += Time.deltaTime;

            if (elapsedTime > Random.Range(idleDurationMin, idleDurationMax))
            {
                currentAnimator.SetTrigger(triggerID);
                yield return new WaitForSeconds(13f);// simple hard code the anim idle2 duration
                elapsedTime = 0f;
            }


            yield return new WaitForEndOfFrame();
        }    
    }
}

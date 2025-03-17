using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DestroySection : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Boundary;
    public bool check = false;
    public bool canDes = false;
    public GameObject player;

    public List<GameObject> allChildren;
    public List<GameObject> childrenWithTag;
    void Start()
    {
        FindAllChildren(transform);
        GetChildObjectsWithTag();
    }

    // Update is called once per frame
    void Update()
    {
        OnDestroyPlatform();
        if(check)
        {
            KillTween();
            Destroy(gameObject);
        }
        if(player.GetComponent<PlayerMovement>().isGameOver || player.GetComponent<PlayerMovement>().isReset)
        {
            KillTween();
        }
        // if(canDes)
        // {
        //     Destroy(gameObject);
        // }
        // else
        // {
        //     StartCoroutine(DestroyPlatform());
        // }
    }

    void OnDestroyPlatform() 
    {
        StartCoroutine(DestroyPlatform());
    }

    IEnumerator DestroyPlatform()
    {
        yield return new WaitForSeconds(5);
        check = Boundary.GetComponent<TouchPlayer>().isTouch;
        
    }

    public void FindAllChildren(Transform transform)
    {
        int len = transform.childCount;
 
        for (int i = 0; i < len; i++)
        {
            allChildren.Add(transform.GetChild(i).gameObject);
 
            if (transform.GetChild(i).childCount > 0)
                FindAllChildren(transform.GetChild(i).transform);
        }
    }
 
    public void GetChildObjectsWithTag()
    {
        foreach (GameObject child in allChildren)
        {
            if (child.tag == "SpecialMoney" || child.tag == "BigSlower" || child.tag == "Health")
                childrenWithTag.Add(child);
        }
    }

    public void KillTween()
    {
        //Debug.Log(childrenWithTag);
        foreach (GameObject child in childrenWithTag)
        {
            if(child != null)
            {
                if(child.tag == "BigSlower")
                {
                    child.GetComponent<Slower>().mySequence.Kill();
                }
                else
                {
                    DOTween.Kill(child.transform);
                }                
            }                
        }
        //DOTween.KillAll();
    }
}

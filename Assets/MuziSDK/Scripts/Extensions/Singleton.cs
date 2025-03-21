using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance;
    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<T>();
                if (instance == null)
                {
                    GameObject obj = new GameObject();
                    obj.name = typeof(T).Name;
                    instance = obj.AddComponent<T>();
                }
            }
            return instance;
        }
    }

    [SerializeField]
    bool canDestroyOnLoad = false;

    public virtual void Awake()
    {
        if (instance == null)
        {
            instance = this as T;
            if (!canDestroyOnLoad)
            {
                DontDestroyOnLoad(this.gameObject);
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    private void Update()
    {
        //Debug.Log(name);
        //MyCustomUpdate();
    }
    //virtual protected void MyCustomUpdate()
    //{

    //}
}

using UnityEngine;
using System.Collections;
/// <summary>
/// This class allows us to start Coroutines from non-Monobehaviour scripts
/// Create a GameObject it will use to launch the coroutine on
/// </summary>
public class CoroutineHandler : MonoBehaviour
{
    static protected CoroutineHandler m_Instance;
    static public CoroutineHandler instance
    {
        get
        {
            if(m_Instance == null)
            {
                GameObject o = new GameObject("CoroutineHandler");
                DontDestroyOnLoad(o);
                m_Instance = o.AddComponent<CoroutineHandler>();
            }
            return m_Instance;
        }
    }
    public void OnDisable()
    {
        if(m_Instance)
            Destroy(m_Instance.gameObject);
    }
    public static Coroutine StartStaticCoroutine(IEnumerator coroutine)
    {
        return instance.StartCoroutine(coroutine);
    }
    
    public static void StopStaticCoroutine(IEnumerator coroutine)
    {
        instance.StopCoroutine(coroutine);
    }
}
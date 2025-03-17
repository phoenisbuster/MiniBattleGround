using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ReturnInGame : MonoBehaviour
{
    public static Action PlayerLeaveMatch;
    // Start is called before the first frame update
    void Start()
    {
    
    }
    public void ConfirmLeaveMatch()
    {
        PlayerLeaveMatch?.Invoke();
        //SceneManager.LoadScene(1);
    }

}

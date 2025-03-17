using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Cinemachine;

public class GameOverPopUp : MonoBehaviour
{
    public Sprite[] Winning;
    public Sprite[] Losing;
    public CinemachineVirtualCamera Cam;
    public GameObject TryAgainBut;
    public GameObject Canvas;
    // Start is called before the first frame update
    void Start()
    {
        Canvas = GameObject.FindGameObjectWithTag("UIElementsIngame");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnEnable()
    {
        NakamaConnect.EndGame += PopUp;
    }
    void OnDisable()
    {
        NakamaConnect.EndGame -= PopUp;
    }

    public void PopUp(bool isWin)
    {
        Cursor.lockState = CursorLockMode.None;
		Cursor.visible = true;
        int i = 0;
        for(; i < 3; i++)
        {
            if(isWin)
            {
                transform.GetChild(i+1).GetComponent<Image>().sprite = Winning[i];
            }
            else
            {
                transform.GetChild(i+1).GetComponent<Image>().sprite = Losing[i];
            }
        }
        transform.DOMoveY(Canvas.transform.position.y, 0.1f).SetEase(Ease.Linear).SetUpdate(true).SetLink(gameObject).OnComplete(()=>
        {
            Cam.gameObject.SetActive(true);
            transform.DOMoveY(Canvas.transform.position.y + 2000, 0.1f).SetEase(Ease.Linear).SetUpdate(true).SetLink(gameObject).OnComplete(()=>
            {             
                TryAgainBut.SetActive(true);
            }).SetDelay(2.5f);
        }).SetDelay(1.5f);
    }
}

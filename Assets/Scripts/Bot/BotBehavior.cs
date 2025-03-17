using System;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.AI;

[RequireComponent (typeof (Rigidbody))]
[RequireComponent (typeof (CapsuleCollider))]
public class BotBehavior : MonoBehaviour
{
    public GameObject finishGate;
    public Transform finishLine;
    //public NavMeshAgent alo;
    public float speed;
	public float norSpeed = 4;
	public float Slowspeed = 2;
	public float Runspeed = 8;
    private bool isPush = false;
	private bool isPull = false;
	private Vector3 direction;
    public static event Action<GameObject> FinishRun;
    // Start is called before the first frame update
    void Start()
    {
        finishGate = GameObject.FindGameObjectWithTag("EndGate");
        finishLine = finishGate.transform.GetChild(1).transform;
        speed = norSpeed;
		//GetComponent<NavMeshAgent>().enabled = true;
        TurnOffMove(true);
    }

    void OnEnable() 
    {
        MapGen.startTime += SetBotDestination;
        NakamaConnect.EndGame += TurnOffMove;
    }

    void OnDisable()
    {
        MapGen.startTime -= SetBotDestination;
        NakamaConnect.EndGame -= TurnOffMove;
    }
    void SetBotDestination()
    {
        GetComponent<NavMeshAgent>().enabled = true;
        GetComponent<NavMeshAgent>().SetDestination(finishLine.position);
    }
    public void TurnOffMove(bool value)
	{
		GetComponent<NavMeshAgent>().enabled = false;
		transform.position = new Vector3(15, 1.5f, 15);
	}

    // Update is called once per frame
    void Update()
    {
        GetComponent<NavMeshAgent>().speed = speed;
        if(isPush)
		{
			transform.Translate(direction * speed*0.75f * Time.deltaTime, Space.World);
		}
		if(isPull)
		{
			transform.Translate(direction * -speed*0.75f * Time.deltaTime, Space.World);
		}       
    }
    public void SpeedUp()
	{
		speed = Runspeed;
	}

	public void Slow()
	{
		speed = Slowspeed;
	}

	public void BackToNorSpeed()
	{
		speed = norSpeed;
	}

	public void Push(bool value, Vector3 direc = default(Vector3))
	{
		isPush = value;
		if(isPush)
			direction = direc;
	}
	public void Pull(bool value, Vector3 direc = default(Vector3))
	{
		isPull = value;
		if(isPull)
			direction = direc;
	}
    public void Winning()
	{
		//Bot Win, do st...
		Debug.Log("Bot Win");
		FinishRun?.Invoke(gameObject);
		gameObject.GetComponent<NavMeshAgent>().enabled = false;
		gameObject.SetActive(false);
	}
}

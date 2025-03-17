using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyMovement : MonoBehaviour
{
    public float speed = 3.5f;
    public GameObject player;
    public Animator Anim;
    public bool isCatch = false;
    public AudioSource music;
    public AudioSource chaseSound;
    public AudioSource biteSound;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 enemyPos = transform.position;
        Vector3 playerPos = player.transform.position;
        float dist = playerPos.z - enemyPos.z;
        //chaseSound.UnPause();
        if(dist <= 1 && !player.GetComponent<PlayerMovement>().isDead)
        {
           player.GetComponent<PlayerMovement>().isStop = true;
           //player.GetComponent<PlayerMovement>().Anim.SetBool("isEaten", true);
           player.GetComponent<PlayerMovement>().HealthPoint = 0;
           isCatch = true;
           //chaseSound.Stop();
           //biteSound.Play();
           StartCoroutine(waiter());
        }
        else
        {
            if(dist >= 20)
                speed = 10f;
            else
                speed = 3.5f;
            if(!player.GetComponent<PlayerMovement>().isDead && Time.timeScale != 0)
            {
                music.UnPause();
                chaseSound.UnPause();
                transform.Translate(Vector3.forward * Time.deltaTime * speed, Space.World);
            }
            else
            {
                music.Pause();
                chaseSound.Pause();
            }            
        }
    }

    IEnumerator waiter()
    {  
        chaseSound.Stop();
        biteSound.Play();
        yield return new WaitForSeconds(2);
        biteSound.Stop();
        music.Stop();
        // Debug.Log("No");
        // SceneManager.LoadScene("Menu");
        //player.GetComponent<PlayerMovement>().HealthPoint = 0f;
    }
}

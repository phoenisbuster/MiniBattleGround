using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
public class PlayerMovement : MonoBehaviour
{
    public float speed = 3;
    public float accelerate = 0.5f;
    public float turnSpeed = 4;
    public float jumpSpeed = 5;
    public Rigidbody rb;
    public int jumpCount = 2;
    public bool isGrounded = true;
    public bool isStop = false;
    public bool isDead = false;
    public int MoneyCount = 0;
    private Vector3 oldPos;
    public float disTravel = 0;
    public AudioSource moneyCollect;
    public AudioSource music;
    public Animator Anim;
    private float distoGround;
    public AudioSource HealthPickUp;
    public AudioSource DeathSound;
    public AudioSource FootStep;

    public float HealthPoint = 100f;
    public bool isGameOver = false;
    public bool isReset = false;
    // Start is called before the first frame update
    void Start()
    {
        oldPos = transform.position;
        GameObject child = transform.GetChild(1).gameObject;
        Anim = child.GetComponent<Animator>();
    }

    IEnumerator waitToStandUp()
    {  
        isStop = true;
        Anim.SetBool("StandUp", false);
        yield return new WaitForSeconds(1f);
        //SceneManager.LoadScene("Menu");
        Anim.SetBool("StandUp", true);
        yield return new WaitForSeconds(1f);
        isStop = false;
    }

    void OnCollisionEnter(Collision theCollision)
    {
        if(theCollision.gameObject.tag == "Ground")
        {
            isGrounded = true;
            jumpCount = 2;
            Anim.SetBool("isJump", false);
            Anim.SetBool("isDoublejump", false);    
            FootStep.UnPause();        
        }
        if(theCollision.gameObject.tag == "Obstacle")
        {
            Anim.SetTrigger("Collided");
            transform.Translate(Vector3.forward * -25 * Time.deltaTime * speed, Space.World);
            HealthPoint -= 10;
            speed = 3;
            StartCoroutine(waitToStandUp());          
        }
        if(theCollision.gameObject.tag == "Slower" || theCollision.gameObject.tag == "BigSlower")
        {
            isGrounded = true;
            jumpCount = 2;
            Anim.SetBool("isJump", false);
            Anim.SetBool("isDoublejump", false);
            HealthPoint -= theCollision.gameObject.tag == "BigSlower"? 25: 0;
        }
        if(theCollision.gameObject.tag == "Money" || theCollision.gameObject.tag == "SpecialMoney")
        {
            moneyCollect.Play();
            MoneyCount += theCollision.gameObject.tag == "Money"? 1 : 2;
            accelerate += 0.1f;
            turnSpeed += 0.2f;
            //HealthPoint += HealthPoint+5 <= 100? 5 : 100-HealthPoint;
        }
    }
 
    //consider when character is jumping .. it will exit collision.
    void OnCollisionExit(Collision theCollision)
    {
        if(theCollision.gameObject.tag == "Ground" || theCollision.gameObject.tag == "Slower")
        {
            isGrounded = false;
            FootStep.Pause();
        }
    }

    void OnCollisionStay(Collision theCollision) 
    {
        if(theCollision.gameObject.tag == "Ground")
        {
            isGrounded = true;         
        }
        if(theCollision.gameObject.tag == "Slower" || theCollision.gameObject.tag == "BigSlower")
        {
            isGrounded = true;
            speed = 2;
            accelerate = 0.5f;
        }
    }
    void OnTriggerEnter(Collider theCollision) 
    {
        if(theCollision.gameObject.tag == "BigSlower")
        {
            //isGrounded = true;
            //jumpCount = 2;
            //Anim.SetBool("isJump", false);
            //Anim.SetBool("isDoublejump", false);
            HealthPoint -= theCollision.gameObject.tag == "BigSlower"? 25: 0;
            speed = 2;
            accelerate = 0.5f;
        }
        if(theCollision.gameObject.tag == "Health")
        {
            HealthPickUp.Play();
            HealthPoint += HealthPoint+10 <= 100? 10 : 100-HealthPoint;
        }
    }

    void OnTriggerStay(Collider theCollision) 
    {
        if(theCollision.gameObject.tag == "BigSlower")
        {
            //isGrounded = true;
            speed = 2;
            accelerate = 0.5f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(isStop == false && HealthPoint > 0 && Time.timeScale != 0)
        {
            if(speed > 6) speed = 6;
            if(accelerate > 1) accelerate = 1;
            if(turnSpeed > 7) turnSpeed = 7;
            speed += Time.deltaTime * accelerate;

            transform.Translate(Vector3.forward * Time.deltaTime * speed, Space.World);
            if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {               
                if(this.gameObject.transform.position.x > LevelBoundary.leftSide)
                {
                    if(isGrounded == false) speed = 3;
                    transform.Translate(Vector3.left * Time.deltaTime * turnSpeed, Space.World);
                }
                accelerate = 0.5f;         
            }
            
            if(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                if(this.gameObject.transform.position.x < LevelBoundary.rightSide)
                {
                    if(isGrounded == false) speed = 3;
                    transform.Translate(Vector3.right * Time.deltaTime * turnSpeed, Space.World);
                }
                accelerate = 0.5f;        
            }
        
            if(Input.GetKeyDown(KeyCode.Space) && isGrounded == true)
            {
                Anim.SetTrigger("Jump");
                rb.AddForce(Vector3.up * jumpSpeed, ForceMode.VelocityChange);
                jumpCount--;
                Anim.SetBool("isJump", true);
                FootStep.Pause();
                //isGrounded = false;                
            }
            if(Input.GetKeyDown(KeyCode.Space) && isGrounded == false && jumpCount > 0)
            {
                Anim.SetTrigger("DoubleJump");
                rb.AddForce(Vector3.up * jumpSpeed, ForceMode.Impulse);
                jumpCount--;
                speed = 3;
                //isGrounded = false;
                Anim.SetBool("isDoublejump", true);
                FootStep.Pause();                            
            }
            float distanceThisFrame = (transform.position - oldPos).magnitude;
            disTravel += distanceThisFrame;
            oldPos = transform.position;
        }
        else if(HealthPoint <= 0 && !isDead)
        {
            isStop = true;
            isGameOver = true;
            isDead = true;
            Anim.SetBool("isEaten", true);
            //Time.timeScale = 0;
            StartCoroutine(GameOver());
        }
        else if(Time.timeScale == 0)
        {
            FootStep.Pause();
        }    
    }
    IEnumerator GameOver()
    {  
        FootStep.Stop();
        DeathSound.Play();
        yield return new WaitForSeconds(2);
        //DeathSound.Stop();
        music.Stop();     
        //Debug.Log("Yes");
        //DOTween.KillAll();
        //SceneManager.LoadScene("Menu");
    }
}

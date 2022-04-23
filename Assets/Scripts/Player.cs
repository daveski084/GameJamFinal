using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;
using UnityEngine.SceneManagement;


public class Player : MonoBehaviour
{
    public static int currPlayerHeath;
    public static bool isInSafeZone = false;
    public static bool hasPower, hasWisdom, hasCourage = false;
       
    [SerializeField]
    private float playerSpeed = 2.0f;
    private float playerBodyTemp;
    //private float maxPlayerBodyTemp = 10f;

    private PlayerInput playerInput;
    private bool groundedPlayer;
    private Rigidbody rb;
    private Animator animator;
    private SoundMan sounds;

    public TempBar tempBar;

    Vector2 inputVector = Vector2.zero;
    Vector3 moveDirection = Vector3.zero;


    private void Start()
    {
        playerBodyTemp = 0f;
        animator = GetComponent<Animator>(); 
        playerInput = GetComponent<PlayerInput>();
        rb = GetComponent<Rigidbody>();
        sounds = GetComponent<SoundMan>(); 
        //moveAction = playerInput.actions["Move"]; 
    }
    public void OnMove(InputValue value)
    {
        inputVector = value.Get<Vector2>();
    }

    private void Update()
    {
        Vector3 move = new Vector3(inputVector.x, 0, inputVector.y);

        if (playerBodyTemp >= 10)
        {
            move = Vector3.zero;
        }

        //If check to get rid of that annoying warning that pops up for having no input. 
        if (move != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(move);
        }
        
        transform.Translate(move * playerSpeed * Time.deltaTime, Space.World);

        var velocity = (move * playerSpeed);
        animator.SetFloat("Speed", velocity.magnitude);

        if(!isInSafeZone)
        {
            BRR();
        }

        if(isInSafeZone)
        {
            WarmingUpByTheFire();
        }

        if(playerBodyTemp >= 10)
        {
            animator.SetBool("isDead", true);
            StartCoroutine(Death()); 
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if(collision.gameObject.tag == "Safe Zone")
        {
            isInSafeZone = true;
            animator.SetBool("isSafe", true);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if(collision.gameObject.tag == "Safe Zone")
        {
            isInSafeZone = false;
            animator.SetBool("isSafe", false);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Safe Zone")
        {
            sounds.PlaySafeSound(); 
        }
        if(collision.gameObject.tag == "Bottle")
        {
            sounds.PlayBottleSound();
            hasCourage = true;
        }
        else if (collision.gameObject.tag == "bottle1")
        {
            sounds.PlayBottleSound();
            hasWisdom = true;
        }
        else if (collision.gameObject.tag == "bottle2")
        {
            sounds.PlayBottleSound();
            hasPower = true; 
        }

        if(collision.gameObject.tag == "Barrier")
        {
            if(hasCourage && hasPower && hasPower)
            {
                sounds.PlayBarrierSound(); 
            }
        }

        if(collision.gameObject.tag == "Enemy")
        {
            sounds.PlayChaseMusic();

            playerBodyTemp += 3; 
            if(GuardController.chase == false)
            {
                sounds.StopChaseMusic(); 
            }
        }

        if(collision.gameObject.tag == "DeathPlane")
        {
            playerBodyTemp = 10; 
        }
    }

    private void BRR()
    {
        playerBodyTemp += Time.deltaTime;
        tempBar.SetPlayerTemp(playerBodyTemp);
    }

    private void WarmingUpByTheFire()
    {
        playerBodyTemp = 0;
        tempBar.SetPlayerTemp(playerBodyTemp); 
    }

    private IEnumerator Death()
    {
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene("Death");
    }

}

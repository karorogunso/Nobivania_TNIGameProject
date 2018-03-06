using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
	public float maxSpeed = 7;
	public float jumpTakeOffSpeed = 7;

	public float maxJump = 300;
	public bool Facing = true;
    public float JumpForce = 100f;

    [SerializeField]
    private bool GodMode = false;
    private bool IsLock; // cannot walk

    //air cannon
    public float AttackCoolDown = 1f;
    private float NextFire;
    
	private Rigidbody2D playerRigidbody;
	private GameObject player;
	private SpriteRenderer spriteRenderer;

	private Animator animator;

    public BoxCollider2D suneoTrigger;
    public SuneoHouse suneoHouse;

    
    private bool m_Grounded;            // Whether or not the player is grounded.
    const float k_GroundedRadius = 0.1f; // Radius of the overlap circle to determine if grounded
    [SerializeField] private LayerMask m_WhatIsGround;                  // A mask determining what is ground to the character

    void Awake(){
		playerRigidbody = GetComponent<Rigidbody2D> ();
		spriteRenderer = GetComponent<SpriteRenderer> (); 
        animator = GetComponent<Animator> ();
        // Setting up references.
        

        
    }
    // Use this for initialization
    void Start () {
		player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void FixedUpdate () {

        m_Grounded = false;

        // The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
        // This can be done using layers instead but Sample Assets will not overwrite your project settings.
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, k_GroundedRadius, m_WhatIsGround);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
                m_Grounded = true;
        }

        // Set the vertical animation
        animator.SetFloat("vSpeed", playerRigidbody.velocity.y);

        // walk


        bool jump = Input.GetButtonDown("Jump");
        if (jump){
            // If the player should jump...
            if (m_Grounded && jump)// && animator.GetBool("Ground"))
            {
                // Add a vertical force to the player.
                m_Grounded = false;
                //animator.SetBool("Ground", false);
                playerRigidbody.AddForce(new Vector2(0f, JumpForce));
            }
		}
        animator.SetBool("Ground", m_Grounded);
        AudioSource audio = new AudioSource(); //Keep Audio in Game Engine Source.
        if(NextFire < Time.time && !IsLock)
        {
            float move = Input.GetAxisRaw("Horizontal");
            animator.SetFloat("Speed", Mathf.Abs(move));
            playerRigidbody.velocity = new Vector2(move * maxSpeed, playerRigidbody.velocity.y);
            //Alex (Character) Flip (2D Flip)
            if (move > 0 && !Facing || (Input.GetButtonDown("Horizontal"))) // for turning right
            {
                spriteRenderer.flipX = Facing;
                Facing = !Facing;
            }
            else if (move < 0 && Facing) // for turning left
            {
                spriteRenderer.flipX = Facing;
                Facing = !Facing;
            }

        }
        

        if (Input.GetButton("Item"))
            OnUseItem();
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.gameObject.name);
        
        if(collision.gameObject.name == "Boat")
        {
            var boatController = collision.gameObject.GetComponent<BoatController>();
            boatController.OnPlayerAttach();
            animator.Play("Idle");
        }

        if(collision.tag == "Enemy")
        {
            OnDamage();
        }
    }


    public void OnUseItem()
    {
        switch (ItemController.Current)
        {
            case ItemType.Empty:
            case ItemType.TestNote:
            case ItemType.Star:
                break;
            case ItemType.BambooCopter:
                playerRigidbody.AddForce(new Vector2(0, JumpForce * 2));
                break;
            case ItemType.AirCannon:
                if(Time.time > NextFire)
                {
                    Fire();
                }
                break;
            case ItemType.RemoteControl:
                break;
            case ItemType.ScalingLight:
                if(Time.time > NextFire)
                {
                    ScaleFire();
                }
                break;
            default:
                break;
        }
        
    }

    private void ScaleFire()
    {
        NextFire = Time.time + AttackCoolDown;
    }

    private void Fire()
    {
        NextFire = Time.time + AttackCoolDown;
        //animator.SetBool("Attack",true);
        animator.Play("Cannon Attack");
        if (!suneoTrigger)
            suneoTrigger = GameObject.Find("SuneoTrigger").GetComponent<BoxCollider2D>();
        Vector3 myPosition = transform.position;
        myPosition.z = suneoTrigger.bounds.min.z;
        suneoTrigger.bounds.Contains(myPosition);
        if (!suneoHouse)
            suneoHouse = GameObject.Find("SuneoObject").GetComponent<SuneoHouse>();
        suneoHouse.OnDamage();

    }
    

    public void OnDamage()
    {
        animator.Play("Hurt");
        Invoke("Reload", 2);
        IsLock = true;
    }
    public void Reload()
    {
        if(!GodMode)
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}

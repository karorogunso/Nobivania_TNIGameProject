using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(Rigidbody2D))]
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

	private SpriteRenderer spriteRenderer;
    private AudioSource audioSource;

	private Animator animator;

    public BoxCollider2D suneoTrigger;
    public SuneoHouse suneoHouse;

    public float NormalGravity = 1f;
    public float FlyGravity = 0.5f;

    public AudioClip JumpSound;
    public AudioClip DeadSound;
    public AudioClip PickupSound;
    public AudioClip ShootSound;


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
		//player = GameObject.FindGameObjectWithTag("Player");
        audioSource = GetComponent<AudioSource>();
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
                audioSource.PlayOneShot(JumpSound);
                // Add a vertical force to the player.
                m_Grounded = false;
                //animator.SetBool("Ground", false);
                playerRigidbody.AddForce(new Vector2(0f, JumpForce));
            }
		}
        animator.SetBool("Ground", m_Grounded);
        
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
        if (Input.GetButtonDown("Item"))
        {
            if (ItemController.Current == ItemType.BambooCopter)
            {
                playerRigidbody.gravityScale = FlyGravity;
            }
        }
        if (Input.GetButtonUp("Item"))
        {
            if (ItemController.Current == ItemType.BambooCopter)
            {
                playerRigidbody.gravityScale = NormalGravity;
            }
        }
	}


    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.gameObject.name);
        
        if(collision.gameObject.name == "Boat")
        {
            if(ItemController.Current == ItemType.RemoteControl)
            {
                var boatController = collision.gameObject.GetComponent<BoatController>();
                boatController.OnPlayerAttach();
                animator.Play("Idle");
            }
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
            case ItemType.BambooCopter:
                // jump trick
                if (m_Grounded)
                {
                    m_Grounded = false;
                    playerRigidbody.AddForce(new Vector2(0, JumpForce));
                }
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
        audioSource.PlayOneShot(ShootSound);

        if (!suneoTrigger)
            suneoTrigger = GameObject.Find("SuneoTrigger").GetComponent<BoxCollider2D>();
        Vector3 myPosition = transform.position;
        if (Facing)
        {
            myPosition.z = suneoTrigger.bounds.min.z;
            suneoTrigger.bounds.Contains(myPosition);
            if (!suneoHouse)
                suneoHouse = GameObject.Find("SuneoObject").GetComponent<SuneoHouse>();
            suneoHouse.OnDamage();
        }

    }
    

    public void OnDamage()
    {
        audioSource.PlayOneShot(DeadSound);
        if (!GodMode)
        {
            IsLock = true;
            animator.SetBool("Hurt", true);
            Invoke("FillBlack",1.5f);
            Invoke("Reload", 4);
            BonusController.Bonus = 0;
            IsLock = true;
            ItemController.Current = ItemType.Empty;
        }
    }

    public void FillBlack()
    {
        var black = GameObject.Find("BlackFill");
        var imBlack = black.GetComponent<Image>();
        imBlack.enabled = true;
        
    }
    public void Reload()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}

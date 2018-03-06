using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class PlayerController : MonoBehaviour
{
	public float maxSpeed = 7;
	public float jumpTakeOffSpeed = 7;

	public float maxJump = 300;
	public bool facing = true;
    public float JumpForce = 100f;

    //air cannon
    public float AttackCoolDown = 1f;
    private float NextFire;

	private Rigidbody2D playerRigidbody;
	private GameObject player;
	private SpriteRenderer spriteRenderer;

	private Animator animator;

    public BoxCollider2D suneoTrigger;

	void Awake(){
		playerRigidbody = GetComponent<Rigidbody2D> ();
		spriteRenderer = GetComponent<SpriteRenderer> (); 
        animator = GetComponent<Animator> ();
	}
	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		// walk
        float move = Input.GetAxisRaw("Horizontal");
        animator.SetFloat("Speed", Mathf.Abs(move));

        playerRigidbody.velocity = new Vector2(move * maxSpeed, playerRigidbody.velocity.y);

        bool jump = Input.GetButtonDown("Jump");
        if (jump){
            playerRigidbody.AddForce(new Vector2(0, JumpForce));
		}
        AudioSource audio = new AudioSource(); //Keep Audio in Game Engine Source.
        

        //Alex (Character) Flip (2D Flip)
        if (move > 0 && !facing || (Input.GetButtonDown("Horizontal"))) // for turning right
        {
            spriteRenderer.flipX = facing;
            facing = !facing;
        }
        else if (move < 0 && facing) // for turning left
        {
            spriteRenderer.flipX = facing;
            facing = !facing;
        }

        if (Input.GetKey("Item"))
            OnUseItem();
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.gameObject.name);
        if(collision.gameObject.name == "Boat")
        {
            var boatController = collision.gameObject.GetComponent<BoatController>();
            boatController.OnPlayerAttach();
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
        if (!suneoTrigger)
            suneoTrigger = GameObject.Find("SuneoTrigger").GetComponent<BoxCollider2D>();
        Vector3 myPosition = transform.position;
        myPosition.z = suneoTrigger.bounds.min.z;
        suneoTrigger.bounds.Contains(myPosition);
    }
}

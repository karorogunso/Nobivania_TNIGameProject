using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour
{
	public float maxSpeed = 7;
	public float jumpTakeOffSpeed = 7;
	public bool facing = true;


	private Rigidbody2D playerRigidbody;
	private GameObject player;
	private SpriteRenderer spriteRenderer;

	private Animator animator;



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

        AudioSource audio = new AudioSource(); //Keep Audio in Game Engine Source.
        
        //Alex (Character) Flip (2D Flip)
        if (move > 0 && !facing || (Input.GetButtonDown("Horizontal"))) // for turning right
        {
            Flip();
        }
        else if (move < 0 && facing) // for turning left
        {
            Flip();
        }
	}

	void Flip() 
    {
        facing = !facing;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }   
}

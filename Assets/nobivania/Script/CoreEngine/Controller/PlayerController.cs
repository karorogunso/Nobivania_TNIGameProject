using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour
{
	public float maxSpeed = 7;
	public float jumpTakeOffSpeed = 7;

	public float maxJump = 300;
	public bool facing = true;
    public float JumpForce = 100f;


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


	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.name == "boat")
        {
            var boatController = collision.gameObject.GetComponent<BoatController>();
            boatController.OnPlayerAttach();
        }
    }

}

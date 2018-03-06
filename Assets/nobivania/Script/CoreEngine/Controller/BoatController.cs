using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
public class BoatController : MonoBehaviour {

    public Vector3 floor;
    public float JumpForce= 100f;
    public float Speed = 1f;
    public float MaxSpeed = 10f;
    public float DecayRate = 0.1f;
    
    public bool IsControl = false;
    public float ExitForce = 450f;
    public Transform PlayerHolder;
    [SerializeField]
    private float Velocity;
    
    private new Rigidbody2D rigidbody;

    public GameObject Player;

    public bool m_Grounded;            
    float k_GroundedRadius = 0.7f;
    [SerializeField] private LayerMask m_WhatIsGround;

    // Use this for initialization
    void Start () {
        floor = transform.position;
        if (!PlayerHolder)
            PlayerHolder = GameObject.Find("PlayerHolder").transform;
        rigidbody = GetComponent<Rigidbody2D>();
        if (!Player)
            Player = GameObject.FindGameObjectWithTag("Player");
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
        if ((IsControl && ItemController.Current == ItemType.RemoteControl))
        {
            bool run = Input.GetButton("Item");
            bool jump = Input.GetButtonDown("Jump");

            Velocity += run ? 1 : 0;
            Velocity = Mathf.Min(Velocity, MaxSpeed);
            Vector2 moveVector = new Vector2(Velocity,0);
            moveVector *= Time.fixedDeltaTime;
            //rigidbody.velocity = new Vector2(run ? 1 : 0, rigidbody.velocity.y);
            if(moveVector.sqrMagnitude > 0.001f) 
                rigidbody.MovePosition(moveVector + new Vector2(transform.position.x, transform.position.y));
            if (m_Grounded && jump)
                rigidbody.AddForce(new Vector2(0, JumpForce));
            Player.transform.position = PlayerHolder.position;
        }
        if (Velocity > 0)
            Velocity -= DecayRate;
       
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.name == "BoatExit")
        {
            // exit form boat
            OnPlayerDeattach();
        }
    }

    public void OnPlayerAttach()
    {
        var controller = Player.GetComponent<PlayerController>();
        controller.enabled = false;
        IsControl = true;
        
    }

    public void OnPlayerDeattach()
    {
        IsControl = false;
        var controller = Player.GetComponent<PlayerController>();
        controller.enabled = true;
        Rigidbody2D rigid2D = Player.GetComponent<Rigidbody2D>();
        rigid2D.AddForce(new Vector2(0,ExitForce));
    }
}

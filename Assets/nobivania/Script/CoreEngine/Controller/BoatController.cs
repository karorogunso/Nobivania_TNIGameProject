﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody2D))]
public class BoatController : MonoBehaviour {

    public Vector3 floor;
    public float JumpForce= 100f;
    public float Speed = 1f;

    //for scene 3
    public Sprite RemoteSprite;

    public bool IsControl = false;
    public Vector2 ExitForce = new Vector2(0,450f);
    public Transform PlayerHolder;
    [SerializeField]
    private float Velocity;
    
    private new Rigidbody2D rigidbody;

    public GameObject Player;
    private Image UI;

    public bool m_Grounded;            
    float k_GroundedRadius = 1f;
    [SerializeField] private LayerMask m_WhatIsGround;

    // Use this for initialization
    void Start () {
        if (SceneManager.GetActiveScene().name == "Act3_Scene1") 
        {
            ItemController.Current = ItemType.RemoteControl;
            if (RemoteSprite)
            {
                UI = GameObject.Find("ItemUI").GetComponent<Image>();
                UI.color = new Color(1, 1, 1, 1);
                UI.sprite = RemoteSprite;
            }
        }
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

        if ((IsControl && ItemController.Current == ItemType.RemoteControl) )
        {

            if (m_Grounded)
            {
                bool run = Input.GetButton("Item");
                bool jump = Input.GetButtonDown("Jump");   
                Vector2 force = new Vector2(0, jump ? JumpForce : 0);

                if(run)
                    rigidbody.velocity = new Vector2(Speed, rigidbody.velocity.y);
                rigidbody.AddForce(force);
            }
            Player.transform.position = PlayerHolder.position;
        }
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
        ItemController.Current = ItemType.Empty;
        IsControl = false;
        var controller = Player.GetComponent<PlayerController>();
        controller.enabled = true;
        Rigidbody2D rigid2D = Player.GetComponent<Rigidbody2D>();
        rigid2D.AddForce(ExitForce);
    }
}

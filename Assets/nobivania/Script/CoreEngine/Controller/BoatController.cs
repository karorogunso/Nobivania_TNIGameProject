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
    public Transform PlayerHolder;
    [SerializeField]
    private float Velocity;
    
    private new Rigidbody2D rigidbody;

    public GameObject Player;
	// Use this for initialization
	void Start () {
        floor = transform.position;
        if (!PlayerHolder)
            PlayerHolder = GameObject.Find("PlayerHolder").transform;
        rigidbody = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
        if((IsControl && ItemController.Current == ItemType.RemoteControl))
        {
            float h = Input.GetAxis("Horizontal");
            bool jump = Input.GetButtonDown("Jump");

            Velocity += h;
            Velocity = Mathf.Clamp(Velocity, -MaxSpeed, MaxSpeed);
            Vector2 moveVector = new Vector2(Velocity,0);
            moveVector *= Time.deltaTime;
            
            if(moveVector.sqrMagnitude > 0.001f) 
                rigidbody.MovePosition(moveVector + new Vector2(transform.position.x, transform.position.y));
            if (jump)
                rigidbody.AddForce(new Vector2(0, JumpForce) );
        }
        Velocity -= DecayRate;
        Velocity = Mathf.Max(Velocity,0);
	}

    void OnPlayerAttach()
    {
        var controller = Player.GetComponent<PlayerController>();
        controller.enabled = false;
        IsControl = true;
    }

    void OnPlayerDeattach()
    {
        IsControl = false;
        var controller = Player.GetComponent<PlayerController>();
        controller.enabled = true;
    }
}

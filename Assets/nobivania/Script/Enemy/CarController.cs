using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class CarController : MonoBehaviour {

    public float MinX = 10f;
    public float MaxX = 20f;

    public float BoostSpeed = 0.1f;
    public float MaxSpeed = 1f;

    public Transform Player;

    private float Velocity = 0f;
    private new Rigidbody2D rigidbody;

	// Use this for initialization
	void Start () {
        if (!Player)
        {
            Player = GameObject.FindGameObjectWithTag("Player").transform;
        }
        rigidbody = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 target = Player.position;
		if(target.x > MinX && target.x < MaxX)
        {
            // move to target
            float inc = target.x < transform.position.x ? -BoostSpeed : BoostSpeed;
            Velocity += inc;
            Velocity = Mathf.Clamp(Velocity, -MaxSpeed, MaxSpeed);

        }
        GetComponent<SpriteRenderer>().flipX = Velocity < 0 ;
        Vector2 moveVecter = new Vector2(Velocity, 0) * Time.deltaTime;
        moveVecter.x += transform.position.x;
        moveVecter.y = transform.position.y;
        rigidbody.MovePosition(moveVecter);
	}
    
    private void OnDrawGizmosSelected()
    {
        Vector3 from = new Vector3(MinX, 0);
        Vector3 to = new Vector3(MinX, 10);
        Gizmos.DrawLine(from, to);
        from.x = to.x = MaxX;
        Gizmos.DrawLine(from, to);
        
    }
}

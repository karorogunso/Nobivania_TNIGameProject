using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Giant_Spawner : MonoBehaviour {

	public GameObject prefab;
	public float projectiledelay;
	public float projectileperiod;
	public float speed;
	public float distanceperstep;
	public float updownspeed;
	public float delay;
	public float period;
	public float periodoffset;
	Rigidbody2D rb;
	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D>();
		InvokeRepeating("LaunchProjectile", projectiledelay, projectileperiod);
		InvokeRepeating("StartWalking", delay, (period*2)+periodoffset);
		InvokeRepeating("StopWalking", delay+period, (period*2)+periodoffset);
		InvokeRepeating("StopWalkingStill", delay+period*2, (period*2)+periodoffset);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void LaunchProjectile()
	{
		CancelInvoke("StartWalking");
		CancelInvoke("StopWalking");
		Instantiate(prefab);
	}
	void StartWalking()
	{
		rb.velocity = transform.up * updownspeed;
	}
	void StopWalking()
	{
		transform.position = new Vector3(transform.position.x + distanceperstep, transform.position.y, transform.position.z);
		rb.velocity = -transform.up * updownspeed;
	}
	void StopWalkingStill()
	{
		rb.velocity = new Vector2(0,0);
	}
	private void OnTriggerEnter2D(Collider2D collision)
    	{
		if(collision.gameObject.name == "Nobita")
		{
			Debug.Log("Insert game over here");
		}
	}
}

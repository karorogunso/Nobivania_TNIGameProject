using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScene3 : MonoBehaviour {

	public float speed;
	public float delay;
	public float enddelay;
	Rigidbody2D rb;
	public float AspectMultiplyer = 1f;
	CameraController othercamcon;
	CameraScene3 thiscamcon;
	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D>();
		othercamcon = GetComponent<CameraController>();
		thiscamcon = GetComponent<CameraScene3>();
		Invoke("walk", delay);
		Invoke("stop", enddelay);
		//Camera.main.orthographicSize =   (1 / Camera.main.aspect) * AspectMultiplyer;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void walk()
	{
		rb.velocity = transform.right * speed;
	}
	void stop()
	{
		rb.velocity = transform.right * 0;
		othercamcon.enabled = true;
		thiscamcon.enabled = false;
		
	}
}

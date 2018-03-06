using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScene3 : MonoBehaviour {

	public float speed;
	public float delay;
	Rigidbody2D rb;
	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D>();
		Invoke("walk", delay);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void walk()
	{
		rb.velocity = transform.right * speed;
	}
}

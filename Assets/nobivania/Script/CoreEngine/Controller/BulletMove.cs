using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMove : MonoBehaviour {

    Animator bulletanim;
    Rigidbody2D rb;
    // Use this for initialization
	void Start () {
        bulletanim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        bulletanim.speed = 0f;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    // Kill bullet once offscreen
    void OnBecameInvisible () {
        //Debug.Log("bullet offscreen");
        Destroybullet();
    }
    void OnCollisionEnter2D()
    {
        //Debug.Log("Collider");
        Destroybullet();
    }
    void Destroybullet()
    {
        bulletanim.speed = 0.5f;
        rb.simulated = false;
        Destroy(gameObject, 1f);
    }
}

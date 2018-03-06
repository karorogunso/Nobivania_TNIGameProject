using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundObject : MonoBehaviour {

    public float speed;
    public float miny;
    public float maxy;
    Rigidbody2D rb;
    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody2D>();
        transform.position = new Vector3(transform.position.x, Random.Range(miny,maxy), 0);
        rb.velocity = transform.right * speed;
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
        //rb.simulated = false;
        Destroy(gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundObject : MonoBehaviour {

    public float speed;
    public float miny;
    public float maxy;
    public float startx;
    Rigidbody2D rb;
    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody2D>();
        transform.position = new Vector3(startx, Random.Range(miny,maxy), 0);
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
    void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log("Collider");
        //Destroybullet();
        //Debug.Log(GetCollisionAngle(collision));
    }
    void Destroybullet()
    {
        //rb.simulated = false;
        Destroy(gameObject);
    }
    /*public float GetCollisionAngle(Transform hitobjectTransform, CircleCollider2D collider, Vector2 contactPoint)
    {
        Vector2 collidertWorldPosition = new Vector2(hitobjectTransform.position.x, hitobjectTransform.position.y);
        Vector3 pointB = contactPoint - collidertWorldPosition;
        float theta = Mathf.Atan2(pointB.x, pointB.y);
        float angle = (360 - ((theta * 180) / Mathf.PI)) % 360;
        return angle;
    }*/

}

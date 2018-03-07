using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuneoHouse : MonoBehaviour {

    public int HP = 3;
    public float BreakTime = 1f;

    public GameObject Explosion;
    public float LoopTime = 0.2f;
    private Animator animator;

    private new Collider2D collider;
    private int CurrentLoop;
    private float StartBreaking;

	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();
        collider = GetComponent<BoxCollider2D>();
	}
	
	// Update is called once per frame
	void Update () {
        animator.SetBool("Hit", false);
	}

    public void OnDamage()
    {
        HP--;
        if(HP <= 0)
        {
            animator.SetBool("Break", true);
            Destroy(collider,BreakTime);
            Destroy(this, BreakTime);
            StartBreaking = Time.time;
            ExplosionEffect();
        }
        else
        {
            //animator.SetBool("Hit", true);
            animator.Play("Hit");
        }
    }

    public void ExplosionEffect()
    {
        Vector3 min = collider.bounds.min;
        Vector3 max = collider.bounds.max;
        Vector3 target = new Vector3(Random.Range(min.x, max.x), Random.Range(min.y, max.y));
        GameObject explore = Instantiate(Explosion, target, Quaternion.identity);
        Destroy(explore, 1f);
        if(Time.time - StartBreaking < BreakTime)
        {
            CurrentLoop++;
            Invoke("ExplosionEffect", LoopTime);
        }
    }
}

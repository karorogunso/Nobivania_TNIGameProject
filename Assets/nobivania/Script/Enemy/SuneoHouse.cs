using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuneoHouse : MonoBehaviour {

    public int HP = 3;
    public float BreakTime = 1f;

    private Animator animator;

    private new Collider2D collider;

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
            Destroy(collider, BreakTime);
        }
        else
        {
            animator.SetBool("Hit", true);
        }
    }
}

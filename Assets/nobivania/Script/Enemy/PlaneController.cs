using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneController : MonoBehaviour {

    public Transform Player;
    public Collider2D TriggerBox;
    private Animator PlaneAnimator;
    public SpriteRenderer Sprite;
    public float LoopTime = 3f;

    private bool IsFlip = false;
    private bool IsAttack = false;


    // Use this for initialization
    void Start () {
        if (!Player)
        {
            Player = GameObject.FindGameObjectWithTag("Player").transform;
        }
        PlaneAnimator = GetComponent<Animator>();
        if(!Sprite)
            Sprite = GetComponent<SpriteRenderer>();
    }
	
	// Update is called once per frame
	void Update () {
        if (!IsAttack)
        {
            Vector3 target = Player.position;
            if (TriggerBox.bounds.Contains(target))
            {
                IsAttack = true;
                transform.position = target;
                if (IsFlip)
                {
                    Sprite.flipX = true;
                    PlaneAnimator.Play("FlyInvert");
                }
                else
                {
                    Sprite.flipX = false;
                    PlaneAnimator.Play("Fly");
                }
                Invoke("Wait", LoopTime);

            }
        }
    }

    void Wait()
    {
        IsAttack = false;
        IsFlip = !IsFlip;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, 0.1f);
    }

    
}

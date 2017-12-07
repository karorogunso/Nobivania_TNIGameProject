/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

    public float Speed = 1f;
    public float JumpForce = 1f;
    public float Gravity = 5f;

    public AudioClip JumpSound;
    public AudioClip DamageSound;
    public AudioClip FinishSound;

    public SpriteRenderer WinLogo;
    public Text restartText;

    public Transform floor;

    private Animator animator;
    private SpriteRenderer sprite;
    private new AudioSource audio;
    private float Velocity;
    private bool IsDead;
    private bool IsWin;

    // Use this for initialization
    void Start () {
        //collider = GetComponent<Collider2D>();
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        audio = GetComponent<AudioSource>();
	}

    // Update is called once per frame
    void Update () {
        float h = Input.GetAxis("Horizontal");
        Vector2 deltamove = Vector2.zero;

        if (IsDead || IsWin)
        {
            h = 0;
        }

        animator.SetFloat("Speed", Mathf.Abs(h));
        if(h != 0)
        {
            h *= Speed;
            if (h < 0)
                sprite.flipX = true;
            else
                sprite.flipX = false;
            h *= Time.deltaTime;
            deltamove.x += h;
        }
        // ground distance
        float groundDistance = transform.position.y - floor.position.y;

        animator.SetFloat("GroundDistance", groundDistance);
        //if (Velocity <= 0)
            //animator.SetFloat("FallSpeed", -Velocity);

        //physics
        Velocity = Velocity - Gravity * Time.deltaTime;

        bool jump = Input.GetButtonDown("Jump");
        if (jump)
        {
            Velocity = JumpForce;
            audio.PlayOneShot(JumpSound);
        } else if (groundDistance <= 0)
        {
            Velocity = 0;
        }
        if (IsDead && groundDistance <= 0)
            Velocity = 0;
        float y = Velocity * Time.deltaTime;
        deltamove.y += y;
        
        Vector3 position = transform.position + new Vector3(deltamove.x,deltamove.y);
        if (groundDistance < 0)
            position.y = floor.position.y;
        transform.position = position;

        // restart
        if (Input.GetKeyUp(KeyCode.Tab))
        {
            SceneManager.LoadScene(Application.loadedLevel);
        }

	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "DamageObject")
        {
            restartText.enabled = true;
            Destroy(collision.gameObject);
            TriggerEnemy();
        }
        else if (collision.tag == "Finish")
        {
            restartText.enabled = true;
            Destroy(collision.gameObject);
            TriggerFinish();
        }
            
    }

    void TriggerEnemy()
    {
        IsDead = true;
        animator.SetBool("Damage", true);
        animator.SetBool("IsDead", true);
        //animator.SetFloat("Speed", 0);
        //animator.SetFloat("GroundDistance", 0);
        //animator.SetFloat("FallSpeed", 0);

        audio.PlayOneShot(DamageSound);
    }
    void TriggerFinish()
    {
        audio.PlayOneShot(FinishSound);
        IsWin = true;
        WinLogo.enabled = true;
    }

}
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DoorController : MonoBehaviour {

    public string NextLevel = "Act1_Scene1";

    public bool IsLock = false;

    public AudioClip OpenSound;
    public AudioClip LockSound;

    private AudioSource audioSource;
	// Use this for initialization
	void Start () {
        audioSource = GetComponent<AudioSource>();
	}
	

    private void OnTriggerEnter2D(Collider2D collision)
    {
	    Debug.Log("Door Trigger Entered");
        if (!IsLock)
        {
            if (OpenSound)
                audioSource.PlayOneShot(OpenSound);
            Animator animator = GetComponent<Animator>();
            animator.SetBool("Open", true);
            Invoke("FillBlack",1.5f);
            Invoke("LoadScene",4f);
        }
        else if (LockSound)
            audioSource.PlayOneShot(LockSound);
    }

    public void FillBlack()
    {
        var black = GameObject.Find("BlackFill");
        var imBlack = black.GetComponent<Image>();
        imBlack.enabled = true;
    }

    public void LoadScene()
    {
        SceneManager.LoadScene(NextLevel);
    }
}

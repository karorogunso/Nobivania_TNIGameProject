using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
	Debug.Log("Door Trigger Entered");
        if (!IsLock)
        {
            if (OpenSound)
                audioSource.PlayOneShot(OpenSound);
            SceneManager.LoadScene(NextLevel);
        }
        else if (LockSound)
            audioSource.PlayOneShot(LockSound);
    }
}

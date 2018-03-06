using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusController : MonoBehaviour {

    public static int Bonus = 0;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            Bonus++;
            var audio = GetComponent<AudioSource>();
            if (audio)
                audio.Play();
            Destroy(this.gameObject);
        }
    }
}

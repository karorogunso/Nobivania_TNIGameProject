using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeshiTrigger : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("trigger :" + collision.tag);
        if (collision.tag == "Player")
        {
            GameObject takeshi = GameObject.Find("Takeshi");
            Takeshi_Spawner spw = takeshi.GetComponent<Takeshi_Spawner>();
            spw.Falling();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMove : MonoBehaviour {

    // Use this for initialization
	void Start () {

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    // Kill bullet once offscreen
    void OnBecameInvisible () {
        //Debug.Log("bullet offscreen");
        Destroy(gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatController : MonoBehaviour {

    public Vector3 floor;
	// Use this for initialization
	void Start () {
        floor = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnPlayerAttach()
    {

    }
}

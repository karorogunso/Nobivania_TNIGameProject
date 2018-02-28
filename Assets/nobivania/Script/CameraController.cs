using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public Transform Player;
    
    public float Smooth = 0.9f;
    private Vector3 FixPoint;
	// Use this for initialization
	void Start () {
        FixPoint = transform.position;
        if(Player == null)
        {
            Player = GameObject.FindGameObjectWithTag("Player").transform;
        }
	}
	
	// Update is called once per frame
	void Update () {
        float x = Player.position.x;
        Vector3 target = transform.position;
        target.x = Mathf.Lerp(transform.position.x,x,Smooth);
        transform.position = target;
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public Transform Player;
    
    public float Smooth = 0.9f;


    public bool IsCutScene = false;

	// Use this for initialization
	void Start () {
        
        if(Player == null)
        {
            Player = GameObject.FindGameObjectWithTag("Player").transform;
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (!IsCutScene)
        {
            float x = Player.position.x;
            Vector3 target = transform.position;
            target.x = Mathf.Lerp(transform.position.x, x, Smooth);
            transform.position = target;
        }
	}
}

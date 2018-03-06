using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Giant_Spawner : MonoBehaviour {

	public GameObject prefab;
	public float delay;
	public float period;
	// Use this for initialization
	void Start () {
		InvokeRepeating("LaunchProjectile", delay, period);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void LaunchProjectile()
	{
		Instantiate(prefab);
	}
}

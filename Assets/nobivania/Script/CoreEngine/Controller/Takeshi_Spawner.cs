using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Takeshi_Spawner : MonoBehaviour {

	public GameObject prefab;
	public float projectiledelay;
	public float projectileperiod;
	public float animdelay;
	public float startx;
	public float giantoffset;
	public float starty;
	public float miny;
	public float maxy;
	Rigidbody2D rb;
	Animator an;
	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D>();
		an = GetComponent<Animator>();
		Invoke("Falling", projectiledelay);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void Falling()
	{
		rb.simulated = true;
		an.SetTrigger("GiantFalls");
		Invoke("LaunchProjectileFirst", animdelay);
		Instantiate(prefab);
	}
	void LaunchFirst()
	{
		Instantiate(prefab, new Vector3(startx, starty, 0), Quaternion.identity);
		an.SetTrigger("GiantDrink");
		InvokeRepeating("LaunchProjectileFirst", animdelay, projectileperiod);
	}
	void LaunchProjectile()
	{
		an.SetTrigger("GiantShout");
		Instantiate(prefab, new Vector3(startx-giantoffset, Random.Range(miny,maxy), 0), Quaternion.identity);
	}
}

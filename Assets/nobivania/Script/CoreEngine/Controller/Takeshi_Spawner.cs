using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Takeshi_Spawner : MonoBehaviour {

	public GameObject prefab;
	public float projectiledelay;
	public float projectileperiod;
	public float animdelay;
	public float startdelay;
	public float startx;
	public float giantoffset;
	public float starty;
	public float miny;
	public float maxy;
	public float animdelay2;
	public float animdelay3;
	Rigidbody2D rb;
	Animator an;
	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D>();
		an = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void Falling()
	{
		rb.simulated = true;
		an.SetTrigger("GiantFalls");
		Invoke("LaunchFirst", animdelay);
	}
	void LaunchFirst()
	{
		rb.simulated = false;
		an.SetTrigger("GiantDrink");
		InvokeRepeating("LaunchProjectile", projectiledelay, projectileperiod);
		Invoke("LaunchFirstItem", animdelay2);
	}
	void LaunchFirstItem()
	{
		Instantiate(prefab, new Vector3(startx, starty, 0), Quaternion.identity);
	}
	void LaunchProjectile()
	{
		an.SetTrigger("GiantShout");
		Invoke("LaunchProjectileItem", animdelay2);
	}
	void LaunchProjectileItem()
	{
		Instantiate(prefab, new Vector3(startx-giantoffset, Random.Range(miny,maxy), 0), Quaternion.identity);
	}
}

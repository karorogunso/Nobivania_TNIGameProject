using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyController : MonoBehaviour {

    public float Width = 10f;
    public float Speed = 1f;
    public float DestroryPoint = -10f;
    private float SpawnTime;

    public Transform SpawnPoint;

    public GameObject SkyObject;

    private List<GameObject> SkyCollection;

	// Use this for initialization
	void Start () {
        SkyCollection = new List<GameObject>();
        SpawnTime = Width / Speed;
        //generate 
        Vector3 position = SpawnPoint.position;
        while(position.x > DestroryPoint)
        {
            SkyCollection.Add(Instantiate(SkyObject, position, Quaternion.identity));
            position.x -= Width;
        }
        InvokeRepeating("CreateOne", SpawnTime, SpawnTime);
	}
	
	// Update is called once per frame
	void Update () {
        SkyCollection.ForEach((g) =>
        {
            Vector3 position = g.transform.position;
            position.x -= Speed * Time.deltaTime;
            g.transform.position = position;
            if(position.x < DestroryPoint)
            {
                Destroy(g);
                SkyCollection.Remove(g);
            }
        });
	}

    void CreateOne()
    {
        Vector3 oldOne = SkyCollection[SkyCollection.Count - 1].transform.position;
        oldOne.x += 10;
        SkyCollection.Add(Instantiate(SkyObject, oldOne, Quaternion.identity));
    }
}

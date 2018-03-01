using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour {

    public float MinX = 10f;
    public float MaxX = 20f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    
    private void OnDrawGizmosSelected()
    {
        Vector3 from = new Vector3(MinX, 0);
        Vector3 to = new Vector3(MinX, 10);
        Gizmos.DrawLine(from, to);
        from.x = to.x = MaxX;
        Gizmos.DrawLine(from, to);
        
    }
}

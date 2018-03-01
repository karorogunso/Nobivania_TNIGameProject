using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirboardController : MonoBehaviour {

    public float Speed = 1f;

    public float MinX;
    public float MaxX;
    private bool IsForward = true;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 position = transform.position;
        float x = Speed * Time.deltaTime;
        if (!IsForward)
            x = -x;
        position.x += x;
        transform.position = position;
        if ((IsForward && position.x > MaxX) || (!IsForward && position.x < MinX))
            IsForward = !IsForward;
            

	}
    private void OnDrawGizmosSelected()
    {
        Vector3 position = transform.position;
        Vector3 from = new Vector3(MinX, position.y,position.z);
        Vector3 to = new Vector3(MaxX, position.y, position.z);
        Gizmos.DrawLine(from, to);
    }
}

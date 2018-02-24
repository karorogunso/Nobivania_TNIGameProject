using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour {

    public ItemType Type;
    public static ItemType Current;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Current = Type;
            Destroy(this.gameObject);
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shizuka : MonoBehaviour {
    private Image UI;

    public GameObject RequestPopup;
    public DoorController Door;
    public AudioClip ThankyouSound;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            if(ItemController.Current == ItemType.TestNote)
            {
                
                OnTestNoteReturn();
                Destroy(RequestPopup);
            }
        }
    }

    private void OnTestNoteReturn()
    {
        ItemController.Current = ItemType.Empty;
        UI = GameObject.Find("ItemUI").GetComponent<Image>();
        UI.color = new Color(1, 1, 1, 0);
        Door.IsLock = false;
        if (ThankyouSound)
        {
            AudioSource source = GetComponent<AudioSource>();
            source.PlayOneShot(ThankyouSound);
        }
        
    }
}

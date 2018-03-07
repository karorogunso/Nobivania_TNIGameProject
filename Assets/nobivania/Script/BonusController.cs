using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusController : MonoBehaviour {

    public static int Bonus = 0;
    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            Bonus++;
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            var PlayerAudioSource = player.GetComponent<AudioSource>();
            var PlayerController = player.GetComponent<PlayerController>();
            PlayerAudioSource.PlayOneShot(PlayerController.PickupSound);
            Destroy(this.gameObject);

            GameObject bonusDisplay = GameObject.Find("BonusDisplay");
            BonusDisplay display = bonusDisplay.GetComponent<BonusDisplay>();
            display.Show();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BonusDisplay : MonoBehaviour {


    public Sprite[] number;

    public void Start()
    {
        Show();
    }

    public void Show()
    {
        Image im = GetComponent<Image>();
        im.sprite = number[BonusController.Bonus];
        
    }
}

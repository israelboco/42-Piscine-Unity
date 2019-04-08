using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUIAmmo : MonoBehaviour {

    public playerScript player;
    public Text weapon;
    public Text ammo;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (player.weaponEquip) {
            weapon.text = player.weapon.weaponName;
            if (player.weapon.melee)
            {
                ammo.text = "\u221E";
            }
            else
            {
                ammo.text = player.weapon.totalBullets.ToString();
            }
        } else {
            weapon.text = "No weapon";
            ammo.text = "-";
        }
	}
}

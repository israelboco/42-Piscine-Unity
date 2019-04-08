using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stat : MonoBehaviour
{


	public float STR;
	public float AGI;
	public float CON;
	public float ARMOR;
	public float HP;
	public float Max_HP;

	public float minDamage;
	public float minDamage_base;
	public float maxDamage;
	public float maxDamage_base;
	public int LV;
	public float XP;
	public float money;

//	public int pt;
//	public float xp_next;


	private void Awake()
	{
		HP = 5 * CON;
		minDamage = STR / 2;
		maxDamage = minDamage + 4;
		Max_HP = HP;
	}


	public void Reload_stat()
	{
//		float pcent = (Max_HP - HP) / 100;
//		HP = HP + (HP * pcent);
		Max_HP = 5 * CON;
		minDamage = STR / 2;
		maxDamage = minDamage + 4;
	}


	private void OnTriggerEnter (Collider other) {
		if (other.tag == "Potion") { 
			HP += (Max_HP / 30) * 100;
			if (HP > Max_HP)
				HP = Max_HP;
			Destroy (other.gameObject);
		}
	}
//	// Use this for initialization
//	void Start ()
//	{
//		
//		pt = 10;
//	}
//	
//	// Update is called once per frame
	// void Update () {
		
	// }
}

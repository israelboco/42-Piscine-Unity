using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stat_z : MonoBehaviour {

	public float STR;
	public float AGI;
	public float CON;
	public float ARMOR;
	public float HP;
	public float Max_HP;
	public float minDamage;
	public float maxDamage;
	public float LV;
	public float XP;
	public float money;


	private void Awake()
	{
		STR = LV;
		AGI = LV;
		CON = LV;
		ARMOR = 1;
		HP = 5 * CON;
		minDamage = STR / 2;
		maxDamage = minDamage + 4;
	}

//	// Use this for initialization
//	void Start ()
//	{
//	
//	}
//	
//	// Update is called once per frame
//	void Update () {
//		
//	}
}

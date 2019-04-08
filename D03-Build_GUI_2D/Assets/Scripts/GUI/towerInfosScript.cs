using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class towerInfosScript : MonoBehaviour {

	// Use this for initialization
	public towerScript tower;
	public Text damage, energy, range, reload;

	// Use this for initialization
	void Start () { }

	// Update is called once per frame
	void Update () {
		damage.text = tower.damage.ToString ();
		energy.text = tower.energy.ToString ();
		range.text = tower.range.ToString ();
		reload.text = tower.fireRate.ToString ();
	}
}
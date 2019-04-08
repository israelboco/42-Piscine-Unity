using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellsScript : MonoBehaviour {

	public SkillTree skill;
	public GameObject fireBall;
	public GameObject healParticle;
	public GameObject zoneImpact;

	bool CanlaunchHeal = true;
	bool CanlaunchFire = true;
	bool CanlaunchZone = true;
	float ReloadTime = 1;

	private void Start () {
		skill = GetComponent<SkillTree> ();

	}

	private void Update () {
		if (Input.GetKeyDown (KeyCode.Alpha1) && skill.Spells_select.Count > 0) {
			SearchSpellKey (0);

		} else if (Input.GetKeyDown (KeyCode.Alpha2) && skill.Spells_select.Count > 1) {
			SearchSpellKey (1);

		} else if (Input.GetKeyDown (KeyCode.Alpha3) && skill.Spells_select.Count > 2) {

			SearchSpellKey (2);

		} else if (Input.GetKeyDown (KeyCode.Alpha4) && skill.Spells_select.Count > 3) {
			SearchSpellKey (3);

		}

	}

	private void SearchSpellKey (int search) {
		if (skill.Spells_select.Count > 0) {
			switch (skill.Spells_select[search].name) {
				case "Healing":
					HealingSpell (skill.Spells_select[search]);
					break;
				case "Inferno":
					if (GetComponent<Stat> ().LV < 15)
						FireBallSpell (transform.up, skill.Spells_select[search]);
					else {
						MultiFireBallSpell (transform.up, skill.Spells_select[search]);
					}
					break;
				case "Invisibility":
					break;
				case "Nova":
					break;
				case "Rage":
					break;
				case "Fire_Ring":
					ZoneSpell (skill.Spells_select[search]);
					break;
				default:
					break;
			}
		}
	}

	private void ZoneSpell (Speel speel) {
		if (!CanlaunchZone)
			return;
		CanlaunchZone = false;
		// Vector3 pop = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		// pop.y = 0;
		Ray rayCast = Camera.main.ScreenPointToRay(Input.mousePosition);
		GameObject obj = GameObject.Instantiate(zoneImpact, rayCast.GetPoint(15), Quaternion.identity) as GameObject;
		obj.GetComponent<SpellsDommage> ().damage = speel.domage;
		Destroy(obj, 6f);
		Invoke ("ReloadZone", speel.cool_down);
	}








	/* Fire */

	public void FireBallSpell (Vector3 destination, Speel speel) {
		if (!CanlaunchFire)
			return;
		// GameObject newFireBall = GameObject.Instantiate (fireBall, transform.position, transform.rotation);
		SetDirectionSpell (GameObject.Instantiate (fireBall, new Vector3 (transform.position.x, fireBall.transform.position.y, transform.position.z), transform.rotation), destination, speel);
		CanlaunchFire = false;
		Invoke ("ReloadFire", speel.cool_down);
	}
	public void SetDirectionSpell (GameObject obj, Vector3 newdir, Speel speel) {
		Debug.Log (speel.name);
		obj.GetComponent<SpellsDommage> ().damage = speel.domage;
		Rigidbody rb = obj.GetComponent<Rigidbody> ();
		Quaternion newRotation = Quaternion.LookRotation (newdir - transform.position);
		obj.transform.rotation = Quaternion.Slerp (transform.rotation, newRotation, 0.2f);
		rb.velocity = transform.forward.normalized * 20f;
		Destroy (obj, 5f);
	}

	public void MultiFireBallSpell (Vector3 destination, Speel speel) {
		if (!CanlaunchFire)
			return;

		SetDirectionSpell (GameObject.Instantiate (fireBall, new Vector3 (transform.position.x, fireBall.transform.position.y, transform.position.z), transform.rotation), destination, speel);
		StartCoroutine (Destination (destination, speel));
		CanlaunchFire = false;
		Invoke ("ReloadFire", speel.cool_down);
	}

	IEnumerator Destination (Vector3 destination, Speel speel) {
		yield return new WaitForSeconds (0.5F);
		SetDirectionSpell (GameObject.Instantiate (fireBall, new Vector3 (transform.position.x, fireBall.transform.position.y, transform.position.z), transform.rotation), destination, speel);
		yield return new WaitForSeconds (0.5F);
		SetDirectionSpell (GameObject.Instantiate (fireBall, new Vector3 (transform.position.x, fireBall.transform.position.y, transform.position.z), transform.rotation), destination, speel);
	}

	/* Heal */
	public void HealingSpell (Speel speel) {
		if (!CanlaunchHeal)
			return;
		this.gameObject.GetComponent<Stat> ().HP += speel.domage * -1;
		Destroy (Instantiate (healParticle, transform.position, transform.rotation) as GameObject, 5f);
		CanlaunchHeal = false;
		Invoke ("ReloadHeal", speel.cool_down);
	}

	void ReloadHeal () {
		CanlaunchHeal = true;

	}
	void ReloadFire () {
		CanlaunchFire = true;

	}
	void ReloadZone () {
		CanlaunchZone = true;
	}

}
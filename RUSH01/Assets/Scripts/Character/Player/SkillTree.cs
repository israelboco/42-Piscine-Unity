using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class SkillTree : MonoBehaviour {

	public bool is_open = true;
	// Use this for initialization
	//	enum spell
	//	{
	//		
	//		care,Healing
	//		Inferno,
	//		Invisibility,
	//		Nova,
	//		Rage,
	//		Fire_Ring,
	//	}

	public GameObject Spell_tree_ui;

	public Stat player_stat;
	public List<Speel> spells;

	public List<GameObject> ui_ensembles;

	public int pc;

	private int last_lv;

	public Text info_speel;

	private bool firs_update = true;

	public List<Speel> Spells_select = new List<Speel> ();
	public List<GameObject> spell_bare;

	//	private void OnMouseEnter()
	//	{
	//		print("coucou");
	//	}

	private int index_selct = 0;
	void give_info (int i) {
		info_speel.text = spells[i].name + "\n " + spells[i].description + "\n info :\n Domage : " + spells[i].domage +
			"\n Cooldown :" + spells[i].cool_down + "\n\n info on Up :\n Domage : " + (spells[i].domage * spells[i].dom_ratio) +
			"\n Cooldown :" + (spells[i].cool_down - spells[i].cool_down_reduction);

		if (spells[i].lv > 0) {

			//foreach (Speel sp in Spells_select)
			for (int j = 0; j < Spells_select.Count; j++) {
				if (Spells_select[j].name == spells[i].name) {
					print (Spells_select[j].name + spells[i].name);
					print ("deja present");
					return;
				}
			}
			if (Spells_select.Count != 4)
				Spells_select.Add (spells[i]);
			else
				Spells_select[index_selct] = spells[i];
			if (index_selct < 3)
				index_selct++;
			else
				index_selct = 0;

			print (index_selct);
		}

	}

	void click_plus (GameObject ui, int index_spell) {

		if (pc <= 0 || player_stat.LV < spells[index_spell].lv_unlock || spells[index_spell].lv == 10) {
			print ("no pc or no lv");
			return;
		}
		pc--;
		Transform spell_ui = ui.transform.GetChild (0);
		Text spell_ui_txt = spell_ui.GetComponentInChildren<Text> ();
		spell_ui_txt.text = (int.Parse (spell_ui_txt.text) + 1).ToString ();
		spells[index_spell].lv = (int.Parse (spell_ui_txt.text) + 1);
		spells[index_spell].domage *= (spells[index_spell].dom_ratio);
		spells[index_spell].cool_down -= spells[index_spell].cool_down_reduction;
		if (spells[index_spell].cool_down <= 0) {
			spells[index_spell].cool_down = 1;
		}
		print ("np");
	}

	void click_moin (GameObject ui, int index_spell) {
		/// partie UI
		Transform spell_ui = ui.transform.GetChild (0);
		Text spell_ui_txt = spell_ui.GetComponentInChildren<Text> ();
		if (int.Parse (spell_ui_txt.text) <= 0)
			return;
		pc++;
		spell_ui_txt.text = (int.Parse (spell_ui_txt.text) - 1).ToString ();
		print (ui.name);
		// partie Stat
		spells[index_spell].lv--;
		spells[index_spell].domage *= (-spells[index_spell].dom_ratio);
		spells[index_spell].cool_down += spells[index_spell].cool_down_reduction;

	}

	private void Awake () {
		player_stat = GetComponent<Stat> ();
		// attention a renger dans l'ordre sinon sa decale tout;
		spells.Add (new Speel () { name = "Healing", domage = -10, cool_down = 10, cool_down_reduction = 1, dom_ratio = 1.5f, lv = 0, lv_unlock = 0 });
		spells.Add (new Speel () { name = "Inferno", domage = 10, cool_down = 10, cool_down_reduction = 1, dom_ratio = 1.5f, lv = 0, lv_unlock = 0 });
		spells.Add (new Speel () { name = "Invisibility", domage = 0, cool_down = 40, cool_down_reduction = 3, dom_ratio = 0, lv = 0, lv_unlock = 6 });
		spells.Add (new Speel () { name = "Nova", domage = 20, cool_down = 14, cool_down_reduction = 1, dom_ratio = 1.5f, lv = 0, lv_unlock = 6 });
		spells.Add (new Speel () { name = "Rage", domage = 10, cool_down = 25, cool_down_reduction = 1, dom_ratio = 1.2f, lv = 0, lv_unlock = 12 });
		spells.Add (new Speel () { name = "Fire_Ring", domage = 30, cool_down = 60, cool_down_reduction = 1, dom_ratio = 2.5f, lv = 0, lv_unlock = 12 });
	}

	void Start () {

		ui_ensembles = GameObject.FindGameObjectsWithTag ("Ui_ensemble").OrderBy (g => g.transform.GetSiblingIndex ()).ToList ();
		spell_bare = GameObject.FindGameObjectsWithTag ("icon_spell").OrderBy (g => g.transform.GetSiblingIndex ()).ToList ();
		pc = player_stat.LV;
		int i = 0;
		foreach (GameObject ui in ui_ensembles) {
			int k = i;
			Transform spell_mp = ui.transform.GetChild (1);
			// bouton + et -
			Button button_p = spell_mp.GetChild (0).GetComponent<Button> ();
			button_p.onClick.AddListener (() => click_plus (ui, k));
			Button button_m = spell_mp.GetChild (1).GetComponent<Button> ();
			button_m.onClick.AddListener (() => click_moin (ui, k));
			//
			Button button_speel = ui.transform.GetChild (0).GetComponent<Button> ();
			button_speel.onClick.AddListener (() => give_info (k));
			spells[i].Sprite_speel = button_speel.transform.GetComponent<Image> ().sprite;

			i++;

			last_lv = 0;

		}

		// Take lv of player
		//give point  ex: lv * 5
	}

	private void ui_to_grey () {
		last_lv = player_stat.LV;
		int i = 0;
		foreach (GameObject ui in ui_ensembles) {

			if (spells[i].lv_unlock <= player_stat.LV) {

				Image[] img = ui.GetComponentsInChildren<Image> ();
				foreach (Image image in img) {
					image.color = Color.white;
				}
			} else {

				Image[] img = ui.GetComponentsInChildren<Image> ();
				foreach (Image image in img) {
					image.color = Color.grey;
				}
			}

			i++;

		}
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.N) || firs_update) {
			if (firs_update) {
				ui_to_grey ();
				firs_update = false;
			}

			if (is_open == true) // si le menu et ouver on le ferme
			{
				is_open = false;
				Spell_tree_ui.SetActive (false);
				return;
			}
			is_open = true;
			Spell_tree_ui.SetActive (true);
		}

		if (is_open == false) // si le menu et pas ouver rien en sert de l'update
			return;

		if (player_stat.LV != last_lv) {
			pc = player_stat.LV - last_lv;
			ui_to_grey ();

		}

		int i = 0;
		foreach (GameObject icon in spell_bare) {
			if (i >= Spells_select.Count)
				break;
			icon.GetComponent<Image> ().sprite = Spells_select[i].Sprite_speel;
			i++;
		}
		//		foreach (var sp in Spells_select)
		//		{
		//			print(sp.name);
		//		}
		//		int i = 0;
		//		foreach (Speel sp in spells)
		//		{
		//			print("ok");
		//		}
		//		foreach (Speel sp in spells)
		//		{
		//			int str_val = int.Parse(ui_ensembles[i].transform.GetChild(0).GetComponentInChildren<Text>().text);
		//			print(str_val);
		//			if(sp.lv != str_val)
		//				print("need_to_up");
		//			i++;
		//
		//		}

	}
}